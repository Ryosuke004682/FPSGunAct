using Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Event2 : SoundManager
{
    [SerializeField, Header("�G���i�[")]
    private GameObject[] enemyObject;

    //Event_Scond�̃p�����[�^
    [SerializeField, Header("�A�j���[�^�[�̃p�����[�^������Ƃ�")]
    private string stateParameterName = "";
    private Animator anim; 

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

    public IEnumerator Event()
    {
        enemyObject = GameObject.FindGameObjectsWithTag("Enemy1");


        if (enemyObject.Length == 0)
        {
            yield return new WaitForSeconds(2);
            anim.SetBool(stateParameterName, true);


            audioSourceSE = GetComponent<AudioSource>();
            var eventSE1 = eventSE[0];
            audioSourceSE.PlayOneShot(eventSE1);
        }
    }
}
