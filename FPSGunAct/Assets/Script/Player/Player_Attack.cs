using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [RequireComponent(typeof(Animator))]
    public class Player_Attack : MonoBehaviour
    {

        Animator _anim;

        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            AnimAttack();
        }

        private void AnimAttack()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _anim.SetBool("Attack", true);
            }
            else
            {
                _anim.SetBool("Attack" , false);
            }
        }
    }
