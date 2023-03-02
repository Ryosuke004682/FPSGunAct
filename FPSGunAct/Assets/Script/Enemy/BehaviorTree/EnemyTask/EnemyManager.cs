using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    private int _health;

    private void Awake()
    {
        _health = 50;
    }

    public bool TaskHit()
    {
        _health -= 10;
 
        bool isDeath = _health <= 0;

        Debug.Log("しっかり当たってるよ。");

        if (isDeath)
        {
            Did();
        }
        
        Debug.Log("死亡確認");
        return isDeath;
    }

    private void Did()
    {
        Debug.Log("撃破！！");
        //死亡エフェクトの時間を加味して0.1秒後。
        Destroy(this.gameObject , 0.1f);

        //死亡時のエフェクト↓

    }
}
