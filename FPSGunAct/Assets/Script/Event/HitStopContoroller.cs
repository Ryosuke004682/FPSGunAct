using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopContoroller : MonoBehaviour
{
    //Ç‚ÇÈÇ±Ç∆
    //é~ÇﬂÇÈ
    //é~ÇﬂÇ»Ç™ÇÁóhÇÁÇ∑ÅiÉJÉÅÉâÇÅj
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
