using Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventAnimation : SoundManager
{
    //�ێ琫�����߂邽�߂ɁAAnimator�̃p�����[�^�̖��O��萔�����Ƃ�
    public const string EVENT1 = "FirstEvent" ;
    public const string EVENT2 = "SecondEvent";
    public const string EVENT3 = "ThirdEvent" ;



    [SerializeField, Header("�ˌ��p�̓G���i�[")]
    private GameObject[] enemyObject;

    //List��n�Ԗڂ̏��������邽�߂̑���Ɏg�p
    private List<string> stateParameterName;

    public new List<AudioClip> eventSE;

    //�C�x���g�A�j���[�V������ǉ�
    public List<Animator> animator = new List<Animator>(); 

    //�R���[�`���̑҂�����
    public List<float> eventWaitTime;

    private void Start()
    {
        audioSourceSE = GetComponent<AudioSource>();
        eventSE = new List<AudioClip>();


    animator = new List<Animator>(GetComponentsInChildren<Animator>());

        for (var i = 0; i < animator.Count; i++)
        {
            animator[i].GetComponent<Animator>().SetBool(stateParameterName[i] , false);
        }
       

        if(animator.Count >= 0)
        {
            StartCoroutine(AnimEvent());
        }
    }

    private void Update()
    {
     
    }

   public IEnumerator AnimEvent()
   {
        enemyObject = GameObject.FindGameObjectsWithTag("Enemy1");

        if(enemyObject.Length == 0)
        {
            yield return new WaitForSeconds(eventWaitTime[0]);
            animator[0].SetBool(EVENT1 , true);

            audioSourceSE.PlayOneShot(eventSE[0]);


            yield return new WaitForSeconds(eventWaitTime[1]);
            animator[1].SetBool(EVENT2 , true);
            audioSourceSE.PlayOneShot(eventSE[1]);
        }


        enemyObject = GameObject.FindGameObjectsWithTag("Enemy2");

        if(enemyObject.Length == 0)
        {
            yield return new WaitForSeconds(eventWaitTime[2]);
            animator[2].SetBool(EVENT3 , true);

            audioSourceSE.PlayOneShot(eventSE[2]);
        }
   }

}
