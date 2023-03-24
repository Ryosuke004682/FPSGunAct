using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJump : MonoBehaviour
{
    //DontDestroyOnLoad()�ŏ������ɃX�|�[�������邱�Ƃ��ł���
    /*�X�|�[�������Ƃ��ɔC�ӂ̃|�W�V�����ɃX�|�[�����Ăق����B*/

    [SerializeField, Header("�X�e�[�W�J�ڌ���A�X�e�[�^�X��ێ����������")] 
    private GameObject[] saveObjects;

    [SerializeField, Header("�X�|�[���̈ʒu")]
    private GameObject playerSpawnPosition;


    public void Start()
    {
        for (var i = 0; i < saveObjects.Length; i++)
        {
            DontDestroyOnLoad(saveObjects[i]);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Warp();
        }
    }

    private void Warp()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("MainScene");
            StartCoroutine(Spawn());
        }
    }

    private IEnumerator Spawn()
    {
        playerSpawnPosition = GameObject.Find("SpawnObject");
        UnityEngine.Debug.Log("(�R���[�`��)�T���Ă��" + playerSpawnPosition);

        for(var i = 0; i < saveObjects.Length; i++)
        {
            playerSpawnPosition = saveObjects[i];
        }

        yield return null;
    }
}
