using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator _anim;

    bool hit = false;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            Debug.Log("OK");
            hit = true;

            _anim.SetInteger("GiveDamage", Random.Range(1, 4));
        }
        else
        {
            _anim.SetInteger("GiveDamage" , 0);
        }
    }

}
