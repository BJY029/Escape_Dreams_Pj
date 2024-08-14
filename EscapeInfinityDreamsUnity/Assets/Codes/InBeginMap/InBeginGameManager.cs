using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBeginGameManager : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject mainCanvas;

    private void Start()
    {
        Time.timeScale = 0f; //���۹�ư ������ ������ ����
    }
    

    public void GameStart()
    {
        Time.timeScale = 1f; //���۹�ư ������ ���� ����
        
        //���� �� ������ �־�� �ڿ��������

        string[] introDialogue = new string[]
        {
            "(����)���� ������ �̷��� ���� ���� ����?",
            "�ӻ���� �˹�ġ�� �޿��� ������, ������ �־���",
            "�׷��� ���� �и� ������ ������ �� ��� ���̾�.",
            "(�㸧�� ������ ����) �� ��� �и��°� �ƴϰ���...?"
        };

        dialogueManager.StartDialogue(introDialogue);
    }

    public void GameExit() //QUit ��ư�� ������ �� ���� ����
    {
    #if UNITY_EDITOR // ����Ƽ �����Ϳ���
            UnityEditor.EditorApplication.isPlaying = false;
    #else //���� ���� ��ũ��Ʈ
                Application.Quit();
    #endif
    }

    public void OnSettingBtn()
    {

    }
}
