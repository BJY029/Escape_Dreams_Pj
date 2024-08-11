using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TeleportLevelRoomInB : MonoBehaviour
{
	public GameObject targetObj;
	public GameObject toObj;
	public CinemachineVirtualCamera thisRoomCamera;
	public CinemachineVirtualCamera nextRoomCamera;
	private Collider2D newConfiner;
	private Collider2D oldConfiner;
	public float teleportCoolDown = 0.5f;
	public float enemyTeleportDelay = 3f;

	private bool isTeleporting = false;
	private bool canTeleport = false;

	public GameObject interactUI;
	public Vector3 UIOffset;

	public List<GameObject> objectsToTeleport;
	public GameObject enemyObj;

	private void Awake()
	{
		oldConfiner = GetComponent<Collider2D>();
		objectsToTeleport = new List<GameObject>();
	}

	private void Start()
	{
		interactUI.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//�÷��̾ ���� ��ġ�� �ִ� ���
        if (collision.CompareTag("Player"))
		{
			//targetObj�� �÷��̾� ����
			targetObj = collision.gameObject;
			//�÷��� ����
			canTeleport = true;
			interactUI.SetActive(true);
			//���� ����Ʈ�� �÷��̾�� ���� �߰�
			objectsToTeleport.Add(targetObj);
			objectsToTeleport.Add(enemyObj);
		}
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
		//�÷��̾ ���� ��ġ ��� ���
		if (collision.CompareTag("Player"))
		{
			canTeleport = false;
			if(interactUI != null) interactUI.SetActive(false);
			objectsToTeleport.Remove(collision.gameObject);
			objectsToTeleport.Remove(enemyObj);
		}
	}

	private void Update()
	{
		//������ �õ����� ��
		if(canTeleport && !isTeleporting && GameManagerInB.instance.warewolfController.isExecuting != true &&Input.GetKeyDown(KeyCode.E))
		{
			if (oldConfiner.CompareTag("autoDoor"))
			{
				GameManagerInB.instance.audioControllerInB.openingAutoDoor();
			}
			else
			{
				GameManagerInB.instance.audioControllerInB.openingDoor();
			}
			//�ڷ�ƾ ȣ��
			StartCoroutine(TeleportRoutine());
		}

		if (canTeleport) interactUI.transform.position = targetObj.transform.position + UIOffset;
	}

	IEnumerator TeleportRoutine()
	{
		isTeleporting = true;

		//���� �� �켱���� ����
		thisRoomCamera.Priority = 0;

		//�÷��̾� ����
		targetObj.transform.position = toObj.transform.position;
		//������ ������ ����(���� ����)
		GameManagerInB.instance.warewolfController.flag = 0.0f;

		yield return new WaitForEndOfFrame();

		//ī�޶� ��ȯ
		nextRoomCamera.Priority = 1;

		//ī�޶� �����̳� ������Ʈ
		CinemachineConfiner confiner = nextRoomCamera.GetComponent<CinemachineConfiner>();
        if ((confiner != null))
        {
			confiner.m_BoundingShape2D = newConfiner;
			confiner.InvalidatePathCache();
        }

		//��(����) ����
		if(enemyObj != null)
		{
			//������ �ð����� ��� ��
			yield return new WaitForSeconds(enemyTeleportDelay);
			//���� ����
			enemyObj.transform.position = toObj.transform.position;
			//���� ������ �ٽ� ����
			GameManagerInB.instance.warewolfController.flag = 1.0f;
		}

		yield return new WaitForSeconds(teleportCoolDown);

		isTeleporting = false;
		interactUI.SetActive(false);
    }
}
