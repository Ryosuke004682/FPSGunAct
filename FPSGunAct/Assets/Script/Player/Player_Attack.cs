using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attack
{
    [RequireComponent(typeof(Animator))]
    public class Player_Attack : MonoBehaviour
    {

        Animator _anim;

        private void Start()
        {
            _anim = GetComponent<Animator>();
            //_anim.SetBool("Attack", false);
        }

        private void Update()
        {
            AnimAttack();
        }

        private void AnimAttack()
        {
            if (Input.GetMouseButton(0))
            {
                _anim.SetBool("Attack", true);
            }
            else
            {
                _anim.SetBool("Attack" , false);
            }
        }

    }
}
