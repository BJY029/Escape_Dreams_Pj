using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class playerAnimationController : MonoBehaviour
{
    Animator animator;
	public GameObject PlayerRespawnLocation;
	public GameObject CatRespawnLocation;
	public bool playerDeadCoroutine;
	public bool canRespawn;

	private CinemachineBrain CinemachineBrain;
	public CinemachineVirtualCameraBase targetCamera;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		CinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
		canRespawn = false;
	}

	//애니메이션 상태 변경을 위한 함수 선언
	public void Dead()
	{
		animator.SetBool("IsAlive", false);
	}

	private void Update()
	{
		//(임시) X키가 눌리면 플레이어는 사망한다.
		if (Input.GetKeyDown(KeyCode.X))
		{
			//만약에 침대와 상호 중이면, 사망 키 발동은 제한하는 조건문
			if (GameManager.Instance.uiSystem.isBedCoroutineRunning == true) return;

			if (animator.GetBool("IsAlive") == true)
			{
				GameManager.Instance.playerController.flag = 0f;
				Dead(); //상태 변경 함수 호출
						//관련 코루틴 실행
				StartCoroutine(PlayerDeadRoutine());

			}
		}

		//만약 플레이어가 현재 사망한 상태이고, 리스폰이 가능한 시점(PlayerDeadRouine 코루틴이 끝난 시점)인 경우에 r키가 눌리면
		if (Input.GetKeyDown(KeyCode.R) && canRespawn == true)
		{
			StartCoroutine(RespawnRoutine());
		}
	}

	//플레이어 사망 코루틴
	public IEnumerator PlayerDeadRoutine()
	{
		//해당 코루틴이 실행되는 중임을 알리는 플래그
		playerDeadCoroutine = true;

		//상호작용 UI가 활성화 되어있으면, 비활성화
		if (GameManager.Instance.uiSystem.interactionUI.activeSelf == true)
		{
			GameManager.Instance.uiSystem.interactionUI.SetActive(false);
		}

		//빛 효과
		yield return StartCoroutine(GameManager.Instance.lightController.DeadLightOut());

		//게임 시간 정지
		Time.timeScale = 0f;

		//재시작 관련 UI 표시 
		GameManager.Instance.uiSystem.DeadStateUi.SetActive(true);

		canRespawn = true;
		//playerDeadCoroutine = false;
	}

	public IEnumerator RespawnRoutine()
	{
		//리스폰 위치로 플레이어와 고양이 이동
		GameManager.Instance.player.transform.position = PlayerRespawnLocation.transform.position;
		GameManager.Instance.cat.transform.position = CatRespawnLocation.transform.position;

		//리스폰 관련 UI 비활성화
		GameManager.Instance.uiSystem.DeadStateUi.SetActive(false);

		//현재 활성화된 가상 카메라 가져오기
		CinemachineVirtualCameraBase activeVirtualCamera = CinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCameraBase;
		//활성화된 가상 카메라의 우선순위 낮추기
		activeVirtualCamera.Priority = 0;
		//Room_0 카메라 우선순위 올려서 메인으로 설정
		targetCamera.Priority = 1;

		//애니메이션을 위해 파라미터 초기화
		animator.SetBool("IsAlive", true);

		//시간 정상 작동
		Time.timeScale = 1f;

		//각종 플래그 초기화
		canRespawn = false;
		playerDeadCoroutine = false;

		//빛 효과 코루틴 호출
		StartCoroutine(GameManager.Instance.lightController.InitAllLights());


		yield return null;
	}
}
