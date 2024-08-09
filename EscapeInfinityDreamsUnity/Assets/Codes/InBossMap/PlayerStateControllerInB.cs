using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerStateControllerInB : MonoBehaviour
{
    Animator animator;
    public GameObject PlayerRespawnLocation;

    private CinemachineBrain CinemachineBrain;
    public CinemachineVirtualCameraBase targetCamera;
	public bool isRespawning;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		CinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();

		isRespawning = false;
	}

	private void Update()
	{
		//플레이어가 리스폰 할 수 있는 상태일때만 작동한다.
		if(Input.GetKeyDown(KeyCode.R) && GameManagerInB.instance.warewolfController.canRespawn == true)
		{
			StartCoroutine(Respawn());
		}
	}

	//리스폰 코루틴
	IEnumerator Respawn()
	{
		//플래그 설정
		isRespawning = true;

		//플레이어 위치 이동
		GameManagerInB.instance.player.transform.position = PlayerRespawnLocation.transform.position;

		//UI 비활성화
		GameManagerInB.instance.UIControllerInB.nonAcviteDeadStateUI();

		//카메라 우선순위 변경
		CinemachineVirtualCameraBase activeVituralCamera = CinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCameraBase;
		activeVituralCamera.Priority = 0;
		targetCamera.Priority = 1;

		//애니메이션 초기화
		animator.SetBool("IsAlive", true);

		//시간 다시 흐르게
		Time.timeScale = 1f;

		//각종 작업 초기화
		GameManagerInB.instance.warewolfController.InitAll();
		GameManagerInB.instance.playerController.InitAll();

		//fade in 라이트 코루틴 ㅈ ㅐ생
		yield return StartCoroutine(GameManagerInB.instance.lightControllerInB.InitLights());

		//각종 플래그 초기화
		GameManagerInB.instance.warewolfController.isExecuting = false;
		GameManagerInB.instance.warewolfController.canRespawn = false;
		isRespawning = false;

		yield return null;
	}
}
