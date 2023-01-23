using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

namespace Event
{
    public class Game_Event0 : MonoBehaviour
    {
        [SerializeField, Header("敵を格納")]
        private GameObject[] enemyObject;

        //First
        [SerializeField, Header("アニメーターのパラメータを入れるとこ")]
        private string stateParameterName = "";

        Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
            anim.SetBool(stateParameterName, false);
        }

        private void Update()
        {

        }

        private void FixedUpdate()
        {
            StartCoroutine(Event());
        }

        public IEnumerator Event()
        {
            yield return null;

            enemyObject = GameObject.FindGameObjectsWithTag("Enemy");
            Debug.Log(enemyObject.Length);


            if (enemyObject.Length == 0)
            {
                anim.SetBool(stateParameterName, true);
            }
        }
    }

}
