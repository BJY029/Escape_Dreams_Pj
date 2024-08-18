using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSourceForWalk;

    private void Start() // �����ϸ� 
    {
        Time.timeScale = 0f; //��� ����
        audioSourceForWalk.mute = true;
    }

    public void GameStart() //���۹�ư Ŭ����
    {
        Time.timeScale = 1.0f; // ���� ����
		audioSourceForWalk.mute = false;
	}

    public void GameQuit() //�����ư Ŭ����
    {
    #if UNITY_EDITOR //����Ƽ ȭ�鿡�� ����
            UnityEditor.EditorApplication.isPlaying = false;
    #else //�Ϲ� ����ÿ��� ����
                Application.Quit();
    #endif
    }
}
