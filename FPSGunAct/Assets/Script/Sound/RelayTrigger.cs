using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class RelayTrigger : MonoBehaviour
{
    private SoundManager ft_SE;

    private void Awake()
    {
        ft_SE = transform.root.gameObject.GetComponent<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ft_SE.RelayedTrigger(other);
    }

}