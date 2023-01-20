using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Pod_Manager : MonoBehaviour
{
   
    Animator _anim;
    

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        PodAnim();
        
    }

    private void PodAnim()
    {
        if(Input.GetMouseButton(1))
        {
            _anim.SetBool("Attack",true);
        }
        else
        {
            _anim.SetBool("Attack" , false);
        }
    }
}
