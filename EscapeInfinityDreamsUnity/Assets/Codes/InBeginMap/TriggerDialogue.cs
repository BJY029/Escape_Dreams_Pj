using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager; // DialogueManager�� ������ �ʵ�
    public string[] dialogueLines; // ����� ����/���
    public Vector3 triggerPosition; // ��縦 ����� ��ġ

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ��ü�� �÷��̾����� Ȯ��
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // �ߺ� Ʈ���� ����
            dialogueManager.StartDialogue(dialogueLines);  //���� ����
        }
    }
}
