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
    private float _swordHitTime = 0.8f;

    [SerializeField, Tooltip("弾が当たった時のヒットストップ")]
    private float _bulletHitStop = 0.02f;




    [SerializeField] Slider hpSlider;
    //[SerializeField] TMPro.TextMeshPro damageText;
    [SerializeField] private Animator _anim;

    [SerializeField] ParticleSystem particle;


    void Start()
    {
        _anim = GetComponent<Animator>();

        _nowHP = _maxHp;
        hpSlider.maxValue = _maxHp;
        hpSlider.value = _maxHp;

        particle.Stop();
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


        if (other.gameObject.CompareTag("Sword"))
        {
            Debug.Log("剣が当たってるよ");
            _anim.SetInteger("GiveDamage", UnityEngine.Random.Range(1, 5));
            var damage = UnityEngine.Random.Range(20, 50);

            Debug.Log($"剣のダメージ量 = {damage}");
            _nowHP -= damage;
            hpSlider.value = _nowHP;

            var cameraRotation = Camera.main.transform.forward;

            Quaternion rotation = Quaternion.LookRotation(cameraRotation, Vector3.up);
            Quaternion randomRotation = Quaternion.Euler(0, 180f, UnityEngine.Random.Range(0, 360));

            particle.transform.position = this.transform.position;
            particle.transform.rotation = randomRotation * rotation;

            Debug.Log("andomRotation" + randomRotation);


            HitStopContoroller.hitStop.Stop(_swordHitTime);
            particle.Play();
            Debug.Log(particle + "再生されてるよ");

            if (_nowHP <= 0)
            {
                Destroy(this.gameObject, 0f);
            }
        }


        if (other.gameObject.CompareTag("Bullet"))
        {
            HitStopContoroller.hitStop.Stop(_bulletHitStop);

            _anim.SetInteger("GiveDamage", UnityEngine.Random.Range(1, 5));
            var damage = UnityEngine.Random.Range(1,3);
            
            
            _nowHP -= damage;
            hpSlider.value = _nowHP;
            

            Debug.Log("HP" + hpSlider.value + "--" + _nowHP);

            if(_nowHP <= 0)
            {
                Destroy(this.gameObject , 0f);
            } 
        }
    }
}
