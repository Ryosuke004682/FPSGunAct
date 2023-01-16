using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{

    [Header("Player�̐ݒ�")]
    [SerializeField, Header("Player�̒ʏ�̑��x")]
    private  float _speed = 2.0f;

    [SerializeField, Header("Player�̑���X�s�[�h")]
    private float _runSpeed = 5.0f;

    [SerializeField, Header("�󒆂ɂ���Ƃ��̃v���C���[�̈ړ��X�s�[�h")]
    private float airSpeed = 1.5f;

    [SerializeField, Header("�W�����v�̃J�E���^�[")]
    private int _jumpCount = 0;

    private int _maxJumpCount = 2;

    [SerializeField, Header("�W�����v��(������ �~1000���Ă�̂Œ���)")]
    private float _jumpPower = 300.0f;

    [SerializeField, Header("��i�W�����v�p�̗�")]
    private float _secondJumpPower = 5.0f;

    [SerializeField, Header("�d��")]
    private float fallSpeed = 5.0f;

    [SerializeField, Header("�d�͂ɂ�闎�����x")]
    private float multiplier = 2.0f;


    //�J����
    [Header("�J�����̐ݒ�")]
    [SerializeField, Header("�J�����̉�]��")]
    private float rotationSpeed = 500;

    bool isJump = false;
    bool isSecondJump; //�Q��ڂ̃W�����v�`�F�b�N
    bool isGraund = false;

    RaycastHit _hit;
    Ray _ray;

    Rigidbody rb;
    Animator _anim;
    Vector3 velocity;

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
        Gravity();
        InputJump();
    }

    private void FixedUpdate()
    {
        Jump();
        
    }

    void PlayerCore()
    {
        var vertical   = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        var horizontalRotate = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y , Vector3.up);

        var velocity = horizontalRotate * new Vector3(horizontal , 0 , vertical).normalized;
        
        var branchSpeed = Input.GetKey(KeyCode.LeftShift) ? velocity * _runSpeed : velocity * _speed;

        var newRotationSpeed = rotationSpeed * Time.deltaTime;

        rb.velocity = branchSpeed;


        if(velocity.sqrMagnitude > 0.5f)
        {
            rotate = Quaternion.LookRotation(velocity,Vector3.up);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation , rotate , newRotationSpeed);

    }
    
    void Jump()
    {
       if(isJump)
        {
            rb.velocity = Vector3.zero;

            rb.AddForce(Vector3.up * _jumpPower , ForceMode.Impulse);
            rb.AddForce(fallSpeed * Physics.gravity,ForceMode.Acceleration);

            _jumpCount++;
            isJump = false;
        }
    }

    void InputJump()
    {
        if (Input.GetKey(KeyCode.Space) && !_anim.GetCurrentAnimatorStateInfo(0).IsName("FirstJump")
                                        && !_anim.IsInTransition(0))
        {
            _anim.SetBool("FirstJump" , true);
            velocity.y += _jumpPower;
        }
        else if(_anim.GetBool("FirstJump") && !isJump && isSecondJump)
        {
            var airMove = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical")).normalized;
            velocity = new Vector3(airMove.x * airSpeed , velocity.y , airMove.z * airSpeed);

            //��i�ڂ̃W�����v�̏���
            if(Input.GetKeyDown(KeyCode.Space))
            {
                var getAnimTime = Mathf.Repeat(_anim.GetCurrentAnimatorStateInfo(0).normalizedTime , 1.0f);

                if(_anim.GetCurrentAnimatorStateInfo(0).IsName("FirstJump")�@&& 0.85f <= getAnimTime && getAnimTime <= 1.1f)
                {
                    isJump = true;
                    _anim.SetBool("FirstJump" , true);

                    transform.LookAt(transform.position + airMove.normalized);

                    velocity.y += _secondJumpPower;
                }
                else
                {
                    isSecondJump = true;
                }
            }
            else if (isSecondJump)
            {
                airMove = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
                velocity = new Vector3(airMove.x * airSpeed , velocity.y , airMove.z * airSpeed);
            }

            velocity.y += Physics.gravity.y * Time.deltaTime;
            rb.AddForce(velocity * Time.deltaTime);

        }
    }


    //�n�ʂ��ǂ������`�F�b�N���āA
    //�n�ʂȂ�d�͌v�Z�����Ȃ��B
    //�󒆂Ȃ�d�͂����X�Ɋ|���Ă����B
    //��i�W�����v��ɃX�g���Ɨ��Ƃ������B
    //����ɔ����āA��i�W�����v�ɂ������␳���|����B

    private void Gravity()
    {
        var distance = 0.25f;

        Vector3 rayPosition = transform.position + Vector3.zero;
        Ray ray = new Ray(rayPosition , Vector3.down);

        isGraund = Physics.Raycast(ray,distance);

        Debug.DrawRay(rayPosition , Vector3.down * distance , Color.green);


        //�����ŏd�͌v�Z
        var gravity = (isGraund) ? 0.0f : 8.5f;
        rb.AddForce( Physics.gravity * gravity , ForceMode.Acceleration);
    }


    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            _jumpCount = 0;
        }
    }
}