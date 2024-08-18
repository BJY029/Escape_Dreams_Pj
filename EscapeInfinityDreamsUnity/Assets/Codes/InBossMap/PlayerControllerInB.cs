using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerControllerInB : MonoBehaviour
{
	private Vector2 inputVec;
	public float Speed;
	private float run;
	private float direction;
	private float flag;
	private bool isWalking;
	private bool isRunning;
	public int RoomFlag;

	public float WalkSoundInterval = 0.5f;
	private float WalkSoundTimer;
	public float RunSoundInterval = 0.2f;
	private float RunSoundTimer;

	private bool isAutoRunning;
	public GameObject autoRunTarget;

	Rigidbody2D rb;
	public Animator animator;	
	SpriteRenderer spriteRenderer;
	private BoxCollider2D boxCollider;
	public AudioSource audioSource;

	public GameObject WolfSpawntimeL;

	private void Awake()
	{
		run = 1.0f;
		direction = 1.0f;
		flag = 1.0f;
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();
		audioSource = GetComponent<AudioSource>();

		isWalking = false;
		isRunning = false;
		animator.SetBool("IsAlive", true);
		isAutoRunning = false;
	}

	public void InitAll()
	{
		spriteRenderer.flipX = true;
		WolfSpawntimeL.SetActive(true);
	}

	private void Update()
	{
		UpdateColliderSize();

		if (GameManagerInB.instance.sceneControllerInB.SceneStarting == true) return;

		if (GameManagerInB.instance.warewolfController.isActiveWolfRun == true
			|| GameManagerInB.instance.playerStateControllerInB.isRespawning == true)
		{
			flag = 0f;
			animator.Play("Idle_0");
			return;
		}

		if(GameManagerInB.instance.warewolfController.isExecuting == true)
		{
			flag = 0f;
			return;
		}

		//만약 자동 달리기가 활성화 되면
		if (isAutoRunning)
		{
			//밑의 움직임 방식 대신, AutoRun 함수로 움직임을 통제한다.
			AutoRun();
			return;
		}



		flag = 1.0f;
		inputVec.x = Input.GetAxisRaw("Horizontal");
		if(Input.GetKey(KeyCode.LeftShift))
		{
			run = 2.0f;
			animator.SetBool("run", true);
		}
		else
		{
			run = 1.0f;
			animator.SetBool("run", false);
		}

		//우선 걷거나 뛰는지를 확인한다.
		if(Mathf.Abs(inputVec.x) > 0.1f)
		{
			if(run == 2.0f)
			{
				isRunning = true;
				isWalking = false;
			}
			else
			{
				isWalking = true;
				isRunning = false;
			}
		}
		else
		{
			isWalking = false;
			isRunning = false;	
		}

		//걷는다면
		if(isWalking)
		{
			//걷기 주기 마다 효과음을 재생한다(0.5초)
			WalkSoundTimer -= Time.deltaTime;
			if (WalkSoundTimer <= 0f)
			{
				StartCoroutine(GameManagerInB.instance.audioControllerInB.playWalkSound());
				WalkSoundTimer = WalkSoundInterval;
			}
		}
		else
		{
			WalkSoundTimer = 0f;
		}

		//뛴다면
		if (isRunning)
		{
			//달리기 주기 마다 효과음을 재생한다.(0.2초)
			RunSoundTimer -= Time.deltaTime;
			if(RunSoundTimer <= 0f)
			{
				StartCoroutine(GameManagerInB.instance.audioControllerInB.playWalkSound());
				RunSoundTimer = RunSoundInterval;	
			}
		}
		else
		{
			RunSoundTimer = 0f;
		}

		animator.SetFloat("Speed", inputVec.magnitude);
	}

	private void FixedUpdate()
	{
		Vector2 nextVec = inputVec * flag * Speed * run * direction * Time.fixedDeltaTime;
		rb.MovePosition(rb.position + nextVec);

		if (inputVec.x < 0) spriteRenderer.flipX = false;
		else if(inputVec.x > 0)spriteRenderer.flipX = true;
		
	}

	void UpdateColliderSize()
	{
		if(spriteRenderer != null && boxCollider != null)
		{
			boxCollider.size = spriteRenderer.bounds.size;
			boxCollider.offset = spriteRenderer.bounds.center - transform.position;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("EndingScene")){
			SceneManager.LoadScene("EndingScene");
		}

		if (collision.CompareTag("WareWolfSpawnTime"))
		{
			GameManagerInB.instance.warewolfController.activeSprite();
		}
		//늑대 콜라이더와 충돌했고, 처형 코루틴을 실행중이 아니면
		if (collision.CompareTag("attackRange") && GameManagerInB.instance.warewolfController.isExecuting == false)
		{
			//처형 함수 호출
			GameManagerInB.instance.warewolfController.Execute();
		}

		if (collision.CompareTag("fadeOut"))
		{
			StartCoroutine(GameManagerInB.instance.lightControllerInB.FadeGlobalLight());
		}

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
		else if (collision.CompareTag("Escape"))
		{
			//마지막 탈출 맵과 collider가 충돌하면
			RoomFlag = 10;
			GameManagerInB.instance.warewolfController.startChasing = false;
			StartCoroutine(GameManagerInB.instance.audioControllerInB.ChasedFadeOut());
			//자동 달리기 함수를 호출한다.
			StartAutoRun();
		}

	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.CompareTag("WareWolfSpawnTime"))
		{
			collision.gameObject.SetActive(false);
		}

		if (collision.CompareTag("Escape"))
		{
			GameManagerInB.instance.playerZoomInCameraInB.SwitchVoidCam();
		}
	}

	void StartAutoRun()
	{
		//자동 달리기 플래그 활성화
		isAutoRunning = true;
		//달리기 속도 지정
		run = 2.0f;
		//애니메이션 지정
		animator.SetBool("run", true);
		//카메라 줌 아웃 설정
		StartCoroutine(GameManagerInB.instance.playerZoomInCameraInB.SwitchZoomInCamera());
		//GameManagerInB.instance.playerZoomInCameraInB.SwitchToDefaultCamera();
	}

	void AutoRun()
	{
		//내 위치
		Vector3 currentPosition = rb.position;
		//목표 위치
		Vector2 directionToTarget = (autoRunTarget.transform.position - currentPosition).normalized;

		//다음 위치
		Vector2 nextVec = directionToTarget * Speed * run * Time.deltaTime;
		rb.MovePosition(rb.position + nextVec);

		//만약 목표 지점에 다달으면, 움직임 정지
		if(Vector2.Distance(currentPosition, autoRunTarget.transform.position) < 0.1f)
		{
			isAutoRunning = false;
			animator.SetBool("run", false);
		}

		//걷는 사운드 재생 함수
		RunSoundTimer -= Time.deltaTime;
		if (RunSoundTimer <= 0f)
		{
			StartCoroutine(GameManagerInB.instance.audioControllerInB.playGrassWalkSound());
			RunSoundTimer = RunSoundInterval;
		}
	}
}
