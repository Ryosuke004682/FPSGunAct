using Cinemachine;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{

    //**�J�����ݒ�**
    [Header("�J�����̐ݒ�")]
    [Space]
    [SerializeField, Header("���C���J����")] private CinemachineVirtualCamera mainCam;
    [SerializeField, Header("�ŏ��̃W�����v�̃J����")] private CinemachineVirtualCamera jumpCam;
    [SerializeField, Header("�U���p�̃J����")] private CinemachineVirtualCamera attackCam;   

    private CinemachinePOV mainCamPOV;
    private CinemachinePOV attackCamPOV;
    private CinemachinePOV jumpCamPOV;

    //�V���O���g��
    public static PlayerCameraController CameraInstance;
    private void Awake()
    {
        if (CameraInstance == null)
        {
            CameraInstance = this;
            Debug.Log("�ĂׂĂ��");
        }
    }

    private void Start()
    {
        mainCamPOV = mainCam.GetCinemachineComponent<CinemachinePOV>();
        jumpCamPOV = jumpCam.GetCinemachineComponent<CinemachinePOV>();
        attackCamPOV = attackCam.GetCinemachineComponent<CinemachinePOV>();
    }

    public void NomalCameraWark()
    {
        //�����̂�f���Ă���
        mainCam.Priority   = 19;
        jumpCam.Priority   = 0;
        attackCam.Priority = 0;
    }


    public void AttackCameraWark()
    {
        //**���C���J�����̈ʒu��J�ڐ�̃J�����ɓ�������
        var attackCamVertical = mainCamPOV.m_VerticalAxis.Value;
        attackCamPOV.m_VerticalAxis.Value = attackCamVertical;

        var attackCamHorizontal = mainCamPOV.m_HorizontalAxis.Value;
        attackCamPOV.m_HorizontalAxis.Value = attackCamHorizontal;

        attackCam.Priority = 17;
        jumpCam.Priority = 0;
        mainCam.Priority = 0;
    }

    public void JumpCameraWark()
    {
        var jumpCamVertical = mainCamPOV.m_VerticalAxis.Value;
        jumpCamPOV.m_VerticalAxis.Value = jumpCamVertical;

        var jumpCamHorizontal = mainCamPOV.m_HorizontalAxis.Value;
        jumpCamPOV.m_HorizontalAxis.Value = jumpCamHorizontal;

        mainCam.Priority = 0;
        jumpCam.Priority = 18;
    }

    public void RunningCameraWark()
    {

    }
}
