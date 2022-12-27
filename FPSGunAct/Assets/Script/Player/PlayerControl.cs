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
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    void InputMove()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        Vector3 velocity = new Vector3(horizontal , 0 , vertical);
        velocity.Normalize();


        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.velocity = velocity * _runSpeed;
        }
        else
        {
            rb.velocity = velocity * _speed;
        }

        



    }

}