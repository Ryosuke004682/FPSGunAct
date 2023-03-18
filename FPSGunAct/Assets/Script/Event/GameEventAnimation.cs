using Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameEventAnimation : SoundManager
{
    public string EVENT1 = "First_Event";
    public string EVENT2 = "Second_Event";
    public string EVENT3 = "Third_Event";
    public string EVENT4 = "Tutorial_LastEvent";


    [SerializeField, Header("�ˌ��p�̓G���i�[")]
    private GameObject[] farEnemy;

    [SerializeField, Header("�ߐڗp�̓G���i�[")]
    private GameObject[] closeEnemy;

    public GameObject playerHolder;

    //�C�x���g�A�j���[�V������ǉ�
    [SerializeField]
    public List<Animator> animations;

    //�G��|������̃C�x���g�̑҂����ԁB
    public List<float> eventWaitTime;

    EnemyParameter parameter = new EnemyParameter();


    private void Start()
    {
        foreach(var anims in GetComponentsInChildren<Animator>())
        {
            animations.Add(anims);

        }

        farEnemy = GameObject.FindGameObjectsWithTag("Enemy1");
        closeEnemy = GameObject.FindGameObjectsWithTag("Enemy2");

        audioSourceSE = GetComponent<AudioSource>();


        StartCoroutine(AnimEvent());
        StartCoroutine(SecondEvent(playerHolder));
        StartCoroutine(LastEvent());

    }

    private void Update()
    {
        FarEnemyHolder(farEnemy);
        CloseEnemy(closeEnemy);
    }

    public void FarEnemyHolder(GameObject[] farEnemy)
    {
        this.farEnemy = farEnemy;

        for(var i = 0; i < farEnemy.Length; i++)//�I�u�W�F�N�g�̏��������B
        {
            if (farEnemy[i] == null)
            {
                farEnemy = farEnemy.Where((value , index) => index != i).ToArray();
                i--;
            }
        }
    }

    public void CloseEnemy(GameObject[] closeEnemy)
    {
        this.closeEnemy = closeEnemy;

        for(var i = 0; i < closeEnemy.Length; i++)
        {
            if (closeEnemy[i] == null)
            {
                closeEnemy = closeEnemy.Where((value, index) => index != i).ToArray();
                i--;
            }
        }

    }

    public IEnumerator AnimEvent()//�͂��߂̃A�j���[�V����
   {
       if (farEnemy.Length == 0)
       {
            Debug.Log("First�R���[�`�����Ă΂�Ă�");

           yield return new WaitForSeconds(eventWaitTime[0]);
           animations[0].Play(EVENT1);
           audioSourceSE.PlayOneShot(eventSE[0]);


           yield return new WaitForSeconds(eventWaitTime[1]);
           animations[1].Play(EVENT2);
           audioSourceSE.PlayOneShot(eventSE[1]);

            yield return null;
       }
   }

    public IEnumerator SecondEvent(GameObject other)//�����̃A�j���[�V����
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Second�R���[�`���Ă΂�Ă�");
            yield return new WaitForSeconds(eventWaitTime[2]);
            animations[2].Play(EVENT3);
            audioSourceSE.PlayOneShot(eventSE[2]);

          yield return null;
        }
    }

    public IEnumerator LastEvent()/*�`���[�g���A���Ō�̃A�j���[�V����*/
    {
       
        if (closeEnemy.Length == 0)
            {
                Debug.Log("Last�R���[�`���Ă΂�Ă�");
                yield return new WaitForSeconds(eventWaitTime[3]);
                animations[3].Play(EVENT4);

                audioSourceSE.PlayOneShot(eventSE[3]);
                 yield return null;
            }
            
    }

}
