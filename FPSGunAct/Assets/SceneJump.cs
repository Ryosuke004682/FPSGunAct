using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJump : MonoBehaviour
{
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
            SceneManager.LoadScene("CamingSoon");
        }
    }

}
