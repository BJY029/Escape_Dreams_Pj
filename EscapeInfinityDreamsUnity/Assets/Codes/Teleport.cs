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

    public GameObject interactionUI; // ����� ������ UI ������Ʈ(text_background)
    public Vector3 uiOffset; // UI ������Ʈ�� ��� ��ġ ����

    private void Start()
    {
        interactionUI.SetActive(false); //������ ��, �ؽ�Ʈ�� ����
    }

    private void OnTriggerEnter2D(Collider2D collision)//���� �浹 ����
    {
        if (collision.CompareTag("Player"))//�ε��� ����� �÷��̾����� Ȯ��
        {
            targetObj = collision.gameObject;//�÷��̾��� ���� ������Ʈ�� ����
            canTeleport = true; // �ڷ���Ʈ ���� ���� ����
            interactionUI.SetActive(true); // �ؽ�Ʈ�� ���̰� ��
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//������ �浹�� ����
    {
        if (collision.CompareTag("Player"))
        {
            canTeleport = false; // �ڷ���Ʈ ���� ���� ����
            interactionUI.SetActive(false); // �ؽ�Ʈ�� ����
        }
    }

    private void Update()
    {
        // Ű �Է��� ó���Ͽ� �ڷ���Ʈ�� ����
        if (canTeleport && !isTeleporting && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TeleportRoutine());//�ڷ���Ʈ�� ����
        }
        // �÷��̾� �������� UI ��ġ ������Ʈ
        if (canTeleport)
        {
            interactionUI.transform.position = targetObj.transform.position + uiOffset;
        }
    }

    IEnumerator TeleportRoutine()
    {
        isTeleporting = true; // �ڷ���Ʈ ����

		// ���� ���� ���� ī�޶� ��Ȱ��ȭ
		thisRoomCamera.Priority = 0;

		targetObj.transform.position = toObj.transform.position; //�÷��̾� �̵�
        catObj.transform.position = new Vector3(toObj.transform.position.x, toObj.transform.position.y - 1f, toObj.transform.position.z); //����� �̵�

        yield return new WaitForEndOfFrame(); // �� ������ ���

        

        // ���� ���� ���� ī�޶� Ȱ��ȭ
        nextRoomCamera.Priority = 1;

        // ���� ī�޶��� ��� ������Ʈ
        CinemachineConfiner confiner = nextRoomCamera.GetComponent<CinemachineConfiner>();
        if (confiner != null)
        {
            confiner.m_BoundingShape2D = newConfiner;
            confiner.InvalidatePathCache(); // ĳ�� ��ȿȭ�Ͽ� ��� ������Ʈ
        }

        yield return new WaitForSeconds(teleportCooldown); // ��ٿ� �ð� ���� ���

        isTeleporting = false; // �ڷ���Ʈ ����
        interactionUI.SetActive(false); //�ڷ���Ʈ ���� �� �ؽ�Ʈ�� ����
    }
}