using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class RelayTrigger : MonoBehaviour
{
    private WalkingSE ft_SE;

    private void Awake()
    {
        ft_SE = transform.root.gameObject.GetComponent<WalkingSE>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ft_SE.RelayedTrigger(other);
    }

}