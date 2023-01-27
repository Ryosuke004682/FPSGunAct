using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Damage : Damage_Master
{
    Animator _anim;

    void Start()
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
        if(other.gameObject.CompareTag("Sword") ||
           other.gameObject.CompareTag("Bullet"))
        {
            _anim.SetInteger("GiveDamage", UnityEngine.Random.Range(1 , 5)) ;
        }
        else
        {
            _anim.SetInteger("GiveDamage" , 0);
        }
    }
}
