using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Move : PlayerCore
{

    public static void Control(float airMovementMul, bool isGrounded , KeyCode inputKey)
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        var movementDirection = Vector3.zero;

        if (Camera.main != null)
        {
            var camForward = Camera.main.transform.forward;

            camForward.y = 0.0f;
            camForward.Normalize();

            movementDirection = (camForward * vertical) + (Camera.main.transform.right * horizontal);
        }
        else
        {
            movementDirection = new Vector3(horizontal, 0.0f , vertical);
        }
        movementDirection.Normalize();

        var currentSpeed = PlayerCore.Instance._speed;


        if (Input.GetKey(inputKey))
        {
            currentSpeed = PlayerCore.Instance._runSpeed;
        }


        var velocity = movementDirection * currentSpeed;
        
        if(!isGrounded)
        {
            velocity *= airMovementMul;//空中に居るときのスピード
        }

        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        _anim.SetFloat("Speed" , velocity.sqrMagnitude);

    }

    public static void PlayerRotate(Vector3 camForward , Quaternion rotate ,Transform transform)
    {
        var rotateSpeed = 500;

        if (Camera.main != null)
        {
            camForward.y = 0.0f;
            rotate = Quaternion.LookRotation(camForward);

           
            transform.rotation = Quaternion.Slerp(transform.rotation ,  rotate, Time.deltaTime * rotateSpeed);
        }
    }
}
