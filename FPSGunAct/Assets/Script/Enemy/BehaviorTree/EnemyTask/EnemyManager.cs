using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    private int _health;

    private void Awake()
    {
        _health = 50;
    }

    public bool TaskHit()
    {
        _health -= 10;
 
        bool isDeath = _health <= 0;

        Debug.Log("�������蓖�����Ă��B");

        if (isDeath)
        {
            Did();
        }
        
        Debug.Log("���S�m�F");
        return isDeath;
    }

    private void Did()
    {
        Debug.Log("���j�I�I");
        //���S�G�t�F�N�g�̎��Ԃ���������0.1�b��B
        Destroy(this.gameObject , 0.1f);

        //���S���̃G�t�F�N�g��

    }
}
