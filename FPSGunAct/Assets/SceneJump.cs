using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJump : MonoBehaviour
{
    //DontDestroyOnLoad()で消さずにスポーンさせることができる
    /*スポーンしたときに任意のポジションにスポーンしてほしい。*/

    [SerializeField, Header("ステージ遷移後も、ステータスを保持したいやつ")] 
    private GameObject[] saveObjects;

    [SerializeField, Header("スポーンの位置")]
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
        UnityEngine.Debug.Log("(コルーチン)探せてるよ" + playerSpawnPosition);

        for(var i = 0; i < saveObjects.Length; i++)
        {
            playerSpawnPosition = saveObjects[i];
        }

        yield return null;
    }
}
