using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventAnimationSE_ : MonoBehaviour
{
    [SerializeField, Header("ƒCƒxƒ“ƒg‚ÌSE")] private AudioClip[] clips;
    [SerializeField, Header("AudioSource")] private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void EventSE()
    {
        source.Play();
    }



}
