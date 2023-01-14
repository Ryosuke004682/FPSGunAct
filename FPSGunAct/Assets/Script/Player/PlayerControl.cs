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

    [SerializeField, Header("�W�����v��(������ �~1000���Ă�̂Œ���)")]
    private float _jumpPower = 300;

    [SerializeField, Header("�d�͂ɂ�闎�����x")]
    private float multiplier = 2f;


    //�J����

    [Header("�J�����̐ݒ�")]
    [SerializeField, Header("�J�����̉�]��")]
    private float rotationSpeed = 500;

    private float fallTime = 0f;


    bool JumpCheack = false;
    bool secondJumpCheack = false;
    bool isGraund   = false;

    RaycastHit _hit;
    Ray _ray;

    Rigidbody rb;
    Transform _transform;

    Quaternion rotate;

    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = true;

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

    //�n�ʂ��ǂ������`�F�b�N����B
    //�d�͂��l����
    //�W�����v�͓�i�W�����v�ł���悤�ɂ���B
    void Jump()
    {
        var gravity = Physics.gravity;
        var velocity = Vector3.down;

        if (_jumpCount < 2 && Input.GetKeyDown(KeyCode.Space))
        {
            JumpCheack = true;
            secondJumpCheack = false;

            rb.velocity = Vector3.zero;
            rb.AddForce(0.0f , _jumpPower * 1000 , 0.0f);
            _jumpCount++;

            if(_jumpCount == 1 && JumpCheack == true)
            {
                secondJumpCheack = true;
                isGraund = false;

                rb.velocity = velocity * 1.0f;
            }
            else if(_jumpCount == 2 && secondJumpCheack == true)
            {
                isGraund = false;
                rb.velocity += velocity * 5.0f;
            }
            else
            {
                isGraund = true;
                secondJumpCheack = false;
            }
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