using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : PlayerCore
{
    public static void PlayerJump(KeyCode keyCode)
    {
        var velocity = Vector3.up;

        if (Input.GetKeyDown(keyCode) && _jumpCount < MAXJUMPCOUNT)
        {
            isJump_Frag = true;
            rb.AddForce(velocity * Instance._jumpPower, ForceMode.Impulse);

            _anim.SetBool("Jump", true);
            _jumpCount++;

            if (_jumpCount == MAXJUMPCOUNT && isJump_Frag == true)
            {
                PlayerCameraController.CameraInstance.JumpCameraWark();
                isSecondJump_Flag = true;

                rb.AddForce(velocity * Instance._secondJumpPower, ForceMode.Impulse);
                _anim.SetBool("SecondJump", true);
            }
        }
        else
        {
            _anim.SetBool("Jump", false);
            _anim.SetBool("SecondJump", false);
        }
    }
    public static void ResetJump()
    {
        _jumpCount = 0;
    }
}
