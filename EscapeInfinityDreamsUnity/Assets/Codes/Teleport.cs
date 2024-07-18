using Cinemachine;
using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject targetObj;//�÷��̾�
    public GameObject toObj;//�̵��� ��
    public GameObject catObj;//����� 
    public CinemachineVirtualCamera thisRoomCamera; // ���� ���� ���� ī�޶�
    public CinemachineVirtualCamera nextRoomCamera; // ���� ���� ���� ī�޶�
    public Collider2D newConfiner; // ���� ���� Collider2D
    public float teleportCooldown = 0.5f; // �ڷ���Ʈ ��ٿ� �ð�

    private bool isTeleporting = false; // �ڷ���Ʈ �ߺ� ���� �÷���
    private bool canTeleport = false; // �ڷ���Ʈ ���� ���� �÷���

    private void OnTriggerEnter2D(Collider2D collision)//���� �浹 ����
    {
        if (collision.CompareTag("Player"))//�ε��� ����� �÷��̾����� Ȯ��
        {
            targetObj = collision.gameObject;//�÷��̾��� ���� ������Ʈ�� ����
            canTeleport = true; // �ڷ���Ʈ ���� ���� ����
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//������ �浹�� ����
    {
        if (collision.CompareTag("Player"))
        {
            canTeleport = false; // �ڷ���Ʈ ���� ���� ����
        }
    }

    private void Update()
    {
        // Ű �Է��� ó���Ͽ� �ڷ���Ʈ�� ����
        if (canTeleport && !isTeleporting && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TeleportRoutine());//�ڷ���Ʈ�� ����
        }
    }

    IEnumerator TeleportRoutine()
    {
        isTeleporting = true; // �ڷ���Ʈ ����

        targetObj.transform.position = toObj.transform.position; //�÷��̾� �̵�
        catObj.transform.position = toObj.transform.position; //����� �̵�

        yield return null; // �� ������ ���

        // ���� ���� ���� ī�޶� ��Ȱ��ȭ
        thisRoomCamera.Priority = 0;

        // ���� ���� ���� ī�޶� Ȱ��ȭ
        nextRoomCamera.Priority = 10;

        // ���� ī�޶��� ��� ������Ʈ
        CinemachineConfiner confiner = nextRoomCamera.GetComponent<CinemachineConfiner>();
        if (confiner != null)
        {
            confiner.m_BoundingShape2D = newConfiner;
            confiner.InvalidatePathCache(); // ĳ�� ��ȿȭ�Ͽ� ��� ������Ʈ
        }

        yield return new WaitForSeconds(teleportCooldown); // ��ٿ� �ð� ���� ���

        isTeleporting = false; // �ڷ���Ʈ ����
    }
}