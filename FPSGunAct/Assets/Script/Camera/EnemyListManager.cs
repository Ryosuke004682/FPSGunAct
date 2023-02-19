using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyListManager : MonoBehaviour
{
    public List<Transform> enemyList = new List<Transform>();

    private void Update()
    {
        for(var i = 0; i < enemyList.Count; i++)
        {
            for(var j = i + 1; j < enemyList.Count; j++)
            {
                if (enemyList[i] == enemyList[j])
                {
                    enemyList.RemoveAt(j);
                }
            }
            //missing‚É‚È‚Á‚Ä‚é‚â‚Â‚ðÁ‚·B
            if (!enemyList[i])
            {
                enemyList.RemoveAt(i);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy1") || other.CompareTag("Enemy2"))
        {
            enemyList.Add(other.gameObject.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy1") || other.gameObject.CompareTag("Enemy2"))
        {
            for(var i = 0; i < enemyList.Count; i++)
            {
                enemyList.RemoveAt(i);
            }
        }
    }

}
