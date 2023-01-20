using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMove : MonoBehaviour
{
    [Header("PlayerÇÃê›íË")]
    [SerializeField, Header("PlayerÇÃí èÌÇÃë¨ìx")]
    private float _speed;


    private float _rotationSpeed = 500;

    Rigidbody rb;
    Animator anim;

    Quaternion rotation;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        anim = GetComponent<Animator>();
        rb   = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        
    }

    private void Move()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical   = Input.GetAxis("Vertical");
        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y , Vector3.up);
        var velocity           = horizontalRotation * new Vector3(horizontal , 0 , vertical).normalized;
        var newRotationSpeed   = _rotationSpeed * Time.deltaTime;


        if(velocity.sqrMagnitude > 0.5f)
        {
            rotation = Quaternion.LookRotation(velocity * _speed , Vector3.up);
            anim.SetFloat("Speed" , velocity.sqrMagnitude);
        }
        else
        {
            anim.SetFloat("Speed" , 0f);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation , rotation , newRotationSpeed);
    }

    private void Jump()
    {

    }

}
