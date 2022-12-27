using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Event2 : MonoBehaviour
{

    [SerializeField, Header("敵を格納")]
    private GameObject[] enemyObject;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Event_Scond", false);
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        StartCoroutine(Event());
    }

    //処理が終わるまでの時間を計測して、
    //計測した時間をEventEndに保持させたい。

    public IEnumerator Event()
    {
        yield return null;

        enemyObject = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemyObject.Length);


        if (enemyObject.Length == 0)
        {
            anim.SetBool("Event_Scond", true);
        }
    }
}
