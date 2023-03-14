using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Sound;

[RequireComponent(typeof(Animator))]
public class EnemyParameter : SwordParameter
{
    [SerializeField] private int _maxHp;
    [SerializeField] private int _nowHP;//�����I�Ɍ��݂�HP��������悤�ɂ��邽�߂ɐ錾�B

    [SerializeField] Slider hpSlider;
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

    private void FixedUpdate()
    {
        
    }

    IEnumerator EventStart()
    {
        yield return new WaitForSeconds(10.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�������Ă���");
        if (other.gameObject.CompareTag("Sword"))
        {
            attackParameter(other);

            var damage = UnityEngine.Random.Range(SwordInstance.attackMin, SwordInstance.attackMax);//�_���[�W��SwordParameter����ύX�\

            hpSlider.value -= damage;
            _nowHP -= damage;


            randomSE = Random.Range( 0 , _clips.Length);
            _source.PlayOneShot(_clips[randomSE]);
            

            var cameraRotation = Camera.main.transform.forward;

            Quaternion rotation = Quaternion.LookRotation(cameraRotation, Vector3.up);
            Quaternion randomRotation = Quaternion.Euler(0, 180f, (Random.Range(0, 360)));

            particle.transform.position = other.transform.position;
            particle.transform.rotation = randomRotation * rotation;

            HitStopContoroller.hitStop.Stop(_swordHitTime);
            particle.Play();


            if (hpSlider.value <= 0 && isLastKillMotion == true)
            {

                _nowHP = 1;
                hpSlider.value = 1;

                _breackSource.PlayOneShot(_breackClip);

                Debug.Log($"{_breackClip}�@���Đ�����Ă��");

                StartCoroutine(EventStart());
                Destroy(other.gameObject);
            }
        }
    }
}
