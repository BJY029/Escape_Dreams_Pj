using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    private void Start() // �����ϸ� 
    {
        Time.timeScale = 0f; //��� ����
    }

    public void GameStart() //���۹�ư Ŭ����
    {
        Time.timeScale = 1.0f; // ���� ����

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
