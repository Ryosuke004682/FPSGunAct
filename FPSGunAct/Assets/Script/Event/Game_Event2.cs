using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Event2 : MonoBehaviour
{

    [SerializeField, Header("�G���i�[")]
    private GameObject[] enemyObject;

    //Event_Scond�̃p�����[�^
    [SerializeField, Header("�A�j���[�^�[�̃p�����[�^������Ƃ�")]
    private string stateParameterName = "";

    Animator anim;
    AudioSource source;
    public AudioClip clip;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool(stateParameterName, false);
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
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
        source.Stop();
        enemyObject = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemyObject.Length);


        if (enemyObject.Length == 0)
        {
            yield return new WaitForSeconds(2);
            anim.SetBool(stateParameterName, true);
            source.playOnAwake = true;
            source.PlayOneShot(clip);
        }


    }
}
