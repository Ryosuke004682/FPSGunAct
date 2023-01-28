using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(AudioClip))]
public class PlayerSE : MonoBehaviour
{
    [Serializable]
    public class AudioClips
    {
        public string tagType;
        public AudioClip[] clips;
    }

    [SerializeField]
    private float pitchRange = 0.1f;

    [SerializeField]
    List<AudioClips> listAudioClips = new List<AudioClips>();

    private Dictionary<string, int> tagIndex = new Dictionary<string, int>();
    private int groundIndex = 0;

    public AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySE_Trigger(Collider other)
    {
        if(tagIndex.ContainsKey(other.gameObject.tag))
            groundIndex = tagIndex[other.gameObject.tag];
    }

    public void PlayerFootSE()
    {
        AudioClip[] clip = listAudioClips[groundIndex].clips;

        source.pitch = 1.0f + UnityEngine.Random.Range(-pitchRange , pitchRange);
        source.PlayOneShot(clip[UnityEngine.Random.Range(0, clip.Length)]);
    }


}
