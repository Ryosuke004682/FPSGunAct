using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : CinemachineExtension
{
    //�Ƃ肠�����A�W�����v�����Ƃ��̉�p��ς��Ă݂�B
    
    [SerializeField, Header("�J�������[�N�̍ŏ���p")]
    private int minAngle = 10;

    [SerializeField, Header("�J�������[�N�̍ő��p")]
    private int maxAngle = 80;


    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam 
        , CinemachineCore.Stage stage 
        , ref CameraState state 
        , float deltaTime)
    {
        Debug.Log($"stage = {stage}");
    }
}


