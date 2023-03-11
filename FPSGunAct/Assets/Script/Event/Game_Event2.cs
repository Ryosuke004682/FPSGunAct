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

    private Animator anim; 
    private AudioSource source;
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

    public void PlaySE()
    {
        source.PlayOneShot(clip);
    }

    public IEnumerator Event()
    {
        enemyObject = GameObject.FindGameObjectsWithTag("Enemy1");
        Debug.Log(enemyObject.Length);

        if (enemyObject.Length == 0)
        {
            yield return new WaitForSeconds(2);
            anim.SetBool(stateParameterName, true);
        }
    }
}
