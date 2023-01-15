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

    [SerializeField, Header("�W�����v�̃J�E���^�[")]
    private float _jumpCount = 0;

    private float _maxJumpCount = 2;

    [SerializeField, Header("�W�����v��(������ �~1000���Ă�̂Œ���)")]
    private float _jumpPower = 300;

    [SerializeField, Header("�d�͂ɂ�闎�����x")]
    private float multiplier = 2f;


    //�J����
    [Header("�J�����̐ݒ�")]
    [SerializeField, Header("�J�����̉�]��")]
    private float rotationSpeed = 500;


    //���C�̐ݒ�
    [SerializeField]
    bool isEnable = false;

    bool isJump = false;
    bool isGraund   = false;

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

        if(_jumpCount < _maxJumpCount && Input.GetKeyDown(KeyCode.Space))
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
        }

       
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

            _jumpCount++;
            isJump = false;
        }
    }

    //�n�ʂ��ǂ������`�F�b�N���āA
    //�n�ʂȂ�d�͌v�Z�����Ȃ��B
    //�󒆂Ȃ�d�͂����X�Ɋ|���Ă����B
    //��i�W�����v��ɃX�g���Ɨ��Ƃ������B
    //����ɔ����āA��i�W�����v�ɂ������␳���|����B
    
    void GravitySetting()
    {
        if(isEnable == false)
            return;
        


        var gravity = Physics.gravity.y;

        //Ray�̓X�t�B�A�L���X�g�ŊǗ��������B

        var scale = transform.lossyScale.x * 0.5f;
        var isHit = Physics.SphereCast(transform.position , scale , transform.forward * 10 , out _hit);

        if(isHit)
        {
            Gizmos.DrawRay(transform.position , transform.forward * _hit.distance);
            Gizmos.DrawWireSphere(transform.position + transform.forward * (_hit.distance ) , scale);
        }
        else
        {
            Gizmos.DrawRay(transform.position , transform.forward * 100);
        }


    }


    

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            _jumpCount = 0;
        }
    }
}