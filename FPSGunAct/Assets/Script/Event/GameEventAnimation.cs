using Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameEventAnimation : SoundManager
{
    //保守性を高めるために、Animatorのパラメータの名前を定数化しとく
    public const string EVENT1 = "Event_One.First_Event 0";
    public const string EVENT2 = "Event_Second.Second_Event";
    public const string EVENT3 = "Event_Third.Third_Event";
    public const string EVENT4 = "LastAnim_Tutorial.Tutorial_LastEvent";


    [SerializeField, Header("射撃用の敵を格納")]
    private GameObject[] farEnemy;

    [SerializeField, Header("近接用の敵を格納")]
    private GameObject[] closeEnemy;

    public GameObject playerHolder;


    public new List<AudioClip> eventSE;

    //イベントアニメーションを追加
    [SerializeField]
    public List<Animation> animations;

    //敵を倒した後のイベントの待ち時間。
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


        Debug.Log(StartCoroutine(AnimEvent()) + "呼ばれてるよん");
        Debug.Log(StartCoroutine(SecondEvent(playerHolder)) + "呼ばれてるよん");
        Debug.Log(StartCoroutine(LastEvent()) + "呼ばれてるよん");

        StartCoroutine(AnimEvent());
        StartCoroutine(SecondEvent(playerHolder));
        StartCoroutine(LastEvent());
    }

    private void Update()
    {

        for(var i = 0; i < farEnemy.Length; i++)
        {
            if (farEnemy[i] == null)//Missingだったら消す
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

   public IEnumerator AnimEvent()//はじめのアニメーション
   {
       if (farEnemy.Length == 0)
       {
           yield return new WaitForSeconds(eventWaitTime[0]);
           Debug.Log("なんでよばれねんだよーーーーーーーーー！！！");
           animations[0].Play(EVENT1);
           audioSourceSE.PlayOneShot(eventSE[0]);


           yield return new WaitForSeconds(eventWaitTime[1]);
           animations[1].Play(EVENT2);
           audioSourceSE.PlayOneShot(eventSE[1]);
       }
   }

    public IEnumerator SecondEvent(GameObject other)//道中のアニメーション
    {
        if (other.gameObject.CompareTag("Player"))
        {
            yield return new WaitForSeconds(eventWaitTime[2]);
            animations[2].Play(EVENT3);
            audioSourceSE.PlayOneShot(eventSE[2]);
        }
    }

    public IEnumerator LastEvent()/*チュートリアル最後のアニメーション*/
    {
            if (closeEnemy.Length == 0)
            {
                yield return new WaitForSeconds(eventWaitTime[3]);
                animations[3].Play(EVENT4);

                audioSourceSE.PlayOneShot(eventSE[3]);
            }
    }

}
