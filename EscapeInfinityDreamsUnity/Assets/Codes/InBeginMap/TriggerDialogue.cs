using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager; // DialogueManager�� ������ �ʵ�
    public string[] dialogueLines; // ����� ����/���
    public Transform player; // �÷��̾��� Transform
    public Vector3 triggerPosition; // ��縦 ����� ��ġ
    public float triggerRange = 1.0f; // ��ġ ����

    private bool hasTriggered = false;

    private void Update()
    {
        // �÷��̾ Ʈ���� ���� ���� ���Դ��� Ȯ��
        float distance = Vector3.Distance(player.position, triggerPosition);
        if (distance < triggerRange && !hasTriggered)
        {
            hasTriggered = true; // �ߺ� Ʈ���� ����
            Debug.Log(dialogueLines);
            dialogueManager.StartDialogue(dialogueLines);
        }
    }
}
