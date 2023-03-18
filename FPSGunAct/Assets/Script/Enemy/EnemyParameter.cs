using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Sound;
using System;

[RequireComponent(typeof(Animator))]
public class EnemyParameter : SwordParameter
{
    [SerializeField] public int _maxHp;
    [SerializeField] public int _nowHP;//�����I�ɃC���X�y�N�^��Ō��݂�HP��������悤�ɂ��邽�߂ɐ錾�B

    [SerializeField] public Slider hpSlider;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _clips;


    [SerializeField] private AudioSource _breackSource;
    [SerializeField] private AudioClip _breackClip;
    
    private int randomSE;
    
    public bool isLastKillMotion = false; //���X�g�L������ꂽ���ꍇ�́Atrue�ɂ���B

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

            Debug.Log("�������Ă郓�S�[�[�[�[�[�[�[�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");

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

            var damage = UnityEngine.Random.Range(SwordInstance.attackMin, SwordInstance.attackMax);//�_���[�W��SwordParameter����ύX�\


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
