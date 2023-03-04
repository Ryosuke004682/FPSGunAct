using Cinemachine;
using DG.Tweening;
using UnityEngine;

//Move�AAttack�AJump
namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerCore : MonoBehaviour
    {
        [Header("Player�̐ݒ�")]
        [Space]
        [SerializeField, Header("Player�̒ʏ�̑��x")]   public static float _speed = 4.0f;
        [SerializeField, Header("Player�̑���X�s�[�h")] public static float _runSpeed = 8.0f;
        [SerializeField, Header("�󒆂ɂ���Ƃ��̈ړ���")] public float _airMovement = 3.0f;
        [SerializeField,Header("���鎞�̃L�[")] protected KeyCode inputSprintKey = KeyCode.LeftShift;


        //**�U���A�h��̃p�����[�^�[�ݒ�**
        [Header("�U���̎��A�h��͂̎�")]
        [Space]
        [SerializeField, Tooltip("�U����")] protected int _attackPower = 50;
        [SerializeField, Tooltip("�h���")] protected int _defence = 20;

        protected bool isAttack;
        protected bool isHit;

        public Collider attackCollider;

        //**�J�����ݒ�**
        [Header("�J�����̐ݒ�")]
        [Space]
        [SerializeField, Header("�J�����̉�]��")] protected float _rotationSpeed = 500;
        [SerializeField, Header("���C���J����")] protected CinemachineVirtualCamera mainCam;
        [SerializeField, Header("�ŏ��̃W�����v�̃J����")] protected CinemachineVirtualCamera secondJumpCam;
        [SerializeField, Header("�U���p�̃J����")] protected CinemachineVirtualCamera attackCam;
        [SerializeField, Header("���b�N�I������Ƃ��̃L�[")] protected KeyCode lockOnKey = KeyCode.R;
        [SerializeField, Header("���b�N�I����������Ƃ��̃L�[")] protected KeyCode lockOnRelese = KeyCode.LeftControl;



        //**�W�����v����**
        [Header("�W�����v�̐ݒ�")]
        [Space]
        [SerializeField, Header("�W�����v��")] protected float _jumpPower = 5.0f;
        [SerializeField, Header("��i�W�����v�ڂ̗�")] protected float _secondJumpPower = 5.0f;

        protected int _jumpCount = 0;
        protected const int MAXJUMPCOUNT = 2;
        protected bool isJump_Frag;
        protected bool isSecondJump_Flag = false;
        protected bool isGrounded = false;


        //**�G�t�F�N�g����**
        [Header("�G�t�F�N�g")]
        [Space]
        [SerializeField, Header("�U���p�G�t�F�N�g")] protected ParticleSystem attackPTL; //PTL = particle

        //**�ڒn����**
        //LayerMask groundLayer = 0;
        //private float groundDistance = 0.1f;

        //**�R���|�[�l���g**
        protected AudioSource _source;
        protected AudioClip[] clips;
        protected static Rigidbody rb;
        protected static Animator _anim;


        //**���b�N�I���̎�**
        protected EnemyListManager enemyListManager;
        protected Transform target;
        protected int targetCount;

        protected Pod_Attack podAttack;


        public Vector3 camForward;
        protected Quaternion playerTransform;


        private void Start()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = true;

            _source = GetComponent<AudioSource>();

            _anim = GetComponent<Animator>();

            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;

            playerTransform = transform.rotation;
            

            mainCam.Priority = 19;
            
        }

        private void Update()
        {
           
            Jump();
            Attack();
            TargetLockOn();
        }

        private void FixedUpdate()
        {
            Move.Control(_airMovement, isGrounded, inputSprintKey);
            Move.PlayerRotate(camForward , playerTransform , transform , _rotationSpeed);
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
        public void Attack()
        {
            
            if (Input.GetMouseButtonDown(0) && isAttack == false)
            {
                isAttack = true;
                _anim.SetBool("Attack", true);
                Debug.Log($"isAttack = {isAttack}");

            }
            else if (Input.GetMouseButtonUp(0) && isAttack == true)
            {
                isAttack = false;
                _anim.SetBool("Attack", false);
            }

            if (Input.GetMouseButtonDown(1))
            {
                var newValue_Vertical = mainCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value;

                attackCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = newValue_Vertical;

                var newValue_Horizontal = mainCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value;

                attackCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = newValue_Horizontal;


                attackCam.Priority = 17;
                secondJumpCam.Priority = 0;
                mainCam.Priority = 0;

            }
            else if (Input.GetMouseButtonUp(1))

            {
                var attackCamValue_Vertical = attackCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value;

                mainCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = attackCamValue_Vertical;

                var attackCamValue_Horizontal = attackCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value;

                mainCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = attackCamValue_Horizontal;


                mainCam.Priority = 19;
                attackCam.Priority = 0;
                secondJumpCam.Priority = 0;
            }
        }

        //**animation�̃R���C�_�[��ON,OFF**
        public void OnCollider()
        {
            attackCollider.enabled = true;

            if (attackCollider.enabled == true)
            {//���̋O�Ռn�̃p�[�e�B�N��������Ƃ�
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