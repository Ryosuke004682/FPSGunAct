using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class EnemyManager : MonoBehaviour
{
    [SerializeField, Header("HP")] protected int _maxHp = 100;
    protected int _nowHP;

    [Header("ダメージの散布幅")]
    [Space]
    [SerializeField, Header("最大ダメージ")] private int _maxDmg = 35;
    [SerializeField, Header("最小ダメージ")] private int _minDmg = 5;


    [SerializeField] Slider hpSlider;
    [SerializeField] TMPro.TextMeshPro damageText;

    Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();

        _nowHP = _maxHp;
        hpSlider.maxValue = _nowHP;
        hpSlider.value    = _maxHp;
    }

    public bool TaskHit()
    {
        _maxHp -= Random.Range(_minDmg,_maxDmg);

        bool isDeath = _nowHP <= 0;

        Debug.Log("しっかり当たってるよ。");

        if(isDeath)
        {
            Did();
            Debug.Log("死亡確認");
        }
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
