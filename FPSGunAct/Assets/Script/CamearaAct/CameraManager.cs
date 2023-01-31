using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : CinemachineExtension
{
    //とりあえず、ジャンプしたときの画角を変えてみる。
    
    [SerializeField, Header("カメラワークの最小画角")]
    private int minAngle = 10;

    [SerializeField, Header("カメラワークの最大画角")]
    private int maxAngle = 80;


    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam 
        , CinemachineCore.Stage stage 
        , ref CameraState state 
        , float deltaTime)
    {
        Debug.Log($"stage = {stage}");
    }
}


