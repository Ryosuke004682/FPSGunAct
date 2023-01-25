using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Damege : Damage_Master
{
    bool hit;

    private void Start()
    {
        
    }


    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Enemy2"))
        {
            hit = true;
            Debug.Log("ìñÇΩÇ¡ÇƒÇÈÇÊÅ`ÇÒ");
        }
    }

}
