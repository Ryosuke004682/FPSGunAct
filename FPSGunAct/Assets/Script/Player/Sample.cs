using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Sample : MonoBehaviour
{
    public CinemachineVirtualCamera main;
    public CinemachineVirtualCamera firstjump;


    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            main.Priority = 19;
        }
        else
        {
            main.Priority = 20;
        }
    }

}
