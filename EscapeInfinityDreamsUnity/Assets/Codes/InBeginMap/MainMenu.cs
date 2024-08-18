using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSourceForWalk;

    public SpriteRenderer spriteRenderer;
    public Button targetButton;

    public float startAlpha = 0.8f;
    public float endAlpha = 0f;
    public float duration = 2f;

    private void Start() // �����ϸ� 
    {
        Time.timeScale = 0f; //��� ����
        audioSourceForWalk.mute = true;
    }

    public void GameStart() //���۹�ư Ŭ����
    {
        Time.timeScale = 1.0f; // ���� ����
        
        RectTransform rectTransform = targetButton.GetComponent<RectTransform>();
        rectTransform.sizeDelta = Vector2.zero;

		audioSourceForWalk.mute = false;
        StartCoroutine(FadeSprite());
     }

    public void GameQuit() //�����ư Ŭ����
    {
    #if UNITY_EDITOR //����Ƽ ȭ�鿡�� ����
            UnityEditor.EditorApplication.isPlaying = false;
    #else //�Ϲ� ����ÿ��� ����
                Application.Quit();
    #endif
    }

    public IEnumerator FadeSprite()
    {
        float elapsedTime = 0f;
        Color color = spriteRenderer.color;

        while(elapsedTime<duration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);

            color.a = newAlpha;
            spriteRenderer.color = color;

            elapsedTime += Time.deltaTime;


			yield return null;
        }

        color.a = endAlpha;
        spriteRenderer.color = color;
    }
}
