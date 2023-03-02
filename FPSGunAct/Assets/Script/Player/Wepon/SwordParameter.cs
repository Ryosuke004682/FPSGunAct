using DG.Tweening;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordParameter : MonoBehaviour
{
    private void OnCollisionEnter(Collision  other)
    {
        if(other.gameObject.CompareTag("Enemy1") || other.gameObject.CompareTag("Enemy2"))
        {
            Debug.Log("‚ ‚ ‚ ‚ ‚ ‚ ‚ ‚ ‚ ‚ ‚ ‚ ‚ ‚ ");
        }
    }
}
