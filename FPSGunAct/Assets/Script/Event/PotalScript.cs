﻿using Sound;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PotalScript : MonoBehaviour
{
    [SerializeField, Header("��ʂ�c�܂���R���C�_�[")] private Collider screenCollider;

    [SerializeField] private Volume volume;
    private LensDistortion distortion;//�c�܂�����

    public float lens_distortion = -0.5f;
    public float x_Mul = 1.0f;
    public float y_Mul = 0.0f;


    public AudioClip clip;
    public AudioSource source;


    public TextMeshProUGUI text;
    public float textColorChange = 3.0f;
    private float t;

    SoundManager sound = new SoundManager();


    private void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out distortion);

        text.gameObject.SetActive(false);

    }

    private void Update()
    {
        ScreenEffect();
        text.transform.rotation = Camera.main.transform.rotation;

        t += Time.deltaTime * textColorChange;
        Color color = Color.HSVToRGB(t % 1.0f, 1.0f, 1.0f);
        text.color = color;
    }

    private void ScreenEffect()
    {
        var value = Time.time;
        x_Mul = 1.0f - Mathf.PingPong(value, 1.0f);
        y_Mul = Mathf.PingPong(value, 1.0f);

        distortion.xMultiplier.Override(x_Mul);
        distortion.yMultiplier.Override(y_Mul);
        distortion.intensity.Override(lens_distortion);
    }


    private IEnumerator SubBGMVolume(float duration)
    {
        var startVolume = source.volume;
        var endVolume = 0.05f;
        var startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            var time = (Time.time - startTime) / duration;
            source.volume = Mathf.Lerp(startVolume, endVolume, time);
            yield return null;
        }
        source.volume = endVolume;
    }

    private IEnumerator AddBGMVolume(float duration)
    {
        var startVolume = source.volume;
        var endVolume = 0.2f;
        var startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            var time = (Time.time - startTime) / duration;
            source.volume = Mathf.Lerp(startVolume, endVolume, time);
            yield return null;
        }
        source.volume = endVolume;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.gameObject.SetActive(true);
            StartCoroutine(SubBGMVolume(3.0f));
        }
    }
}