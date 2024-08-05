using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float detectionRange = 5.0f; // 플레이어 감지 범위
    public float speed = 2.0f; // 적 이동 속도
    public Vector3 initialPosition; // 초기 위치 저장 변수

    private SpriteRenderer spriteRenderer; // SpriteRenderer 컴포넌트

    private void Awake()
    {
        // SpriteRenderer 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        // 오브젝트가 활성화될 때 초기 위치로 이동
        transform.position = initialPosition;
        if (GameManager.Instance != null && GameManager.Instance.player != null)
        {
            player = GameManager.Instance.player.transform;
        }
        spriteRenderer.flipX = true;  //활성화할 때, 오른쪽을 보고있게 좌우반전을 한다.
    }
    private void FixedUpdate()
    {
        Vector2 direction = player.position - transform.position;
        float distanceToPlayer = direction.magnitude; //플레이어와 적의 거리

        if (distanceToPlayer < detectionRange)   //거리가 지정된 범위보다 가까우면 플레이어를 향해 달려간다.
        {
            spriteRenderer.flipX = false;

            Vector2 moveDir = direction.normalized * speed * Time.deltaTime;
            transform.position = (Vector2)transform.position + moveDir;
        }
    }
}