using Cinemachine;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attack : PlayerCore
{
    public static void PlayerAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAttack = true;
            _anim.SetBool("Attack", true);
        }
        else if (Input.GetMouseButtonUp(0) && isAttack == true)
        {
            isAttack = false;
            _anim.SetBool("Attack", false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("押されてるよ");
            PlayerCameraController.CameraInstance.AttackCameraWark();

        }
        else if (Input.GetMouseButtonUp(1))
        {
            //ここに通常のカメラワークを入れる。
            PlayerCameraController.CameraInstance.NomalCameraWark();
        }
    }
}
