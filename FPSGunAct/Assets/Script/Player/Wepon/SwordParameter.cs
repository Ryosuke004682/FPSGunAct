using DG.Tweening;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordParameter : MonoBehaviour
{
    private void OnCollisionEnter(Collision  other)
    {
        //UŒ‚‚ª“–‚½‚Á‚Ä‚é‚©‚Ç‚¤‚©
        if (other.gameObject.CompareTag("Enemy1") || other.gameObject.CompareTag("Enemy2"))
        {
            Debug.Log("UŒ‚‚ª“–‚½‚Á‚Ä‚é‚æ");
        }
    }
}
