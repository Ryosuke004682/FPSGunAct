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
        [SerializeField, Header("Player�̒ʏ�̑��x")] public float _speed = 2.0f;

        [SerializeField, Header("Player�̑���X�s�[�h")] public  float _runSpeed = 3.5f;

        [SerializeField, Header("�󒆂ɂ���Ƃ��̈ړ���")] public float _airMovement = 3.0f;

        [SerializeField,Header("���鎞�̃L�[")] protected KeyCode inputKey = KeyCode.LeftShift;



        //**�U���A�h��̃p�����[�^�[�ݒ�**
        [Header("�U���̎��A�h��͂̎�")]
        [Space]
        [SerializeField, Tooltip("�U����")] protected int _attackPower = 50;
        [SerializeField, Tooltip("�h���")] protected int _defence = 20;

        protected static bool isAttack;
        protected static bool isHit;

        public Collider attackCollider;

        //**�W�����v����**
        [Header("�W�����v�̐ݒ�")]
        [Space]
        [SerializeField, Header("�W�����v��")] public float _jumpPower = 5.0f;
        [SerializeField, Header("��i�W�����v�ڂ̗�")] public float _secondJumpPower = 5.0f;
        [SerializeField,Header("�W�����v�̃L�[")] public KeyCode _inputJumpKey = KeyCode.Space;

        public static int _jumpCount = 0;
        public const int MAXJUMPCOUNT = 2;
        public static bool isJump_Frag;
        public static bool isSecondJump_Flag = false;
        public static bool isGrounded = false;


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


        //�V���O���g��
        public static PlayerCore Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;

            _source = GetComponent<AudioSource>();
            _anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            
            rb.freezeRotation = true;
            playerTransform = transform.rotation;
        }

        private void Update()
        {
            //**�U���֘A**
            Attack.PlayerAttack();
           

            Jump.PlayerJump(_inputJumpKey);
           
        }

        private void FixedUpdate()
        {
            Move.Control(_airMovement, isGrounded, inputKey);
            Move.PlayerRotate(camForward, playerTransform, transform);
        }

        //**animation�̃R���C�_�[��ON,OFF**
        public void OnCollider()
        {
            attackCollider.enabled = true;
            Debug.Log("OnCollider�Ă΂�Ă��");

            if (attackCollider.enabled == true)
            {//���̋O�Ռn�̃p�[�e�B�N��������Ƃ�
            }
        }

        public void OffCollider()
        {
            Debug.Log("OffCollider�Ă΂�Ă��");
            attackCollider.enabled = false;
        }


        //**�����蔻��S��**
        private void OnCollisionEnter(Collision other)
        {
            
            if (other.gameObject.CompareTag("Ground"))
            {
                Debug.Log("True");
                //�����ɓ�i�W�����v��ɃJ���������Ƃɖ߂����߂̃v���p�e�B�������B
                PlayerCameraController.Instance.NomalCameraWark();
            }
        }
    }
}