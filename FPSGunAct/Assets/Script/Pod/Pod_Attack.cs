using Cinemachine;
using Player;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Pod_Attack : MonoBehaviour
{
    [SerializeField, Header("弾の射出スピード"),Tooltip("弾の速度")]
    protected float inJect = 10.0f;

    [Header("ポッドと弾を入れる")]
    public GameObject Bullet;
    public GameObject ShotPoint;

    private float time = 0;

    [SerializeField ,Header("射撃スピード")]
    private float reLoadTime = 0.08f;

    Quaternion rotation;


    private void Start()
    {
        
    }

    private void Update()
    {
        PodAttack();

        rotation = Quaternion.Lerp(transform.rotation, rotation , Time.deltaTime );
    }
    public void PodAttack()
    {
        if (Input.GetMouseButton(1))
        {
            Shot();

        }
    }

        public void Shot()
        {
            time += Time.deltaTime;

            if (time > reLoadTime)
            {

                var bulletPosition = ShotPoint.transform.position;
                var newBallet = Instantiate(Bullet, bulletPosition, transform.rotation);
                //var direction = newBallet.transform.forward;

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


        }
}
