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
        [Header("Playerの設定")]
        [Space]
        [SerializeField, Header("Playerの通常の速度")]   private float _speed = 2.0f;
        [SerializeField, Header("Playerの走るスピード")] private float _runSpeed = 5.0f;
        [SerializeField, Header("重力")] private float fallSpeed = 5.0f;


        //**攻撃、防御のパラメーター設定**
        [Header("攻撃の事、防御力の事")]
        [Space]
        [SerializeField, Tooltip("攻撃力")] private int _attackPower = 50;
        [SerializeField, Tooltip("防御力")] private int _defence = 20;

        bool isAttack;
        bool isHit;

        public Collider attackCollider;

        //**カメラ設定**
        [Header("カメラの設定")]
        [Space]
        [SerializeField, Header("カメラの回転量")] public float _rotationSpeed = 500;
        [SerializeField, Header("メインカメラ")]   public CinemachineVirtualCamera mainCam;
        [SerializeField, Header("最初のジャンプのカメラ")] public CinemachineVirtualCamera secondJumpCam;
        [SerializeField, Header("攻撃用のカメラ")] private CinemachineVirtualCamera attckCam;
        [SerializeField, Header("ロックオンするときのキー")] private KeyCode lockOnKey = KeyCode.R;
        [SerializeField, Header("ロックオン解除するときのキー")] private KeyCode lockOnRelese = KeyCode.LeftControl;




        //**ジャンプ判定**
        [Header("ジャンプの設定")]
        [Space]
        [SerializeField, Header("ジャンプ力")] private float _jumpPower = 5.0f;
        [SerializeField, Header("二段ジャンプ目の力")] private float _secondJumpPower = 5.0f;

        public int _jumpCount = 0;
        const int MAXJUMPCOUNT = 2;
        bool isJump_Frag;
        bool isSecondJump_Flag = false;


        //**エフェクト周り**
        [Header("エフェクト")]
        [Space]
        [SerializeField, Header("攻撃用エフェクト")] private ParticleSystem attackPTL; //PTL = particle


        //**接地判定**
        //LayerMask groundLayer = 0;
        //private float groundDistance = 0.1f;


        //**コンポーネント**
        AudioSource _source;
        AudioClip[] clips;
        Rigidbody rb;
        Animator _anim;
        Quaternion rotate;


        //**ロックオンの事**
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

        //**ヒットストップ**
        public void OnHitAttack()
        {
           

        }

        //**animationのコライダーのON,OFF**
        public void OnCollider()
        {
            attackCollider.enabled = true;

            if (attackCollider.enabled == true)
            {//パーティクル系を入れるとこ
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