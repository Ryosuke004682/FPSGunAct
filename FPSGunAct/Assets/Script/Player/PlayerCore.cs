using Cinemachine;
using DG.Tweening;
using UnityEngine;

//Move、Attack、Jump
namespace Player
{
    /*
     メンバ変数には[_]アンダースコアを付けてる。
     定数は全部大文字
     bool変数には、「is～」を付けてる。
     イベント関数には、「On～」を付けてる。
     */

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerCore : MonoBehaviour
    {
        [Header("Playerの設定")]
        [Space]
        [SerializeField, Header("Playerの通常の速度")] public float _speed = 2.0f;

        [SerializeField, Header("Playerの走るスピード")] public  float _runSpeed = 3.5f;

        [SerializeField, Header("空中にいるときの移動量")] public float _airMovement = 3.0f;

        [SerializeField, Header("カメラの回転速度")] public float _rotationSpeed = 10.0f;

        [SerializeField,Header("走る時のキー")] protected KeyCode inputKey = KeyCode.LeftShift;



        //**攻撃、防御のパラメーター設定**
        [Header("攻撃の事、防御力の事")]
        [Space]
        [SerializeField, Tooltip("攻撃力")] protected int _attackPower = 50;
        [SerializeField, Tooltip("防御力")] protected int _defence = 20;

        protected static bool isAttack;
        protected static bool isHit;

        public Collider attackCollider;

        //**ジャンプ判定**
        [Header("ジャンプの設定")]
        [Space]
        [SerializeField, Header("ジャンプ力")] public float _jumpPower = 5.0f;
        [SerializeField, Header("二段ジャンプ目の力")] public float _secondJumpPower = 5.0f;
        [SerializeField,Header("ジャンプのキー")] public KeyCode _inputJumpKey = KeyCode.Space;

        public static int _jumpCount = 0;
        public const int MAXJUMPCOUNT = 2;
        public static bool isJump_Frag;
        public static bool isSecondJump_Flag = false;
        public static bool isGrounded = false;


        //**エフェクト周り**
        [Header("エフェクト")]
        [Space]
        [SerializeField, Header("攻撃用エフェクト")] protected ParticleSystem attackPTL; //PTL = particle


        [Header("接地判定、登れる段差の設定")]
        [Space]
        [SerializeField, Header("段差をのぼるためのレイを飛ばす")] private Transform transform;
        [SerializeField, Header("レイの距離")] private float _rayDistance = 10.0f;
        [SerializeField, Header("登れる段差 (地面からのレイの長さ)")] private float _stepOffSet = 0.5f;
        [SerializeField, Header("登れる角度")] private float _angle = 70.0f;

        int layermask;


        //**コンポーネント**
        public AudioSource _source;
        public AudioClip[] clips;
        public static Rigidbody rb;
        public static Animator _anim;


        //**ロックオンの事**
        public EnemyListManager enemyListManager;
        public Transform target;
        public int targetCount;

        private Pod_Attack podAttack;


        //シングルトン
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

            layermask = ~(1 << LayerMask.NameToLayer("Player"));//プレイヤータグ以外のレイヤー全部

        }

        private void Update()
        {
            //**攻撃関連**
            Attack.PlayerAttack();
            Jump.PlayerJump(_inputJumpKey);
           
        }

        private void FixedUpdate()
        {
            Move.Control(_airMovement, isGrounded, inputKey);
            
        }

        //**animationのコライダーのON,OFF**
        public void OnCollider()
        {
            attackCollider.enabled = true;
            Debug.Log("OnCollider呼ばれてるよ");

            if (attackCollider.enabled == true)
            {//剣の軌跡系のパーティクルを入れるとこ
            }
        }

        public void OffCollider()
        {
            Debug.Log("OffCollider呼ばれてるよ");
            attackCollider.enabled = false;
        }


        //**当たり判定全般**
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                Debug.Log("True");
                PlayerCameraController.CameraInstance.NomalCameraWark();//未実装
            }
        }
    }
}