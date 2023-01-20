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

        [SerializeField, Header("ジャンプのカウンター")]
        private int _jumpCount = 0;

        private int _maxJumpCount = 2;

        [SerializeField, Header("ジャンプ力")]
        private float _jumpPower = 5.0f;

        [SerializeField, Header("二段ジャンプ用の力")]
        private float _secondJumpPower = 5.0f;

        [SerializeField, Header("重力")]
        private float fallSpeed = 5.0f;

        [SerializeField, Header("重力による落下速度")]
        private float multiplier = 0.0f;

        //カメラ
        [Header("カメラの設定")]
        [SerializeField, Header("カメラの回転量")]
        private float rotationSpeed = 500;

        bool isJump = false;
        bool isGround = false;


        RaycastHit _hit;
        Ray _ray;

        Rigidbody rb;
        Animator _anim;



        Quaternion rotate;

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

            if (_jumpCount < _maxJumpCount && Input.GetKeyDown(KeyCode.Space))
            {

                isJump = true;

                if (_jumpCount == 0)
                {
                    _anim.SetBool("FirstJump", true);
                }
                else if (_jumpCount == 1)
                {
                    _anim.SetBool("SecondJump", true);
                }
                else if (_jumpCount == _maxJumpCount)
                {
                    _anim.SetBool("SecondJump", false);
                }
                else
                {
                    _anim.SetBool("FirstJump", false);
                    _anim.SetBool("SecondJump", false);
                }
            }
        }

        private void FixedUpdate()
        {
            Jump();
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
                rotate = Quaternion.LookRotation(velocity, Vector3.up);
            }

            //走る、通常アニメーション
            //var branchSpeed = Input.GetKey(KeyCode.LeftShift) ? velocity * _runSpeed : velocity * _speed;
            //rb.velocity = branchSpeed;

            if (Input.GetKey(KeyCode.Space))
            {
                var a = velocity * _runSpeed;
                _anim.SetFloat("Speed", _runSpeed);

                rb.velocity = a;
            }
            else
            {
                var b = velocity * _speed;
                _anim.SetFloat("Speed", _speed);

                rb.velocity = b;
            }




            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, newRotationSpeed);
        }

        //地面の接地判定
        //重力
        //ジャンプ
        private void Jump()
        {
            Vector3 velocity = Vector3.zero;

            if (isJump)
            {

            }


        }




        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                Debug.Log("True");
                _jumpCount = 0;
            }
        }
    }
}