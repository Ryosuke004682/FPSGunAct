using Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Transition : MonoBehaviour
{
    [SerializeField, Header("画面を歪ませる")] private Collider screenEffect;

    [SerializeField, Header("ワープのリクエストを送るとこ")] private Collider warpCollider;

    SoundManager sound = new SoundManager();

    private void Start()
    {
        
    }

    private void Update()
   {
        
   }

    private void ScreenEffect()
    { 
    }


}
