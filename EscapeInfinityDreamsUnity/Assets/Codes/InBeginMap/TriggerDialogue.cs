using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager; // DialogueManager를 연결할 필드
    public string[] dialogueLines; // 출력할 독백/대사
    public Transform player; // 플레이어의 Transform
    public Vector3 triggerPosition; // 대사를 출력할 위치
    public float triggerRange = 1.0f; // 위치 범위

    private bool hasTriggered = false;

    private void Update()
    {
        // 플레이어가 트리거 범위 내에 들어왔는지 확인
        float distance = Vector3.Distance(player.position, triggerPosition);
        if (distance < triggerRange && !hasTriggered)
        {
            hasTriggered = true; // 중복 트리거 방지
            Debug.Log(dialogueLines);
            dialogueManager.StartDialogue(dialogueLines);
        }
    }
}
