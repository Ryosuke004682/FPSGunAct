using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Pod_Attack : MonoBehaviour
{
    

    [SerializeField, Header("�e�̎ˏo�X�s�[�h"),Tooltip("�e�̑��x")]
    protected float inJect = 10.0f;

    [Header("�|�b�h�ƒe������")]
    public GameObject Bullet;
    public GameObject ShotPoint;

    private float time = 0;

    [SerializeField ,Header("�ˌ��X�s�[�h")]
    private float reLoadTime = 0.08f;


    private void Start()
    {
        
    }

    private void Update()
    {
        PodAttack();
    }

    private void PodAttack()
    {
        if(Input.GetMouseButton(1))
        {
            Shot();
        }
    }

    private void Shot()
    {
        time += Time.deltaTime;

        if (time > reLoadTime)
        {

            var bulletPosition = ShotPoint.transform.position;
            var newBallet = Instantiate(Bullet, bulletPosition, transform.rotation);
//            var direction = newBallet.transform.forward;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPosition = ray.direction;

            newBallet.GetComponent<Rigidbody>().AddForce(rayPosition
                * inJect, ForceMode.Impulse);

            Destroy(newBallet, 1.0f);

            time = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }



}
