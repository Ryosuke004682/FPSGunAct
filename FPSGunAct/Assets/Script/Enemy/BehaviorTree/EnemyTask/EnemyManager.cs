using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class EnemyManager : MonoBehaviour
{
    [SerializeField, Header("HP")] protected int _maxHp = 100;
    protected int _nowHP;

    [Header("�_���[�W�̎U�z��")]
    [Space]
    [SerializeField, Header("�ő�_���[�W")] private int _maxDmg = 35;
    [SerializeField, Header("�ŏ��_���[�W")] private int _minDmg = 5;


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

        Debug.Log("�������蓖�����Ă��B");

        if(isDeath)
        {
            Did();
            Debug.Log("���S�m�F");
        }
        return isDeath;
    }

    private void Did()
    {
        Debug.Log("���j�I�I");
        //���S�G�t�F�N�g�̎��Ԃ���������0.1�b��B
        Destroy(this.gameObject , 0.1f);

        //���S���̃G�t�F�N�g��

    }
}
