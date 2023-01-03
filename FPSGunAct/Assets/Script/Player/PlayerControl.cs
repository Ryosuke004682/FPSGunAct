using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField, Header("Playerの通常の速度")]
    private  float _speed = 2.0f;

    [SerializeField, Header("Playerの走るスピード")]
    private float _runSpeed = 5.0f;

    [SerializeField, Header("ジャンプ力")]
    private float _jumpPower;

    [SerializeField, Header("カメラの回転量")]
    private float rotationSpeed = 500;

    bool JumpCheack = false;
    bool isGraund   = false;

    RaycastHit _hit;
    Ray _ray;


    Rigidbody rb;
    Vector3 position;

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

    void GrandCheack()
    {

    }


    //重力も考える
    //ジャンプは二段ジャンプできるようにする。
    void Jump()
    {
     
    }
}