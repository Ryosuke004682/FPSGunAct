using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FootSE : MonoBehaviour
{
    private PlayerSE se;

    private void Awake()
    {
        se = transform.root.gameObject.GetComponent<PlayerSE>();
    }

    private void OnTriggerEnter(Collider other)
    {
        se.PlaySE_Trigger(other);
    }

    private void Update()
    {
        
    }
}
