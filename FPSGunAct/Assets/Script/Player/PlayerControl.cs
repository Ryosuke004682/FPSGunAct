using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerControl : MonoBehaviour
    {
        [Header("Player�̐ݒ�")]
        [Space]
        [SerializeField, Header("Player�̒ʏ�̑��x")]   private float _speed = 2.0f;
        [SerializeField, Header("Player�̑���X�s�[�h")] private float _runSpeed = 5.0f;
        [SerializeField, Header("�d��")] private float fallSpeed = 5.0f;


        //**�U���A�h��̃p�����[�^�[�ݒ�**
        [Header("�U���̎��A�h��͂̎�")]
        [Space]
        [SerializeField, Tooltip("�U����")] private int _attackPower = 50;
        [SerializeField, Tooltip("�h���")] private int _defence = 20;

        bool isAttack;
        bool isHit;

        public Collider attackCollider;

        //**�J�����ݒ�**
        [Header("�J�����̐ݒ�")]
        [Space]
        [SerializeField, Header("�J�����̉�]��")] public float rotationSpeed = 500;
        [SerializeField, Header("���C���J����")]   public CinemachineVirtualCamera mainCam;
        [SerializeField, Header("�ŏ��̃W�����v�̃J�������[�N")] public CinemachineVirtualCamera secondJumpCam;


        //**�W�����v����**
        [Header("�W�����v�̐ݒ�")]
        [Space]
        [SerializeField, Header("�W�����v��")] private float _jumpPower = 5.0f;
        [SerializeField, Header("��i�W�����v�ڂ̗�")] private float _secondJumpPower = 5.0f;

        public int _jumpCount = 0;
        const int MAXJUMPCOUNT = 2;
        bool isJump_Frag;
        bool isSecondJump_Flag;
        bool isGround;


        //**�ڒn����**
        LayerMask groundLayer = 0;
        private float groundDistance = 0.1f;


        //**�R���|�[�l���g**
        AudioSource _source;
        Rigidbody rb;
        Animator _anim;
        Quaternion rotate;


        //**���b�N�I���̎�**
        public EnemyListManager enemyListManager;
        public Transform target;
        public int targetCount;


        private void Start()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = true;

            _source = GetComponent<AudioSource>();

            _anim = GetComponent<Animator>();

            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;

            rotate = transform.rotation;
        }

        private void Update()
        {
            Jump();
            Attack();
            TargetLockOn();
        }

        private void FixedUpdate()
        {
            PlayerCore();
        }

        void PlayerCore()
        {
            var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis("Horizontal");

            var horizontalRotate = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
            
            var velocity = horizontalRotate * new Vector3(horizontal, 0, vertical).normalized;

            var newRotationSpeed = rotationSpeed * Time.deltaTime;

            if (velocity.sqrMagnitude > 1.0f)
            {
                rotate = Quaternion.LookRotation(velocity);
                transform.rotation = Quaternion.Lerp(transform.rotation , rotate , Time.deltaTime * 20);
            }

            _anim.SetFloat("Speed", velocity.sqrMagnitude);

           
            if(Input.GetKey(KeyCode.LeftShift))
            {
                var runSpeed = velocity * _runSpeed;
                rb.velocity = runSpeed;

                _anim.SetBool("SprintSpeed" , true);
            }
            else
            {
                var nomalSpeed = velocity * _speed;
                rb.velocity = nomalSpeed;

                _anim.SetBool("SprintSpeed" , false);
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, newRotationSpeed);
        }

        //�d��
        //�W�����v
        private void Jump()
        {
            Vector3 velocity = Vector3.up;

            if (Input.GetKeyDown(KeyCode.Space) &&  _jumpCount < MAXJUMPCOUNT )
            {
                isJump_Frag = true;

                //�W�����v��
                rb.AddForce(velocity * _jumpPower , ForceMode.Impulse);

                _anim.SetBool("Jump" , true);
                _jumpCount++;

                mainCam.Priority = 0;
                secondJumpCam.m_Priority = 20;
                
                Debug.Log($"firstJumpCam.Priority = { secondJumpCam.Priority}");
                
                if (_jumpCount == MAXJUMPCOUNT && isJump_Frag == true)
                {

                    isSecondJump_Flag = true;

                    rb.AddForce(velocity * _secondJumpPower , ForceMode.Impulse);

                    _anim.SetBool("SecondJump" , true);
                }
            }
            else
            {
                _anim.SetBool("Jump" , false);
                _anim.SetBool("SecondJump" , false);
            }
        }


        void TargetLockOn()
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                //���X�g���󂾂�����~�߂�B
                if(enemyListManager.enemyList.Count == 0)
                {
                    return;
                }

                if (enemyListManager.enemyList.Count <= targetCount)
                {
                    targetCount = 0;
                }

                //�^�[�Q�b�g�����X�g����Z�b�g����B
                target = enemyListManager.enemyList[targetCount];

                targetCount++;
            }
          �@if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                target = null;
            }

            if(target)
            {
                //�^�[�Q�b�g�̍��W��⊮
                var position = Vector3.zero;
                position = target.position;
                //�����̓J������ɂ���B
                position.y = Camera.main.transform.position.y;

                Camera.main.transform.LookAt(position);
            }

        }


        void Attack()
        {
            if (Input.GetMouseButtonDown(0) && isAttack == false)
            {
                isAttack = true;
                _anim.SetBool("Attack", true);
            }
            else if(Input.GetMouseButtonUp(0))
            {
                isAttack = false;
                _anim.SetBool("Attack" , false);
            }
        }

        public void OffCollider()
        {
            attackCollider.enabled = false;
        }

        public void OnCollider()
        {
            attackCollider.enabled = true;

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Enemy2") && isHit == false)
            {
                //�����Ƀ_���[�W����
                Debug.Log("OK");
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                Debug.Log("True");
                mainCam.Priority = 20;
                _jumpCount = 0;
            }
        }
    }

    
}