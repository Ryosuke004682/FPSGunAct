using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Event2 : MonoBehaviour
{

    [SerializeField, Header("�G���i�[")]
    private GameObject[] enemyObject;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Event_Scond", false);
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
            anim.SetBool("Event_Scond", true);
        }
    }
}
