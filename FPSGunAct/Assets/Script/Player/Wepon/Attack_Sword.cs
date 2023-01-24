using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Attack_Sword : MonoBehaviour
{
    Rigidbody rb;
    
    public Collider attackCollider;

    bool isHit;


    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void OffSetCollider()
    {
        attackCollider.enabled = false;
    }

    public void OnSetCollider()
    {
        attackCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Enemy2") && isHit == false)
        {
            isHit = true;

            Debug.Log("ìñÇΩÇ¡ÇƒÇÈÇÊÅ`ÇÒ");
        }
    }

}
