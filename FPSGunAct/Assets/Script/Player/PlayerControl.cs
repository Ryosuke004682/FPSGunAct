using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerControl : MonoBehaviour
    {
        [Header("Playerの設定")]
        [SerializeField, Header("Playerの通常の速度")]
        private float _speed = 2.0f;

        [SerializeField, Header("Playerの走るスピード")]
        private float _runSpeed = 5.0f;

        [SerializeField, Header("重力")]
        private float fallSpeed = 5.0f;


        //**攻撃、防御のパラメーター設定**
        [Header("攻撃の事、防御力の事")]
        [SerializeField, Tooltip("攻撃力")]
        private int _attackPower = 50;

        [SerializeField, Tooltip("防御力")]
        private int _defence = 20;

        bool isAttack;
        bool isHit;

        public Collider attackCollider;


        //カメラ設定
        [Header("カメラの設定")]
        [SerializeField, Header("カメラの回転量")]
        private float rotationSpeed = 500;

        [SerializeField, Header("メインカメラ")]
        public CinemachineVirtualCamera mainCam;

        [SerializeField, Header("最初のジャンプのカメラワーク")]
        public CinemachineVirtualCamera secondJumpCam;


        //**ジャンプ判定**
        [Header("ジャンプの設定")]
        [SerializeField, Header("ジャンプ力")]
        private float _jumpPower = 5.0f;

        [SerializeField, Header("二段ジャンプ目の力")]
        private float _secondJumpPower = 5.0f;

        private int _jumpCount = 0;
        const int MAXJUMPCOUNT = 2;
        bool isJump_Frag;
        bool isSecondJump_Flag;
        bool isGround;

        //**接地判定**
        LayerMask groundLayer = 0;
        private float groundDistance = 0.1f;

        
        AudioSource _source;
        Rigidbody rb;
        Animator _anim;
        Quaternion rotate;

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
            PlayerCore();
            Jump();
            Attack();
        }

        private void FixedUpdate()
        {
           
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


                mainCam.Priority = 0;
                secondJumpCam.Priority = 19;
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

        private void Angle()
        {
            var cam = secondJumpCam.GetCinemachineComponent(CinemachineCore.Stage.Aim).GetComponent<CinemachinePOV>().m_VerticalAxis.Value;
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
                //ここにダメージ処理
                Debug.Log("当たってるよ～ん");
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