using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{
    [Header("Playerの設定")]
    [SerializeField, Header("Playerの通常の速度")]
    private  float _speed = 2.0f;

    [SerializeField, Header("Playerの走るスピード")]
    private float _runSpeed = 5.0f;

    [SerializeField, Header("ジャンプのカウンター")]
    private float _jumpCount = 0;

    private float _maxJumpCount = 2;

    [SerializeField, Header("ジャンプ力(内部で ×1000してるので注意)")]
    private float _jumpPower = 300;

    [SerializeField, Header("重力による落下速度")]
    private float multiplier = 2f;


    //カメラ
    [Header("カメラの設定")]
    [SerializeField, Header("カメラの回転量")]
    private float rotationSpeed = 500;


    //レイの設定
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

    //地面かどうかをチェックして、
    //地面なら重力計算をしない。
    //空中なら重力を徐々に掛けていく。
    //二段ジャンプ後にストンと落としたい。
    //それに伴って、二段ジャンプにも少し補正を掛ける。
    
    void GravitySetting()
    {
        if(isEnable == false)
            return;
        


        var gravity = Physics.gravity.y;

        //Rayはスフィアキャストで管理したい。

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