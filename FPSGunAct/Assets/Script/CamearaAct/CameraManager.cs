using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    //とりあえず、ジャンプしたときの画角を変えてみる。
    
    [SerializeField, Header("カメラワークの最小画角")]
    private int minAngle = 10;

    [SerializeField, Header("カメラワークの最大画角")]
    private int maxAngle = 80;

    [SerializeField]
    GameObject player;

    private void CameraAngle_Jump()
    {
        var distance_Jump = transform.position;
        distance_Jump += this.transform.forward;
    }

    private void CameraAngle_Attack()
    {
        var distance = transform.position;
        

    }

    private void CameraAngle_PodAttack()
    {

    }
}


