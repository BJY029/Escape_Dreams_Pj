using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBeginGameManager : MonoBehaviour
{
    public DialogueManager dialogueManager;

    private void Start()
    {
        string[] introDialogue = new string[]
        {
            "(����)���� ������ �̷��� ���� ���� ����?",
            "�ӻ���� �˹�ġ�� �޿��� ������, ������ �־���",
            "�׷��� ���� �и� ������ ������ �� ��� ���̾�.",
            "(�㸧�� ������ ����) �� ��� �и��°� �ƴϰ���...?"
        };

        dialogueManager.StartDialogue(introDialogue);
    }
}
