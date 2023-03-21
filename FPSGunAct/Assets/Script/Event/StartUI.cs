using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    public Text text;
    public float fadeInTime = 2f;

    private void Start()
    {
        // テキストを非表示にする
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);

        // シーンがロードされたらFadeInを開始する
        SceneManager.sceneLoaded += FadeIn;
    }

    private void FadeIn(Scene scene, LoadSceneMode mode)
    {
        // FadeIn用のコルーチンを開始する
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeInTime)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInTime);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            elapsedTime += Time.time;
            yield return null;
        }

        // 最後にalphaを1に設定し、完全に表示する
        text.color = new Color(text.color.r, text.color.g, text.color.b, 100f);
    }
}
