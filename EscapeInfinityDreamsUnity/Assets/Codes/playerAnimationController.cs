using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.Universal;

public class playerAnimationController : MonoBehaviour
{
    Animator animator;
	public GameObject PlayerRespawnLocation;
	public GameObject CatRespawnLocation;
	public bool playerDeadCoroutine;
	public bool canRespawn;
	public bool isRespawning;

	public SpriteRenderer catSprite;

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
		if (Input.GetKeyDown(KeyCode.X) && GameManager.Instance.sceneManager.SceneisStarting == false)
		{
			//만약에 침대와 상호 중이면, 사망 키 발동은 제한하는 조건문, 추가로 종이 UI와 상호작용 중일때도 발동을 제한한다.
			if (GameManager.Instance.uiSystem.isBedCoroutineRunning == true || GameManager.Instance.uiSystem.isPaperisVisualable == true) return;

			if (animator.GetBool("IsAlive") == true)
			{
				GameManager.Instance.playerController.flag = 0f;

						//관련 코루틴 실행
				StartCoroutine(PlayerDeadRoutine());
			}
		}

		//만약 플레이어가 현재 사망한 상태이고, 리스폰이 가능한 시점(PlayerDeadRouine 코루틴이 끝난 시점)인 경우에 r키가 눌리면
		if (Input.GetKeyDown(KeyCode.R) && canRespawn == true && GameManager.Instance.sceneManager.SceneisStarting == false)
		{
			StartCoroutine(RespawnRoutine());
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)  
    {
        if (collision.CompareTag("enemy"))     //적과 충돌했을 때, 사망한다.
        {
            if (animator.GetBool("IsAlive") == true)
            {
                GameManager.Instance.playerController.flag = 0f;
                Dead(); //상태 변경 함수 호출
                        //관련 코루틴 실행
                StartCoroutine(PlayerDeadRoutine());
            }
        }
    }

    //플레이어 사망 코루틴
    public IEnumerator PlayerDeadRoutine()
	{
		//해당 코루틴이 실행되는 중임을 알리는 플래그
		playerDeadCoroutine = true;

		animator.SetBool("isDrinking", true);
		yield return new WaitForSeconds(1f);
		animator.SetBool("isDrinking", false);
		yield return new WaitForSeconds(1f);
		animator.SetBool("IsAlive", false);


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
		isRespawning = true;

		//리스폰 위치로 플레이어와 고양이 이동
		GameManager.Instance.player.transform.position = PlayerRespawnLocation.transform.position;
		GameManager.Instance.cat.transform.position = CatRespawnLocation.transform.position;
		catSprite.flipX = false;

		//만약 이상현상이 발생하지 않은 상태에서 자살을 택하면, 레벨을 상승하고, 다음 단계로 진행한다.
		if (GameManager.Instance.isAbnormal == false)
		{
			if (GameManager.level != 0) //레벨 0일때 자살을 시도하면 다음 단계로 넘어갈 수 없다.
			{
				GameManager.level += 1;
				GameManager.Instance.abnorbalManager.nextStage();
			}
		}
		else//이상 현상이 발생했는데, 자살을 택하면, 실패로 판정, 레벨을 초기화한다.
		{
			GameManager.level = 0;
			GameManager.Instance.abnorbalManager.Init();
		}

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


		//빛 효과 코루틴 호출
		yield return(StartCoroutine(GameManager.Instance.lightController.InitAllLights()));

		//버그 방지를 위해 cat대화창 오브젝트 활성화
		GameManager.Instance.catDialogController.gameObject.SetActive(true);
		//대화 창을 출력하는 코루틴 호출
		yield return StartCoroutine(GameManager.Instance.catDialogController.dialogController());

		//각종 플래그 초기화
		canRespawn = false;
		playerDeadCoroutine = false;
		isRespawning = false;

		yield return null;
	}
}
