using Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : SoundManager
{
    public const string EVENT1 = "First_Event";
    public const string EVENT2 = "Second_Event";
    public const string EVENT3 = "Third_Event";
    public const string EVENT4 = "Tutorial_LastEvent ";


    [SerializeField, Header("�ˌ��p�̓G���i�[")]
    private GameObject[] farEnemy;

    [SerializeField, Header("�ߐڗp�̓G���i�[")]
    private GameObject[] closeEnemy;

    public GameObject SecondEventObje;

    //�C�x���g�A�j���[�V������ǉ�
    [SerializeField]
    public List<Animator> animations;

    //�G��|������̃C�x���g�̑҂����ԁB
    public List<float> eventWaitTime;


    private new void Awake()
    {
        farEnemy   = GameObject.FindGameObjectsWithTag("Enemy1");
        closeEnemy = GameObject.FindGameObjectsWithTag("Enemy2");

        SecondEventObje.SetActive(false);
    }


    private void Start()
    {
        animations[0].SetBool(EVENT1, false);
        animations[1].SetBool(EVENT2, false);
        animations[2].SetBool(EVENT3, false);
        animations[3].SetBool(EVENT4, false);
    }

    private void Update()
    {
        FarEnemyHolder();
        CloseEnemy();
    }

    //Missing�ɂȂ��Ă�I�u�W�F�N�g�̏��������Ĕz����X�V�B
    public void FarEnemyHolder()
    {
        for(var i = 0; i < farEnemy.Length; i++)
        {
            if (farEnemy[i] == null)
            {
                farEnemy = farEnemy.Where((value , index) => index != i).ToArray();
                i--;
            }
            if (farEnemy.Length == 0)
            {
                StartCoroutine(AnimEvent());
            }
        }
    }

    public void CloseEnemy()
    {
        for(var i = 0; i < closeEnemy.Length; i++)
        {
            if (closeEnemy[i] == null)
            {
                closeEnemy = closeEnemy.Where((value, index) => index != i).ToArray();
                i--;
            }
            if(closeEnemy.Length == 0)
            {
                StartCoroutine(LastEvent());
            }
        }
    }

    public IEnumerator AnimEvent()//�͂��߂̃A�j���[�V����
   {
       if (farEnemy.Length == 0)
       {
            Debug.Log("First�R���[�`�����Ă΂�Ă�");

           yield return new WaitForSeconds(eventWaitTime[0]);
           animations[0].SetBool(EVENT1 , true);
           audioSourceSE.PlayOneShot(eventSE[0]);


           yield return new WaitForSeconds(eventWaitTime[1]);
           animations[1].SetBool(EVENT2 , true);
           audioSourceSE.PlayOneShot(eventSE[1]);

            yield return null;
       }
   }

    /*�����́A�X�N���v�g�uEventObj3_Anim�v�Ŏg���Ă܂��B*/
    public IEnumerator SecondEvent()//�����̃A�j���[�V����
    { 
        Debug.Log("Second�R���[�`���Ă΂�Ă�");
        yield return new WaitForSeconds(eventWaitTime[2]);
        animations[2].SetBool(EVENT3, true);
        audioSourceSE.PlayOneShot(eventSE[2]);

        SecondEventObje.SetActive(true);
    }

    /*�`���[�g���A���Ō�̃A�j���[�V����*/
    public IEnumerator LastEvent()
    {
        if (closeEnemy.Length == 0)
        {
            Debug.Log("Last�R���[�`���Ă΂�Ă�");
            yield return new WaitForSeconds(eventWaitTime[3]);
            animations[3].SetBool(EVENT4, true);
            audioSourceSE.PlayOneShot(eventSE[3]);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SecondEvent());
        }
    }
}
