using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager; // DialogueManager를 연결할 필드
    public string[] dialogueLines; // 출력할 독백/대사
    public Vector3 triggerPosition; // 대사를 출력할 위치

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체가 플레이어인지 확인
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // 중복 트리거 방지
            dialogueManager.StartDialogue(dialogueLines);  //독백 시작
        }
    }
}
