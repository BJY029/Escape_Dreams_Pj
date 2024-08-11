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
		//플레이어가 텔포 위치에 있는 경우
        if (collision.CompareTag("Player"))
		{
			//targetObj에 플레이어 저장
			targetObj = collision.gameObject;
			//플래그 설정
			canTeleport = true;
			interactUI.SetActive(true);
			//텔포 리스트에 플레이어와 늑대 추가
			objectsToTeleport.Add(targetObj);
			objectsToTeleport.Add(enemyObj);
		}
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
		//플레이어가 텔포 위치 벗어난 경우
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
		//텔포를 시도했을 때
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
			//코루틴 호출
			StartCoroutine(TeleportRoutine());
		}

		if (canTeleport) interactUI.transform.position = targetObj.transform.position + UIOffset;
	}

	IEnumerator TeleportRoutine()
	{
		isTeleporting = true;

		//현재 방 우선순위 낮춤
		thisRoomCamera.Priority = 0;

		//플레이어 텔포
		targetObj.transform.position = toObj.transform.position;
		//늑대의 움직임 멈춤(버그 방지)
		GameManagerInB.instance.warewolfController.flag = 0.0f;

		yield return new WaitForEndOfFrame();

		//카메라 전환
		nextRoomCamera.Priority = 1;

		//카메라 컨파이너 업데이트
		CinemachineConfiner confiner = nextRoomCamera.GetComponent<CinemachineConfiner>();
        if ((confiner != null))
        {
			confiner.m_BoundingShape2D = newConfiner;
			confiner.InvalidatePathCache();
        }

		//적(늑대) 텔포
		if(enemyObj != null)
		{
			//지정된 시간동안 대기 후
			yield return new WaitForSeconds(enemyTeleportDelay);
			//늑대 텔포
			enemyObj.transform.position = toObj.transform.position;
			//늑대 움직임 다시 시작
			GameManagerInB.instance.warewolfController.flag = 1.0f;
		}

		yield return new WaitForSeconds(teleportCoolDown);

		isTeleporting = false;
		interactUI.SetActive(false);
    }
}
