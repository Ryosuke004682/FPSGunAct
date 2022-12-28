using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game_Event1 : MonoBehaviour
{
    //Playerがコライダーに当たったら、Eventを発生させる。

    [SerializeField,Header("アニメーションの名前を入れるとこ")]
    private string _animName = "";

    [SerializeField, Header("トリガーのタグネームを入れるとこ")]
    private string _eventerTag = "";

    Animator _anim;


    private void Start()
    {
        _anim = GetComponent<Animator>();

        _anim.SetBool(_animName,false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == _eventerTag)
        {
            StartCoroutine(EventAction());
        }
    }


    IEnumerator EventAction()
    {
        yield return new WaitForSeconds(5);
        _anim.SetBool(_animName, true);
    }


    

}
