using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Game_Event0 : MonoBehaviour
{
    [SerializeField, Header("�G���i�[")]
    private GameObject[] enemyObject;

    //First
    [SerializeField, Header("�A�j���[�^�[�̃p�����[�^������Ƃ�")]
    private string stateParameterName = "";

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool(stateParameterName, false);
    }

    private void Update()
    {
       
    }

    private void FixedUpdate()
    {
        StartCoroutine(Event());
    }

    //�������I���܂ł̎��Ԃ��v�����āA
    //�v���������Ԃ�EventEnd�ɕێ����������B

    public IEnumerator Event()
    {
        yield return null;

        enemyObject = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemyObject.Length);


        if (enemyObject.Length == 0)
        {
            anim.SetBool(stateParameterName, true);
        }
    }
}
