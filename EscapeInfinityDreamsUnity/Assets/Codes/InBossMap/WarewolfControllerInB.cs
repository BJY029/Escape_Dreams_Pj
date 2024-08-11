using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarewolfControllerInB : MonoBehaviour
{
    public Transform wareWolf;
    public float moveSpeed;
	public float stopDistance = 0.5f;
	public float flag;
	public int RoomFlag;

	public float cameraChangeTime;
	public bool isActiveWolfRun;
	public bool isExecuting;
	public bool startChasing;
	public bool canRespawn;
	bool isRunning;
	public float RunSoundInterval;
	private float RunSoundTimer;

	public GameObject WareWolfLocation;
    private Rigidbody2D rb;
    private Vector2 movement;
	Animator animator;
	private SpriteRenderer spriteRenderer;
	private BoxCollider2D boxCollider;
	public AudioSource audioSource;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();
		audioSource = GetComponent<AudioSource>();

		//시작과 동시에 해당 스프라이트를 활성화 해제 한다.
		wareWolf.gameObject.SetActive(false);
		startChasing = false;
		canRespawn = false;
		flag = 1.0f;
		isRunning = false;

	}

	public void InitAll()
	{
		//늑대인간의 위치를 초기화한다.
		wareWolf.transform.position = WareWolfLocation.transform.position;
		wareWolf.gameObject.SetActive(false);
	}

	private void Update()
	{
		//주기적으로 늑대인간의 collider를 업데이트 해준다.
		UpdateColliderSize();

		//따라오는 상황이 아니면 따라오지 않는다.
		if (startChasing == false) return;

		//방향 계산
		Vector2 direction = (GameManagerInB.instance.player.transform.position - transform.position).normalized;

		//거리 계산
		float distanceToPlayer = Vector2.Distance(transform.position, GameManagerInB.instance.player.transform.position);

		
		//만약 거리가 정지 지점보다 크면
		if(distanceToPlayer > stopDistance)
		{
			//계속 따라온다.
			movement = direction;
			isRunning = true;
		}
		else
		//정지 지점에 도달하면
		{
			//움직임을 멈춘다.
			movement = Vector2.zero;
			isRunning = false;
		}

		//움직임에 따른 애니메이션 설정
		animator.SetFloat("Speed", movement.magnitude * moveSpeed);

		if (isRunning)
		{
			RunSoundTimer -= Time.deltaTime;
			if(RunSoundTimer <= 0f)
			{
				StartCoroutine(GameManagerInB.instance.audioControllerInB.playWolfWalkSound());
				RunSoundTimer = RunSoundInterval;
			}
		}
		else
		{
			RunSoundTimer = 0f;
		}
	}

	private void FixedUpdate()
	{
		//늑대 움직임 구현
		rb.MovePosition(rb.position + movement * flag * moveSpeed * Time.fixedDeltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Hall"))
		{
			RoomFlag = 0;
		}
		else if (collision.CompareTag("Level1"))
		{
			RoomFlag = 1;
		}
		else if (collision.CompareTag("Level2"))
		{
			RoomFlag = 2;
		}
		else if (collision.CompareTag("Level3"))
		{
			RoomFlag = 3;
		}
		else if (collision.CompareTag("Level4"))
		{
			RoomFlag = 4;
		}
		else if (collision.CompareTag("Level5"))
		{
			RoomFlag = 5;
		}
		else if (collision.CompareTag("Level6"))
		{
			RoomFlag = 6;
		}
		else if (collision.CompareTag("Level7"))
		{
			RoomFlag = 7;
		}
		else if (collision.CompareTag("Final"))
		{
			RoomFlag = 8;
		}
		else if (collision.CompareTag("WrongWay"))
		{
			RoomFlag = -1;
		}
	}

	//플레이어가 특정 위치에 도달한 경우 호출된다.
	public void activeSprite()
	{
		//늑대인간을 활성화 한다.
		//코루틴을 사용하려면 해당 스프라이트가 활성화 되어 있어야 한다.
		wareWolf.gameObject.SetActive(true);
		//코루틴 호출
		StartCoroutine(activeWolf());
	}

	//늑대 collider와 닿아서 처형당하는 상황일 때 호출된다.
	public void Execute()
	{
		StartCoroutine(ExecutePlayer());
	}

	//늑대 활성화 코루틴
	IEnumerator activeWolf()
	{
		//코루틴이 진행되는 동안, 플레이어 움직임을 제한하기 위한 플래그
		isActiveWolfRun = true;

		//카메라 전환
		GameManagerInB.instance.wolfZoomInCameraInB.SwitchToZoomCamera();
		//전환 간 대기
		yield return new WaitForSeconds(cameraChangeTime);

		//특정 애니메이션 재생
		animator.SetBool("Bark", true);
		GameManagerInB.instance.audioControllerInB.PlayRoar();
		//애니메이션 재생 간 대기
		yield return new WaitForSeconds(1.0f);

		//애니메이션 다시 Idle로 변경
		animator.SetBool("Bark", false);
		//일정 시간 대기
		yield return new WaitForSeconds(1.0f);

		//다시 원래 카메라로 변경
		GameManagerInB.instance.wolfZoomInCameraInB.SwitchToDefaultCamera();
		//변경 되는 동안 대기
		yield return new WaitForSeconds(cameraChangeTime);

		//플래그 해제
		isActiveWolfRun = false;
		startChasing = true;
	}

	//플레이어 사망 효과 코루틴
	IEnumerator ExecutePlayer()
	{
		//해당 코루틴을 실행 중에 다른 함수를 통제하기 위한 코루틴
		isExecuting = true;

		//카메라 전환
		GameManagerInB.instance.wolfZoomInCameraInB.SwitchToZoomCamera();
		//전환 간 대기

		//특정 애니메이션 재생
		animator.SetBool("Bark", true);
		StartCoroutine(GameManagerInB.instance.audioControllerInB.attackPlay());
		//애니메이션 재생 간 대기
		yield return new WaitForSeconds(1.0f);
		animator.SetBool("Bark", false);

		//플레이어로 카메라를 줌인 하는 함수 호출
		GameManagerInB.instance.wolfZoomInCameraInB.SwitchToZoomCameraToPlayer();

		

		//플레이어 사망 애니메이션 설정
		GameManagerInB.instance.playerController.animator.SetBool("IsAlive", false);
		GameManagerInB.instance.playerController.animator.Play("die_0");
		//빛 효과 연출
		yield return GameManagerInB.instance.lightControllerInB.PlayerDeadLight();
		//yield return new WaitForSeconds(2.0f);

		//재시작 안내 UI 노출
		GameManagerInB.instance.UIControllerInB.activeDeadStateUI();

		//시간 멈춤
		Time.timeScale = 0f;

		canRespawn = true;
		//isExecuting = false;
	}


	//collider 사이즈를 업데이트 하는 함수
	void UpdateColliderSize()
	{
		if (boxCollider != null && spriteRenderer != null)
		{
			//bounds.size = 경계 상자의 전체 크기를 나타내는 Vector3. max - min의 값과 같다.
			//콜라이더의 크기를 스프라이트 크기로 변경한다.
			boxCollider.size = spriteRenderer.bounds.size;
			//bounds.center = 경계 상자의 중심 위치를 나타내는 Vector3
			//콜라이더의 위치를 변경한다.
			boxCollider.offset = spriteRenderer.bounds.center - transform.position;
		}
	}
}
