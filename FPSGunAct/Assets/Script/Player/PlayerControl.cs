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


    Rigidbody rb;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        InputMove();
    }

    private void FixedUpdate()
    {
        
    }

    void InputMove()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        Vector3 velocity = new (horizontal , 0 , vertical);
        var nomalizeSpeed = velocity.normalized;


        if(Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = nomalizeSpeed * _runSpeed;
        }
        else
        {
            rb.velocity = nomalizeSpeed * _speed;
        }
    }
}