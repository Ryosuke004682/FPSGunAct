using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game_Event1 : MonoBehaviour
{
    //Player���R���C�_�[�ɓ���������AEvent�𔭐�������B

    [SerializeField,Header("�A�j���[�V�����̖��O������Ƃ�")]
    private string _animName = "";

    [SerializeField, Header("�g���K�[�̃^�O�l�[��������Ƃ�")]
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
