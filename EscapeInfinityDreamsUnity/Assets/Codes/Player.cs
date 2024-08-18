using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float Speed;
	private float run;
	public float flag;
	public float direction;
	public float acc;
    Rigidbody2D rb;
	Animator animator;
	SpriteRenderer spriteRenderer;
	public abnorbalManager abnorbalManager;
	public Light2D GlobalLight;
	private ShadowCaster2D shadowCaster;
	private BoxCollider2D boxCollider;
	public AudioSource AudioSource;
	public int cnt;

	private bool isWalking;
	private bool isRunning;
	public float WalkSoundInterval = 0.5f;
	private float WalkSoundTimer;
	public float RunSoundInterval = 0.3f;
	private float RunSoundTimer;

	public float waitTime = 1.0f;

	public UiSystem uiSystem; //uisystem에서 코루틴의 실행 정보를 가지고 오기 위한 선언

    //시작과 동시에 초기화 하는 목록들
    private void Awake()
	{
		cnt = 0;
		run = 1.0f;
		flag = 1.0f;
		direction = 1.0f;
		acc = 1.0f;

		isWalking = false;
		isRunning = false;

		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		shadowCaster = GetComponent<ShadowCaster2D>();
		boxCollider = GetComponent<BoxCollider2D>();
		AudioSource = GetComponent<AudioSource>();
	}

	//창문 오디오를 한번만 재생하기 위해 cnt를 0으로 초기화 하는 함수
	public void init()
	{
		cnt = 0;
	}

	public void InitAll()
	{
		GlobalLight.color = Color.white;
		direction = 1.0f;
		acc = 1.0f;
		shadowCaster.enabled = true;
		WalkSoundInterval = 0.5f;
		RunSoundInterval = 0.3f;
	}

	private void Start()
	{
		shadowCaster.enabled = true;
	}
	//기본적인 update 함수로 매 프레임 호출된다.
	//프레임마다 작업량이 다르므로 호출 주기가 일정하지 않다.
	private void Update()
	{
		if (GameManager.Instance.sceneManager.SceneisStarting == true) return;
		UpdateColliderSize();
		//코루틴이 실행중이면
		if (uiSystem.isBedCoroutineRunning == true || GameManager.Instance.playerAnimationController.isRespawning == true)
		{
			animator.SetFloat("Speed", 0f);
			//이벤트 시스템 비활성화 = 입력 제한
			uiSystem.eventSystem.enabled = false;
			flag = 0; //이동 중에 상호작용 시 오류 방지 위한 추가 플래그
			//animator.Play("Idle_0");//나중에 잠 자는 애니매이션이 추가되면 변경
			return;
		}

		//만약 플레이어가 사망했다면, 해당 입력키 제한
		if (GameManager.Instance.playerAnimationController.playerDeadCoroutine == true)
		{
			uiSystem.eventSystem.enabled = false;
			return;
		}

		uiSystem.eventSystem.enabled = true;
		flag = 1.0f;//다시 속도를 원래대로 적용
					//키보드로 입력받은 값을 Vector.x값에 저장
		inputVec.x = Input.GetAxisRaw("Horizontal");

		//달리기 키가 눌리면 run 값을 2로 설정
		//Run 애니메이션 true로 설정
		if (Input.GetKey(KeyCode.LeftShift))
		{
			run = 2.0f;
			animator.SetBool("run", true);
		}
		else
		{
			run = 1.0f;
			animator.SetBool("run", false);
		}


		if (Mathf.Abs(inputVec.x) > 0.1f)
		{
			if (run == 2.0f)
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


		if (isWalking)
		{
			WalkSoundTimer -= Time.deltaTime;
			if (WalkSoundTimer <= 0f)
			{
				StartCoroutine(GameManager.Instance.audioController.playWalkSound());
				WalkSoundTimer = WalkSoundInterval;
			}
		}
		else
		{
			WalkSoundTimer = 0f;
		}

		if (isRunning)
		{
			RunSoundTimer -= Time.deltaTime;
			if (RunSoundTimer <= 0f)
			{
				StartCoroutine(GameManager.Instance.audioController.playWalkSound());
				RunSoundTimer = RunSoundInterval;
			}
		}
		else
		{
			RunSoundTimer = 0f;
		}
	}

	//함수 호출 간격이 일정하도록 보장하는 update 함수다.
	//따라서 물리 계산 및 업데이트, Ray 처리에 주로 사용된다.
	private void FixedUpdate()
	{
		//플레이어 움직임 구현
		//flag는 상호작용시 해당 플레이어의 이동을 멈추기 위함
		Vector2 nextVec = inputVec * Speed * acc * run * flag * direction * Time.fixedDeltaTime;
		rb.MovePosition(rb.position + nextVec);

		//플레이어의 이동방향ㅁ에 맞게 스프라이트가 바라보는 방향 수정
		if (direction < 0)	//이동 방향이 반대인 경우, 바라보는 방향도 반대로 설정
		{
			if (inputVec.x > 0) spriteRenderer.flipX = false;
			else if (inputVec.x < 0) spriteRenderer.flipX = true;
		}
		else
		{
			if (inputVec.x > 0) spriteRenderer.flipX = true;
			else if (inputVec.x < 0) spriteRenderer.flipX = false;
		}
	}

	//Update가 완전히 끝난 후 프레임당 한 번 호출되는 함수다.
	//플레이어의 움직임을 Update에서 완료 후, 이동한 위치에 따라 카메라의 위치를 수정하는
	//방식을 구현할 때 사용
	private void LateUpdate()
	{
		//애니메이션을 위한 연산
		animator.SetFloat("Speed", inputVec.magnitude);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//flag가 24 일때만 실행(빛 변경 효과)
		if (abnorbalManager.flag == 24)
		{
			//플레이어의 위치가 복도, Room_1 일때만 빛이 변경되도록 설정
			if (collision.CompareTag("mainMap") || collision.CompareTag("Room_1"))
			{
				GlobalLight.color = Color.red;
			}
			else if (collision.CompareTag("Room_0"))
			{
				GlobalLight.color = Color.white;
			}
		}
		if(abnorbalManager.flag == 25) //좌우 반전인 경우
		{
			if (collision.CompareTag("mainMap") || collision.CompareTag("Room_1"))
			{
				direction = -1.0f; //해당 맵에서는 방향을 반대로 적용
			}
			else if (collision.CompareTag("Room_0"))
			{
				direction = 1.0f; //기본 방에서는 이상현상 적용 x
			}
		}
		if (abnorbalManager.flag == 26) //스피드 변경
		{
			if (collision.CompareTag("mainMap") || collision.CompareTag("Room_1"))
			{
				acc = 2.0f;
				WalkSoundInterval = 0.3f;
				RunSoundInterval = 0.2f;
			}
			else if (collision.CompareTag("Room_0"))
			{
				acc = 1.0f;
				WalkSoundInterval = 0.5f;
				RunSoundInterval = 0.3f;
			}
		}
		if (abnorbalManager.flag == 27) //그림자 삭제인 경우
		{
			if (collision.CompareTag("mainMap") || collision.CompareTag("Room_1"))
			{
				shadowCaster.enabled = false;
			}
			else if (collision.CompareTag("Room_0"))
			{
				shadowCaster.enabled = true;
			}
		}
		if (abnorbalManager.flag == 29) //창문 오디오 이상현상
		{
			if (collision.CompareTag("Window") && cnt == 0)
			{
				StartCoroutine(PlayWindowKnock());
				cnt = 1;
			}
		}
	}

	//창문 효과음을 일정 시간 이후에 출력하도록 하는 코루틴
	IEnumerator PlayWindowKnock()
	{
		yield return new WaitForSeconds(waitTime);
		GameManager.Instance.audioController.PlayWindoeKnocking();
	}

	void UpdateColliderSize()
	{
		if (spriteRenderer != null && boxCollider != null)
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