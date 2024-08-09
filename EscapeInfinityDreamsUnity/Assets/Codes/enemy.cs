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
    private Animator animator; // Animator ������Ʈ
    private playerAnimationController playerAnimationController; // �÷��̾� �ִϸ��̼� ��Ʈ�ѷ�

    private void Awake()
    {
        // SpriteRenderer ������Ʈ�� Animator ������Ʈ ��������
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerAnimationController = player.GetComponent<playerAnimationController>();
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
        animator.SetBool("IsRunning", false); // �ʱ� ���´� �޸��� ����
    }
    private void FixedUpdate()
    {
        if (playerAnimationController.playerDeadCoroutine == true)
        {
            // �÷��̾ ��� ���¶�� ���� ����
            animator.SetBool("IsRunning", false); // �޸��� �ִϸ��̼� ����
            return;
        }

        Vector2 direction = player.position - transform.position;
        float distanceToPlayer = direction.magnitude; //�÷��̾�� ���� �Ÿ�

        if (distanceToPlayer < detectionRange)   //�Ÿ��� ������ �������� ������ �÷��̾ ���� �޷�����.
        {
            spriteRenderer.flipX = false;

            Vector2 moveDir = direction.normalized * speed * Time.deltaTime;
            transform.position = (Vector2)transform.position + moveDir;
            animator.SetBool("IsRunning", true); // �޸��� �ִϸ��̼� ����
        }
    }
}