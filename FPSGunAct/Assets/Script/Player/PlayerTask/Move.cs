using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class Move : PlayerCore
{

    PlayerCore core = null;

    public static void Control(float airMovementMul, bool isGrounded, KeyCode inputKey)
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        var isRun = false;

        var movementDirection = Vector3.zero;

        if (Camera.main != null)
        {
            var camForward = Camera.main.transform.forward;
            var camRight = Camera.main.transform.right;

            camForward.y = 0.0f;
            camForward.Normalize();

            movementDirection = (camForward * vertical) + (camRight * horizontal);


            if (isRun == false)
            {
                _anim.SetFloat("Speed", movementDirection.sqrMagnitude);
                _anim.SetBool("SprintSpeed", false);
            }

            if(movementDirection.sqrMagnitude > 0.01f)
            {
                var lookRotation = Quaternion.LookRotation(movementDirection);

                Instance.transform.rotation = Quaternion.Slerp(Instance.transform.rotation, lookRotation, Time.deltaTime * Instance._rotationSpeed);
            }
        }
        else
        {
            movementDirection = new Vector3(horizontal, 0.0f, vertical);
            _anim.SetFloat("Speed", movementDirection.sqrMagnitude);

        }
        movementDirection.Normalize();


        var currentSpeed = Instance._speed; //Instance = PlayerCore.Instance

        if (Input.GetKey(inputKey))
        {
            isRun = true;

            if (isRun == true && movementDirection.sqrMagnitude >= 0.8f)
            {
                currentSpeed = Instance._runSpeed;
                _anim.SetBool("SprintSpeed", true);
            }
        }

        var velocity = movementDirection * currentSpeed;

        if (!isGrounded)
        {
            velocity *= airMovementMul;//空中に居るときのスピード
        }

        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        _anim.SetFloat("Speed", velocity.sqrMagnitude);
    }
}
