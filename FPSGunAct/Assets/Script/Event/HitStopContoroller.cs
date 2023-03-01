using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopContoroller : MonoBehaviour
{
    //~‚ß‚é
    //~‚ß‚È‚ª‚ç—h‚ç‚·iƒJƒƒ‰‚ğj
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
        yield return new WaitForSeconds(stopFrame);
        Time.timeScale = 1;



    }
}
