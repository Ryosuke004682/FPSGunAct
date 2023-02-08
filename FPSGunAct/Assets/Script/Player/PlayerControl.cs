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

        [SerializeField, Header("ジャンプ力")]
        private float _jumpPower = 5.0f;

        [SerializeField, Header("二段ジャンプ用の力")]
        private float _secondJumpPower = 5.0f;

        [SerializeField, Header("重力")]
        private float fallSpeed = 5.0f;

        [SerializeField, Header("ジャンプ")]
        private int _jumpCount = 0;

        const int MAXJUMPCOUNT = 2;


        LayerMask groundLayer = 0;
        private float groundDistance = 0.1f;

        //カメラ
        [Header("カメラの設定")]
        [SerializeField, Header("カメラの回転量")]
        private float rotationSpeed = 500;

        [SerializeField, Header("メインカメラ")]
        public CinemachineVirtualCamera mainCam;

        [SerializeField, Header("最初のジャンプのカメラワーク")]
        public CinemachineVirtualCamera firstJumpCam;

  
        //**ジャンプ判定**
        bool isJump_Frag;
        bool isSecondJump_Flag;
        bool isGround;


        //**攻撃判定**
        bool isAttack;
        bool isHit;
        
        Rigidbody rb;
        Animator _anim;
        public Collider attackCollider;

        Quaternion rotate;
        public GameObject playerHolder;

        private void Start()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = true;

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
            PlayerCamera();
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

        void PlayerCamera()
        {
            var angle = Input.GetAxis("Horizontal") * rotationSpeed;

            var playerPosition = playerHolder.transform.position;

            transform.RotateAround(playerPosition , Vector3.up,angle);
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
                firstJumpCam.Priority = 19;
                Debug.Log($"firstJumpCam.Priority = { firstJumpCam.Priority}");

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