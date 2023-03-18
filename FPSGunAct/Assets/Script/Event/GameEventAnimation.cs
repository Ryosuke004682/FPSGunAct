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


    [SerializeField, Header("射撃用の敵を格納")]
    private GameObject[] farEnemy;

    [SerializeField, Header("近接用の敵を格納")]
    private GameObject[] closeEnemy;

    public GameObject playerHolder;

    //イベントアニメーションを追加
    [SerializeField]
    public List<Animator> animations;

    //敵を倒した後のイベントの待ち時間。
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

        for(var i = 0; i < farEnemy.Length; i++)//オブジェクトの情報を消す。
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

    public IEnumerator AnimEvent()//はじめのアニメーション
   {
       if (farEnemy.Length == 0)
       {
            Debug.Log("Firstコルーチンが呼ばれてる");

           yield return new WaitForSeconds(eventWaitTime[0]);
           animations[0].Play(EVENT1);
           audioSourceSE.PlayOneShot(eventSE[0]);


           yield return new WaitForSeconds(eventWaitTime[1]);
           animations[1].Play(EVENT2);
           audioSourceSE.PlayOneShot(eventSE[1]);

            yield return null;
       }
   }

    public IEnumerator SecondEvent(GameObject other)//道中のアニメーション
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Secondコルーチン呼ばれてる");
            yield return new WaitForSeconds(eventWaitTime[2]);
            animations[2].Play(EVENT3);
            audioSourceSE.PlayOneShot(eventSE[2]);

          yield return null;
        }
    }

    public IEnumerator LastEvent()/*チュートリアル最後のアニメーション*/
    {
       
        if (closeEnemy.Length == 0)
            {
                Debug.Log("Lastコルーチン呼ばれてる");
                yield return new WaitForSeconds(eventWaitTime[3]);
                animations[3].Play(EVENT4);

                audioSourceSE.PlayOneShot(eventSE[3]);
                 yield return null;
            }
            
    }

}
