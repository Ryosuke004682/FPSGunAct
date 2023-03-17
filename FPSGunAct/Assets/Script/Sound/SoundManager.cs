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

        public AudioClip[] attackSE;

        public enum AttackSE
        {
            Pod_Shooting,
            Attack1,
            Attack2
        };

        public AudioClip[] breackSE;

        public enum Breack
        {
            BreackSE
        };

        public AudioClip[] eventSE;

        public enum GimicEvent
        { 
           EventSound1,
           EventSound2,
           EventSound3,
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