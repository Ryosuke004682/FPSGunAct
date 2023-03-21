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
        // �e�L�X�g���\���ɂ���
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);

        // �V�[�������[�h���ꂽ��FadeIn���J�n����
        SceneManager.sceneLoaded += FadeIn;
    }

    private void FadeIn(Scene scene, LoadSceneMode mode)
    {
        // FadeIn�p�̃R���[�`�����J�n����
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

        // �Ō��alpha��1�ɐݒ肵�A���S�ɕ\������
        text.color = new Color(text.color.r, text.color.g, text.color.b, 100f);
    }
}
