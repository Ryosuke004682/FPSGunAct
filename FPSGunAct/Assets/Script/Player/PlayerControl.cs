using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.HID;
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
        [SerializeField, Header("�J�����̉�]��")] public float _rotationSpeed = 500;
        [SerializeField, Header("���C���J����")]   public CinemachineVirtualCamera mainCam;
        [SerializeField, Header("�ŏ��̃W�����v�̃J����")] public CinemachineVirtualCamera secondJumpCam;
        [SerializeField, Header("�U���p�̃J����")] private CinemachineVirtualCamera attckCam;
        [SerializeField, Header("���b�N�I������Ƃ��̃L�[")] private KeyCode lockOnKey = KeyCode.R;
        [SerializeField, Header("���b�N�I����������Ƃ��̃L�[")] private KeyCode lockOnRelese = KeyCode.LeftControl;




        //**�W�����v����**
        [Header("�W�����v�̐ݒ�")]
        [Space]
        [SerializeField, Header("�W�����v��")] private float _jumpPower = 5.0f;
        [SerializeField, Header("��i�W�����v�ڂ̗�")] private float _secondJumpPower = 5.0f;

        public int _jumpCount = 0;
        const int MAXJUMPCOUNT = 2;
        bool isJump_Frag;
        bool isSecondJump_Flag = false;


        //**�G�t�F�N�g����**
        [Header("�G�t�F�N�g")]
        [Space]
        [SerializeField, Header("�U���p�G�t�F�N�g")] private ParticleSystem attackPTL; //PTL = particle


        //**�ڒn����**
        //LayerMask groundLayer = 0;
        //private float groundDistance = 0.1f;


        //**�R���|�[�l���g**
        AudioSource _source;
        AudioClip[] clips;
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

            //var newRotationSpeed = _rotationSpeed * Time.deltaTime;

            if (velocity.sqrMagnitude > 1.0f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, rotate, Time.deltaTime);

                rotate = Quaternion.LookRotation(velocity);
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, 500.0f);

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

                Debug.Log($"�W�����v�O��MainCam : mainCam.Priority = {mainCam.Priority}");
                Debug.Log($"�W�����v�O��SecondCam : secondJumpCam.Priority = { secondJumpCam.Priority}");

                
                if (_jumpCount == MAXJUMPCOUNT && isJump_Frag == true)
                {

                    mainCam.Priority = 0;
                    secondJumpCam.Priority = 18;

                    Debug.Log($"�W�����v���mainCam :mainCam.Priority = {mainCam.Priority}");
                    Debug.Log($"�W�����v���SecondCam : secondJumpCam.Priority = {secondJumpCam.Priority}");

                    var newValue_Vertical = mainCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value;

                    secondJumpCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = newValue_Vertical;

                    var newValue_Horizontal = mainCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value;

                    secondJumpCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = newValue_Horizontal;


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
            if (Input.GetKeyDown(lockOnKey))//�K��l�́uR�L�[�v
            {
                //���X�g���󂾂�����~�߂�B
                if(enemyListManager.enemyList.Count == 0)
                {
                    mainCam.LookAt = this.transform;
                    mainCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 3;
                    secondJumpCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 3;

                    return;
                }

                if (enemyListManager.enemyList.Count <= targetCount)
                {
                    targetCount = 0;
                }

                //**���b�N�I���V�X�e��**
                //�^�[�Q�b�g�����X�g����Z�b�g����B
                mainCam.LookAt = enemyListManager.enemyList[targetCount];
                mainCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 5;
                secondJumpCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 5;

                targetCount++;
            }
            //**���b�N�I������
          �@if(Input.GetKeyDown(lockOnRelese))
            {
                mainCam.LookAt = this.transform;
                mainCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 3;
                
            }

            if(target)
            {
                //�^�[�Q�b�g�̍��W��⊮
                var position = Vector3.zero;
                position = target.position;

                //�����̓J������ɂ���B
                position.y = mainCam.LookAt.transform.position.y;
                mainCam.LookAt.transform.LookAt(position);
            }
        }

        //**�U�����Ă��邩�ǂ����̔���**
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

        //**�q�b�g�X�g�b�v**
        public void OnHitAttack()
        {
           

        }

        //**animation�̃R���C�_�[��ON,OFF**
        public void OnCollider()
        {
            attackCollider.enabled = true;

            if (attackCollider.enabled == true)
            {//�p�[�e�B�N���n������Ƃ�
            }
            else
            { }

        }

        public void OffCollider()
        {
            attackCollider.enabled = false;
        }
 

        //**�����蔻��S��**
        private void OnCollisionEnter(Collision other)
        {
            //�U�����������Ă邩�ǂ���
            if (other.gameObject.CompareTag("Enemy1") || other.gameObject.CompareTag("Enemy2"))
            {
                isHit = true;
                Debug.Log("Attack�QHIT�QOK");
            }

            //�n�ʂ����肷��
            if (other.gameObject.CompareTag("Ground"))
            {
                Debug.Log("True");
                mainCam.Priority = 19;
                _jumpCount = 0;
            }
        }
    }

    
}