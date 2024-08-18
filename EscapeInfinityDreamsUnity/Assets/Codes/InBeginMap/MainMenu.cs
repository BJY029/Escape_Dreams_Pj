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

    private void Start() // 시작하면 
    {
        Time.timeScale = 0f; //모두 멈춤
        audioSourceForWalk.mute = true;
    }

    public void GameStart() //시작버튼 클릭시
    {
        Time.timeScale = 1.0f; // 원상 복구
        
        RectTransform rectTransform = targetButton.GetComponent<RectTransform>();
        rectTransform.sizeDelta = Vector2.zero;

		audioSourceForWalk.mute = false;
        StartCoroutine(FadeSprite());
     }

    public void GameQuit() //종료버튼 클릭시
    {
    #if UNITY_EDITOR //유니티 화면에서 종료
            UnityEditor.EditorApplication.isPlaying = false;
    #else //일반 실행시에서 종료
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
