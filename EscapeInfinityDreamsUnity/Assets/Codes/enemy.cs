using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public float detectionRange = 5.0f; // �÷��̾� ���� ����
    public float speed = 2.0f; // �� �̵� �ӵ�
    public Vector3 initialPosition; // �ʱ� ��ġ ���� ����

    private SpriteRenderer spriteRenderer; // SpriteRenderer ������Ʈ

    private void Awake()
    {
        // SpriteRenderer ������Ʈ ��������
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        // ������Ʈ�� Ȱ��ȭ�� �� �ʱ� ��ġ�� �̵�
        transform.position = initialPosition;
        if (GameManager.Instance != null && GameManager.Instance.player != null)
        {
            player = GameManager.Instance.player.transform;
        }
        spriteRenderer.flipX = true;  //Ȱ��ȭ�� ��, �������� �����ְ� �¿������ �Ѵ�.
    }
    private void FixedUpdate()
    {
        Vector2 direction = player.position - transform.position;
        float distanceToPlayer = direction.magnitude; //�÷��̾�� ���� �Ÿ�

        if (distanceToPlayer < detectionRange)   //�Ÿ��� ������ �������� ������ �÷��̾ ���� �޷�����.
        {
            spriteRenderer.flipX = false;

            Vector2 moveDir = direction.normalized * speed * Time.deltaTime;
            transform.position = (Vector2)transform.position + moveDir;
        }
    }
}