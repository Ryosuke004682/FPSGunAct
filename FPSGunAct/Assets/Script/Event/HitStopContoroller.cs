using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopContoroller : MonoBehaviour
{
    //��邱��
    //�~�߂�
    //�~�߂Ȃ���h�炷�i�J�������j
    public static HitStopContoroller hitStop;

    private void Awake()
    {
        hitStop = this;
    }

    public void Stop(float stopFrame)
    {
        StartCoroutine(HitStopAct(stopFrame));
    }

    private IEnumerator HitStopAct(float stopFrame)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(stopFrame);
        Time.timeScale = 1;
    }
}
