using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class AudioClips
    {
        public string groundTypeTag;
        public AudioClip[] audioClips;
    }

    [SerializeField] List<AudioClips> listAudioClips = new List<AudioClips>();

    [SerializeField] string[] terrainLayerToTag;
    [SerializeField] float pitchRange = 0.1f;

    private Dictionary<string, int> tagToIndex = new Dictionary<string, int>();
    private int groundIndex = 0;
    private Terrain terrain;
    private TerrainData tData;

    protected AudioSource source;

    private void Awake()
    {
        source = GetComponents<AudioSource>()[0];

        for(var i = 0; i < listAudioClips.Count(); ++i)
        {
            tagToIndex.Add(listAudioClips[i].groundTypeTag,i);
        }

    }

    private void Start()
    {
        terrain = Terrain.activeTerrain;
        tData   = terrain.terrainData;
    }


    public void RelayedTrigger(Collider other)
    {
        if (tagToIndex.ContainsKey(other.gameObject.tag))
            groundIndex = tagToIndex[other.gameObject.tag];

        if(other.gameObject.GetInstanceID() == terrain.gameObject.GetInstanceID())
        {
            Vector3 position = transform.position - terrain.transform.position;
            var offsetX = (int)(tData.alphamapWidth * position.x / tData.size.x);
            var offsetY = (int)(tData.alphamapHeight * position.z / tData.size.z);

            float[,,] alpamaps = tData.GetAlphamaps(offsetX , offsetY , 1 , 1);

            float[] weights = alpamaps.Cast<float>().ToArray();
            var terrainLayer = System.Array.IndexOf(weights , weights.Max());
            groundIndex = tagToIndex[terrainLayerToTag[terrainLayer]];
        }
    }

    public void PlayFootstepSE()
    {
        AudioClip[] clips = listAudioClips[groundIndex].audioClips;

        source.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
        source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}
