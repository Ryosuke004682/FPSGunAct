using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    //�Ƃ肠�����A�W�����v�����Ƃ��̉�p��ς��Ă݂�B
    
    [SerializeField, Header("�J�������[�N�̍ŏ���p")]
    private int minAngle = 10;

    [SerializeField, Header("�J�������[�N�̍ő��p")]
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


