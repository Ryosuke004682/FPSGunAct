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

    [SerializeField, Tooltip("プレイヤーのヒットストップの時間")]
    private float _hitStopTime = 0.2f;


    [SerializeField] Slider hpSlider;
    //[SerializeField] TMPro.TextMeshPro damageText;
    [SerializeField] private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();

        _nowHP = _maxHp;
        hpSlider.maxValue = _maxHp;
        hpSlider.value = _maxHp;

    }

    private void Update()
    {
        hpSlider.transform.rotation = Camera.main.transform.rotation;
        //damageText.transform.rotation = Camera.main.transform.rotation;
    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Sword") ||
           other.gameObject.CompareTag("Bullet"))
        {

            _anim.SetInteger("GiveDamage", UnityEngine.Random.Range(1, 5));
            var damage = UnityEngine.Random.Range(1,3);
           

            Debug.Log($"いまの" + damage);
            
            _nowHP -= damage;
            hpSlider.value = _nowHP;
            

            Debug.Log("HP" + hpSlider.value + "--" + _nowHP);

            if(_nowHP <= 0)
            {
                Destroy(this.gameObject , 0f);
            }
        }

        if(other.gameObject.CompareTag("Sword"))
        {
            Debug.Log("剣が当たってるよ");
            _anim.SetInteger("GiveDamage", UnityEngine.Random.Range(1, 5));
            var damage = UnityEngine.Random.Range(20 , 50);

            Debug.Log($"剣のダメージ量 = {damage}");
            _nowHP -= damage;
        }


    }
}
