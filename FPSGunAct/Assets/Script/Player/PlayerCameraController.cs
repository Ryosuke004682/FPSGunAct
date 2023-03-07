using Cinemachine;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{

    //**カメラ設定**
    [Header("カメラの設定")]
    [Space]
    [SerializeField, Header("メインカメラ")] private CinemachineVirtualCamera mainCam;
    [SerializeField, Header("最初のジャンプのカメラ")] private CinemachineVirtualCamera jumpCam;
    [SerializeField, Header("攻撃用のカメラ")] private CinemachineVirtualCamera attackCam;   

    private CinemachinePOV mainCamPOV;
    private CinemachinePOV attackCamPOV;
    private CinemachinePOV jumpCamPOV;

    //シングルトン
    public static PlayerCameraController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("呼べてるよ");
        }
    }

    private void Start()
    {
        mainCamPOV = mainCam.GetCinemachineComponent<CinemachinePOV>();
        jumpCamPOV = jumpCam.GetCinemachineComponent<CinemachinePOV>();
        attackCamPOV = attackCam.GetComponent<CinemachinePOV>();
    }

    public void NomalCameraWark()
    {
        //いつものやつ映してるやつ
        //var mainCam = 
    }


    public void AttackCameraWark()
    {
        //**メインカメラの位置を遷移先のカメラに同期する
        var attackCamVertical = mainCamPOV.m_VerticalAxis.Value;
        attackCamPOV.m_VerticalAxis.Value = attackCamVertical;

        var attackCamHorizontal = mainCamPOV.m_HorizontalAxis.Value;
        attackCamPOV.m_VerticalAxis.Value = attackCamHorizontal;

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
