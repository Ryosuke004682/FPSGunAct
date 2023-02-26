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
    [SerializeField] TMPro.TextMeshPro damageText;


    [SerializeField] private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();

        hpSlider.value = _maxHp;
        _nowHP = _maxHp;
        hpSlider.maxValue = _maxHp;
        hpSlider.value = _maxHp;

    }

    private void Update()
    {
        hpSlider.transform.rotation = Camera.main.transform.rotation;
        damageText.transform.rotation = Camera.main.transform.rotation;
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

            var damage = UnityEngine.Random.Range(1,3);
            damageText.text = damage.ToString();
            
            _nowHP -= damage;
            hpSlider.value = _nowHP;
            

            Debug.Log("HP" + hpSlider.value + "--" + _nowHP);

            if (_nowHP == 0)
            {
                Destroy(this.gameObject , 0f);
            }
            else if(_nowHP <= 0)
            {
                Destroy(this.gameObject , 0f);
            }


        }
    }

   
}
