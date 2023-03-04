using Cinemachine;
using DG.Tweening;
using UnityEngine;

//Move、Attack、Jump
namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerCore : MonoBehaviour
    {
        [Header("Playerの設定")]
        [Space]
        [SerializeField, Header("Playerの通常の速度")]   public static float _speed = 4.0f;
        [SerializeField, Header("Playerの走るスピード")] public static float _runSpeed = 8.0f;
        [SerializeField, Header("空中にいるときの移動量")] public float _airMovement = 3.0f;
        [SerializeField,Header("走る時のキー")] protected KeyCode inputSprintKey = KeyCode.LeftShift;


        //**攻撃、防御のパラメーター設定**
        [Header("攻撃の事、防御力の事")]
        [Space]
        [SerializeField, Tooltip("攻撃力")] protected int _attackPower = 50;
        [SerializeField, Tooltip("防御力")] protected int _defence = 20;

        protected bool isAttack;
        protected bool isHit;

        public Collider attackCollider;

        //**カメラ設定**
        [Header("カメラの設定")]
        [Space]
        [SerializeField, Header("カメラの回転量")] protected float _rotationSpeed = 500;
        [SerializeField, Header("メインカメラ")] protected CinemachineVirtualCamera mainCam;
        [SerializeField, Header("最初のジャンプのカメラ")] protected CinemachineVirtualCamera secondJumpCam;
        [SerializeField, Header("攻撃用のカメラ")] protected CinemachineVirtualCamera attackCam;
        [SerializeField, Header("ロックオンするときのキー")] protected KeyCode lockOnKey = KeyCode.R;
        [SerializeField, Header("ロックオン解除するときのキー")] protected KeyCode lockOnRelese = KeyCode.LeftControl;



        //**ジャンプ判定**
        [Header("ジャンプの設定")]
        [Space]
        [SerializeField, Header("ジャンプ力")] protected float _jumpPower = 5.0f;
        [SerializeField, Header("二段ジャンプ目の力")] protected float _secondJumpPower = 5.0f;

        protected int _jumpCount = 0;
        protected const int MAXJUMPCOUNT = 2;
        protected bool isJump_Frag;
        protected bool isSecondJump_Flag = false;
        protected bool isGrounded = false;


        //**エフェクト周り**
        [Header("エフェクト")]
        [Space]
        [SerializeField, Header("攻撃用エフェクト")] protected ParticleSystem attackPTL; //PTL = particle

        //**接地判定**
        //LayerMask groundLayer = 0;
        //private float groundDistance = 0.1f;

        //**コンポーネント**
        protected AudioSource _source;
        protected AudioClip[] clips;
        protected static Rigidbody rb;
        protected static Animator _anim;


        //**ロックオンの事**
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

        //重力
        //ジャンプ
        private void Jump()
        {
            Vector3 velocity = Vector3.up;

            if (Input.GetKeyDown(KeyCode.Space) &&  _jumpCount < MAXJUMPCOUNT )
            {
                isJump_Frag = true;

                //ジャンプ力
                rb.AddForce(velocity * _jumpPower , ForceMode.Impulse);

                _anim.SetBool("Jump" , true);
                _jumpCount++;

                Debug.Log($"ジャンプ前のMainCam : mainCam.Priority = {mainCam.Priority}");
                Debug.Log($"ジャンプ前のSecondCam : secondJumpCam.Priority = { secondJumpCam.Priority}");

                
                if (_jumpCount == MAXJUMPCOUNT && isJump_Frag == true)
                {

                    mainCam.Priority = 0;
                    secondJumpCam.Priority = 18;

                    Debug.Log($"ジャンプ後のmainCam :mainCam.Priority = {mainCam.Priority}");
                    Debug.Log($"ジャンプ後のSecondCam : secondJumpCam.Priority = {secondJumpCam.Priority}");

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
            if (Input.GetKeyDown(lockOnKey))//規定値は「Rキー」
            {
                //リストが空だったら止める。
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

                //**ロックオンシステム**
                //ターゲットをリストからセットする。
                mainCam.LookAt = enemyListManager.enemyList[targetCount];
                mainCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 5;
                secondJumpCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 5;

                targetCount++;
            }
            //**ロックオン解除
          　if(Input.GetKeyDown(lockOnRelese))
            {
                mainCam.LookAt = this.transform;
                mainCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 3;
            }

            if(target)
            {
                //ターゲットの座標を補完
                var position = Vector3.zero;
                position = target.position;

                //高さはカメラ基準にする。
                position.y = mainCam.LookAt.transform.position.y;
                mainCam.LookAt.transform.LookAt(position);
            }
        }

        //**攻撃しているかどうかの判定**
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

        //**animationのコライダーのON,OFF**
        public void OnCollider()
        {
            attackCollider.enabled = true;

            if (attackCollider.enabled == true)
            {//剣の軌跡系のパーティクルを入れるとこ
            }
            else
            { }

        }

        public void OffCollider()
        {
            attackCollider.enabled = false;
        }
 

        //**当たり判定全般**
        private void OnCollisionEnter(Collision other)
        {
            //攻撃が当たってるかどうか
            if (other.gameObject.CompareTag("Enemy1") || other.gameObject.CompareTag("Enemy2"))
            {
                isHit = true;
                Debug.Log("Attack＿HIT＿OK");
            }

            //地面か判定する
            if (other.gameObject.CompareTag("Ground"))
            {
                Debug.Log("True");
                mainCam.Priority = 19;
                _jumpCount = 0;
            }
        }
    }
}