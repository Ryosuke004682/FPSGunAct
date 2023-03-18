using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Sound;
using System;

[RequireComponent(typeof(Animator))]
public class EnemyParameter : SwordParameter
{
    [SerializeField] public int _maxHp;
    [SerializeField] public int _nowHP;//明示的にインスペクタ上で現在のHPを見えるようにするために宣言。

    [SerializeField] public Slider hpSlider;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _clips;


    [SerializeField] private AudioSource _breackSource;
    [SerializeField] private AudioClip _breackClip;
    
    private int randomSE;
    
    public bool isLastKillMotion = false; //ラストキルを入れたい場合は、trueにする。

    void Start()
    {
        particle.Stop();

        _nowHP = _maxHp;
        hpSlider.maxValue = _maxHp;
        hpSlider.value = _maxHp;
    }

    private void Update()
    {
        hpSlider.transform.rotation = Camera.main.transform.rotation;
    }

    public void OnDestroy()
    {
        if (hpSlider.value == 0 && _nowHP == 0)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //attackParameter(other);

            Debug.Log("当たってるンゴーーーーーーー！！！！！！！！！！！！！！！！！！");

            var damage = UnityEngine.Random.Range(1, 5);

            hpSlider.value -= damage;
            _nowHP -= damage;

            hpSlider.value = Mathf.Max(hpSlider.value - damage, 0);
            _nowHP = Mathf.Max(_nowHP - damage, 0);

            HitStopContoroller.hitStop.Stop(_bulletHitStop);

            OnDestroy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            attackParameter(other);

            var damage = UnityEngine.Random.Range(SwordInstance.attackMin, SwordInstance.attackMax);//ダメージはSwordParameterから変更可能


            hpSlider.value -= damage;
            _nowHP -= damage;

            hpSlider.value = Mathf.Max(hpSlider.value - damage, 0);
            _nowHP = Mathf.Max(_nowHP - damage, 0);

            randomSE = UnityEngine.Random.Range(0, _clips.Length);
            _source.PlayOneShot(_clips[randomSE]);


            var cameraRotation = Camera.main.transform.forward;

            Quaternion rotation = Quaternion.LookRotation(cameraRotation, Vector3.up);
            Quaternion randomRotation = Quaternion.Euler(0, 0, (UnityEngine.Random.Range(0, 360)));

            particle.transform.position = this.transform.position;
            particle.transform.rotation = randomRotation * rotation;


            HitStopContoroller.hitStop.Stop(_swordHitTime);
            particle.Play();

            OnDestroy();
        }
    }
}
