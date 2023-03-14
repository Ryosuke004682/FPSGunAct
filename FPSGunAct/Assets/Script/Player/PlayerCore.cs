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

        [SerializeField, Header("�J�����̉�]���x")] public float _rotationSpeed = 10.0f;

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


        //**�R���|�[�l���g**
        public AudioSource _source;
        public AudioClip[] clips;
        public int a;

        public static Rigidbody rb;
        public static Animator _anim;


        //**���b�N�I���̎�**
        public EnemyListManager enemyListManager;
        public Transform target;
        public int targetCount;


        private Pod_Attack podAttack;
        


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

        //����U��������SE��ǉ��������B
        public void OnAttackSE()
        {
            if (clips != null)
            {
                a = Random.Range(0, clips.Length);
            }
            _source.PlayOneShot(clips[a]);

        }
        public void OnAttackSEA()
        {
            if (clips != null)
            {
                a = Random.Range(0, clips.Length);
            }
            _source.PlayOneShot(clips[a]);

        }

        //**�����蔻��S��**
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
               Jump.ResetJump();
                      PlayerCameraController.CameraInstance.NomalCameraWark();
            }
        }

    }
}