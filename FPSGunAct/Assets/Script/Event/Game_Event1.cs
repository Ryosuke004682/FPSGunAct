using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Event
{
    public class Game_Event1 : MonoBehaviour
    {
        //Playerがコライダーに当たったら、Eventを発生させる。
        [SerializeField, Header("アニメーションの名前を入れるとこ")]
        private string _animName = "";

        [SerializeField, Header("トリガーのタグネームを入れるとこ")]
        private string _eventerTag = "";

        Animator _anim;

        //消したいオブジェクトたち
        public GameObject[] _deleteObj;

        private void Start()
        {
            _anim = GetComponent<Animator>();

            _anim.SetBool(_animName, false);

            //FirstStageを消す
            _deleteObj[0].SetActive(true);

            //Colliderを発生させる
            _deleteObj[1].SetActive(false);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == _eventerTag)
            {
                StartCoroutine(EventAction());
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == _eventerTag)
            {
                //Colliderを発生させる
                _deleteObj[1].SetActive(true);
            }
        }


        IEnumerator EventAction()
        {
            yield return new WaitForSeconds(3);
            _anim.SetBool(_animName, true);

            yield return new WaitForSeconds(6);
            //FirstStageを消す
            _deleteObj[0].SetActive(false);

        }
    }
}