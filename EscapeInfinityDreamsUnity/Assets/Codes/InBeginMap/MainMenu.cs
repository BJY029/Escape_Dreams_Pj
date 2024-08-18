using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSourceForWalk;

    private void Start() // 시작하면 
    {
        Time.timeScale = 0f; //모두 멈춤
        audioSourceForWalk.mute = true;
    }

    public void GameStart() //시작버튼 클릭시
    {
        Time.timeScale = 1.0f; // 원상 복구
		audioSourceForWalk.mute = false;
	}

    public void GameQuit() //종료버튼 클릭시
    {
    #if UNITY_EDITOR //유니티 화면에서 종료
            UnityEditor.EditorApplication.isPlaying = false;
    #else //일반 실행시에서 종료
                Application.Quit();
    #endif
    }
}
