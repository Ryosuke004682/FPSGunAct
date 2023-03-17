using Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

namespace Event
{
    public class Game_Event0 : SoundManager
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

            StartCoroutine(FirstEvent());
        }

        public IEnumerator FirstEvent()
        {
            enemyObject = GameObject.FindGameObjectsWithTag("Enemy1");

            yield return null;

            while(enemyObject.Length > 0)
            {

                GameObject enemyToMove = enemyObject[0];
                enemyObject = RemoveEnemyFromArray(enemyToMove, enemyObject);

                yield return null;
            }

          if(enemyObject.Length == 0)
            {
                anim.SetBool(stateParameterName, true);
                var thisSE = eventSE[0];
                audioSourceSE.PlayOneShot(thisSE);
            }
                
            
        }

        private GameObject[] RemoveEnemyFromArray(GameObject enemy,GameObject[] array)
        {
            return array.Where(x => x != enemy).ToArray();
        }
    }

}
