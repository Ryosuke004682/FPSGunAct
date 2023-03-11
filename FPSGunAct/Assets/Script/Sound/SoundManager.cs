using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]

    public class SoundManager : MonoBehaviour
    {
        public AudioSource audioSourceBGM;
        public AudioSource audioSourceSE;
        public AudioSource audioSourceEssentials;

        public AudioClip[] bgmlist;

        public float low = 0.95f;
        public float high = 1.05f;

        protected int randomSound;

        public enum BGM
        {
            tutorial,
            MainScene,
            Boss
        };

        public AudioClip[] seList;

        public enum SE
        {
            Attack1,
            Attack2,
            Attack3,
            Attack4,
            Attack5,
            Attack6,
            Attack7,
            Attack8,
            Attack9,
            shooting
        };


        public static SoundManager SoundInstance;
        public void Awake()
        {
            if (SoundInstance == null)
            {
                SoundInstance = this;
            }
        }
    }
}