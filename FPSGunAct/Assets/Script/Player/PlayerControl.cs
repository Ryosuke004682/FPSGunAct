using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerControl : MonoBehaviour
    {
        [Header("Player�̐ݒ�")]
        [SerializeField, Header("Player�̒ʏ�̑��x")]
        private float _speed = 2.0f;

        [SerializeField, Header("Player�̑���X�s�[�h")]
        private float _runSpeed = 5.0f;

        [SerializeField, Header("�W�����v��")]
        private float _jumpPower = 5.0f;

        [SerializeField, Header("��i�W�����v�p�̗�")]
        private float _secondJumpPower = 5.0f;

        [SerializeField, Header("�d��")]
        private float fallSpeed = 5.0f;

        [SerializeField, Header("�W�����v")]
        private int _jumpCount = 0;

        LayerMask groundLayer = 0;


        private float groundDistance = 0.1f;

        //�J����
        [Header("�J�����̐ݒ�")]
        [SerializeField, Header("�J�����̉�]��")]
        private float rotationSpeed = 500;

        
        const int MAXJUMPCOUNT = 2;

        bool isJump_Frag;
        bool isSecondJump_Flag;
        bool isJump;
        bool isGround;

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
            Jump();
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
                rotate = Quaternion.LookRotation(velocity, Vector3.up);
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

        //�d��
        //�W�����v
        private void Jump()
        {
            Vector3 velocity = Vector3.up;

            if (Input.GetKeyDown(KeyCode.Space) &&  _jumpCount < MAXJUMPCOUNT )
            {
                isJump_Frag = true;
                isSecondJump_Flag = false;

                //�W�����v��
                rb.AddForce(velocity * _jumpPower , ForceMode.Impulse);

                _anim.SetBool("Jump" , true);
                _jumpCount++;


                if(_jumpCount == MAXJUMPCOUNT && isJump_Frag == true)
                {

                    isSecondJump_Flag = true;

                    rb.AddForce(velocity * _secondJumpPower , ForceMode.Impulse);

                    _anim.SetBool("SecondJump" , true);
                }
            }
            else
            {
                isJump_Frag = false;
                isSecondJump_Flag = false;

                _anim.SetBool("Jump" , false);
                _anim.SetBool("SecondJump" , false);
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