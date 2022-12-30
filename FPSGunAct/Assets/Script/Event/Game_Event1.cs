using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Event
{
    public class Game_Event1 : MonoBehaviour
    {
        //Player���R���C�_�[�ɓ���������AEvent�𔭐�������B
        [SerializeField, Header("�A�j���[�V�����̖��O������Ƃ�")]
        private string _animName = "";

        [SerializeField, Header("�g���K�[�̃^�O�l�[��������Ƃ�")]
        private string _eventerTag = "";

        Animator _anim;

        //���������I�u�W�F�N�g����
        public GameObject[] _deleteObj;

        private void Start()
        {
            _anim = GetComponent<Animator>();

            _anim.SetBool(_animName, false);

            //FirstStage������
            _deleteObj[0].SetActive(true);

            //Collider�𔭐�������
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
                //Collider�𔭐�������
                _deleteObj[1].SetActive(true);
            }
        }


        IEnumerator EventAction()
        {
            yield return new WaitForSeconds(3);
            _anim.SetBool(_animName, true);

            yield return new WaitForSeconds(6);
            //FirstStage������
            _deleteObj[0].SetActive(false);

        }
    }
}