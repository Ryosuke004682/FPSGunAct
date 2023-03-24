using Sound;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class StageChange : MonoBehaviour
{
    [SerializeField, Header("画面を歪ませるコライダー")] private Collider screenCollider;

    [SerializeField] private Volume volume;
    private LensDistortion distortion;//歪ませるやつ
    
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

    private IEnumerator SubTime(float duration)
    {
        var startTime = source.volume;
        var endTime = 0.05f;
        var t = 0.0f;

        while (t < duration)
        {
            t += Time.time;
            var time = t / duration;

            var volume = Mathf.Lerp(startTime , endTime , time);
            source.volume = Mathf.Lerp(Mathf.Epsilon, volume, volume);

            yield return null;
        }
        source.volume = endTime;

    }

    private IEnumerator AddTime(float duration)
    {
        var startTime = source.volume;
        var endTime = 0.2f;
        var t = 0.0f;

        while(t < duration)
        {
            t += Time.time;
            var time = t / duration;
            var volume = Mathf.Lerp(startTime, endTime, time);
            source.volume = Mathf.Lerp(Mathf.Epsilon, volume, volume);

            yield return null;
        }

        source.volume = endTime;
    }

    private void ScreenEffect()
    {
        var value = Time.time;
        x_Mul = 1.0f - Mathf.PingPong(value , 1.0f);
        y_Mul = Mathf.PingPong(value , 1.0f);

        distortion.xMultiplier.Override(x_Mul);
        distortion.yMultiplier.Override(y_Mul);
        distortion.intensity.Override(lens_distortion);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            text.gameObject.SetActive(true);
            StartCoroutine(SubTime(3.0f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(AddTime(3.0f));
        }
    }

}
