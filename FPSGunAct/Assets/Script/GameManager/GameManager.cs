using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField, Header("“G‚ðŠi”[")]
    private GameObject[] enemyObject;

    Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetBool("Event_One", false);
    }

    private void Update()
    {
       
    }

    private void EventStanby()
    {
        enemyObject = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemyObject.Length);

        if (enemyObject.Length == 0)
        {
            _anim.SetBool("Event_One", true);
        }
    }
}
