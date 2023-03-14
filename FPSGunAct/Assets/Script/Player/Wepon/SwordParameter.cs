using DG.Tweening;
using Player;
using UnityEngine.UI;
using UnityEngine;


public class SwordParameter : MonoBehaviour
{
    [SerializeField, Tooltip("�v���C���[�̃q�b�g�X�g�b�v�̎���")]
    public float _swordHitTime = 0.8f;

    [SerializeField, Tooltip("�e�������������̃q�b�g�X�g�b�v")]
    private float _bulletHitStop = 0.02f;

    [SerializeField] public  ParticleSystem particle;

    [SerializeField] public int attackMax = 100;
    [SerializeField] public int attackMin = 10;


    public static SwordParameter SwordInstance;
    public void Awake()
    {
        if(SwordInstance == null)
        {
            SwordInstance = this;
        }
    }


    private void Start()
    {
       
    }

    private void Update()
    {
     
    }

    public void attackParameter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy1") || other.gameObject.CompareTag("Enemy2"))
        {
         
        }
    }
}
