using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventAnimationSE_ : MonoBehaviour
{
    [SerializeField, Header("�C�x���g��SE")] private AudioClip[] clips;
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
