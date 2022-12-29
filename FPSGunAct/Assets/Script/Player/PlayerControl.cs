using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField, Header("Playerの通常の速度")]
    private  float _speed = 2.0f;

    [SerializeField, Header("Playerの走るスピード")]
    private float _runSpeed = 5.0f;

    [SerializeField, Header("カメラの回転量")]
    private float rotationSpeed = 500;

    Rigidbody rb;

    Quaternion rotate;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        rotate = transform.rotation;
    }

    private void Update()
    {
       PlayerCore();
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

    void Jump()
    {

    }

}