using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Event2 : MonoBehaviour
{

    [SerializeField, Header("敵を格納")]
    private GameObject[] enemyObject;

    //Event_Scondのパラメータ
    [SerializeField, Header("アニメーターのパラメータを入れるとこ")]
    private string stateParameterName = "";

    private Animator anim;
    private AudioSource source;
    public AudioClip clip;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool(stateParameterName, false);
        source = GetComponent<AudioSource>();
        source.playOnAwake = true;
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
        source.Stop();
        enemyObject = GameObject.FindGameObjectsWithTag("Enemy1");
        Debug.Log(enemyObject.Length);


        if (enemyObject.Length == 0)
        {
            yield return new WaitForSeconds(2);
            anim.SetBool(stateParameterName, true);
            source.playOnAwake = true;
            
            Debug.Log(source.playOnAwake);
            source.PlayOneShot(clip);
        }


    }
}
