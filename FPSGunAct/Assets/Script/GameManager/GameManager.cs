using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Text[] text;

    bool flag = false;

    private void Start()
    {
        
    }

    IEnumerator NextText()
    {
        text[0].text = "";
        yield return null;
        
    }

}
