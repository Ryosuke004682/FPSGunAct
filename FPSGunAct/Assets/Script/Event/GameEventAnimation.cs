using Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameEventAnimation : SoundManager
{
    //�ێ琫�����߂邽�߂ɁAAnimator�̃p�����[�^�̖��O��萔�����Ƃ�
    public const string EVENT1 = "Event_One.First_Event 0";
    public const string EVENT2 = "Event_Second.Second_Event";
    public const string EVENT3 = "Event_Third.Third_Event";
    public const string EVENT4 = "LastAnim_Tutorial.Tutorial_LastEvent";


    [SerializeField, Header("�ˌ��p�̓G���i�[")]
    private GameObject[] farEnemy;

    [SerializeField, Header("�ߐڗp�̓G���i�[")]
    private GameObject[] closeEnemy;

    public GameObject playerHolder;


    public new List<AudioClip> eventSE;

    //�C�x���g�A�j���[�V������ǉ�
    [SerializeField]
    public List<Animation> animations;

    //�G��|������̃C�x���g�̑҂����ԁB
    public List<float> eventWaitTime;

    EnemyParameter parameter = new EnemyParameter();


    private void Start()
    {
        foreach(var anims in GetComponentsInChildren<Animation>())
        {
            animations.Add(anims);
        }


        farEnemy = GameObject.FindGameObjectsWithTag("Enemy1");
        closeEnemy = GameObject.FindGameObjectsWithTag("Enemy2");

            audioSourceSE = GetComponent<AudioSource>();
            eventSE = new List<AudioClip>();


        Debug.Log(StartCoroutine(AnimEvent()) + "�Ă΂�Ă���");
        Debug.Log(StartCoroutine(SecondEvent(playerHolder)) + "�Ă΂�Ă���");
        Debug.Log(StartCoroutine(LastEvent()) + "�Ă΂�Ă���");

        StartCoroutine(AnimEvent());
        StartCoroutine(SecondEvent(playerHolder));
        StartCoroutine(LastEvent());
    }

    private void Update()
    {

        for(var i = 0; i < farEnemy.Length; i++)
        {
            if (farEnemy[i] == null)//Missing�����������
            {
                farEnemy = farEnemy.Where((value , index) => index != i).ToArray();
                i--;
            }
        }

        for(var i = 0; i < closeEnemy.Length; i++)
        {
            if (closeEnemy[i] == null)
            {
                closeEnemy = closeEnemy.Where((value , index) => index != i).ToArray();
            }
        }

        Debug.Log(farEnemy.Length);
        Debug.Log(closeEnemy.Length);


    }

   public IEnumerator AnimEvent()//�͂��߂̃A�j���[�V����
   {
       if (farEnemy.Length == 0)
       {
           yield return new WaitForSeconds(eventWaitTime[0]);
           Debug.Log("�Ȃ�ł�΂�˂񂾂�[�[�[�[�[�[�[�[�[�I�I�I");
           animations[0].Play(EVENT1);
           audioSourceSE.PlayOneShot(eventSE[0]);


           yield return new WaitForSeconds(eventWaitTime[1]);
           animations[1].Play(EVENT2);
           audioSourceSE.PlayOneShot(eventSE[1]);
       }
   }

    public IEnumerator SecondEvent(GameObject other)//�����̃A�j���[�V����
    {
        if (other.gameObject.CompareTag("Player"))
        {
            yield return new WaitForSeconds(eventWaitTime[2]);
            animations[2].Play(EVENT3);
            audioSourceSE.PlayOneShot(eventSE[2]);
        }
    }

    public IEnumerator LastEvent()/*�`���[�g���A���Ō�̃A�j���[�V����*/
    {
            if (closeEnemy.Length == 0)
            {
                yield return new WaitForSeconds(eventWaitTime[3]);
                animations[3].Play(EVENT4);

                audioSourceSE.PlayOneShot(eventSE[3]);
            }
    }

}
