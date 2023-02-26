using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class EnemyParameter : Damage_Master
{
    [SerializeField] private int _maxHp;
    [SerializeField] private int _nowHP;

    [SerializeField] Slider hpSlider;


    [SerializeField] private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();

        hpSlider.value = _maxHp;
        _nowHP = _maxHp;

    }

    private void Update()
    {
        hpSlider.transform.rotation = Camera.main.transform.rotation;
    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Sword") ||
           other.gameObject.CompareTag("Bullet"))
        {
            _anim.SetInteger("GiveDamage", UnityEngine.Random.Range(1, 5));

            _nowHP -= UnityEngine.Random.Range(1, 2);
            hpSlider.value = _nowHP / _maxHp;

            Debug.Log("HP" + hpSlider.value + "--" + _nowHP);

            if (_nowHP == 0)
            {
                Destroy(this.gameObject , 0f);
            }


        }
    }

   
}
