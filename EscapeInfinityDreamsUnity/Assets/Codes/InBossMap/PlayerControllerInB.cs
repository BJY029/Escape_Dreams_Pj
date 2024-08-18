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

		//���� �ڵ� �޸��Ⱑ Ȱ��ȭ �Ǹ�
		if (isAutoRunning)
		{
			//���� ������ ��� ���, AutoRun �Լ��� �������� �����Ѵ�.
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

		//�켱 �Ȱų� �ٴ����� Ȯ���Ѵ�.
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

		//�ȴ´ٸ�
		if(isWalking)
		{
			//�ȱ� �ֱ� ���� ȿ������ ����Ѵ�(0.5��)
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

		//�ڴٸ�
		if (isRunning)
		{
			//�޸��� �ֱ� ���� ȿ������ ����Ѵ�.(0.2��)
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
		//���� �ݶ��̴��� �浹�߰�, ó�� �ڷ�ƾ�� �������� �ƴϸ�
		if (collision.CompareTag("attackRange") && GameManagerInB.instance.warewolfController.isExecuting == false)
		{
			//ó�� �Լ� ȣ��
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
			//������ Ż�� �ʰ� collider�� �浹�ϸ�
			RoomFlag = 10;
			GameManagerInB.instance.warewolfController.startChasing = false;
			StartCoroutine(GameManagerInB.instance.audioControllerInB.ChasedFadeOut());
			//�ڵ� �޸��� �Լ��� ȣ���Ѵ�.
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
		//�ڵ� �޸��� �÷��� Ȱ��ȭ
		isAutoRunning = true;
		//�޸��� �ӵ� ����
		run = 2.0f;
		//�ִϸ��̼� ����
		animator.SetBool("run", true);
		//ī�޶� �� �ƿ� ����
		StartCoroutine(GameManagerInB.instance.playerZoomInCameraInB.SwitchZoomInCamera());
		//GameManagerInB.instance.playerZoomInCameraInB.SwitchToDefaultCamera();
	}

	void AutoRun()
	{
		//�� ��ġ
		Vector3 currentPosition = rb.position;
		//��ǥ ��ġ
		Vector2 directionToTarget = (autoRunTarget.transform.position - currentPosition).normalized;

		//���� ��ġ
		Vector2 nextVec = directionToTarget * Speed * run * Time.deltaTime;
		rb.MovePosition(rb.position + nextVec);

		//���� ��ǥ ������ �ٴ�����, ������ ����
		if(Vector2.Distance(currentPosition, autoRunTarget.transform.position) < 0.1f)
		{
			isAutoRunning = false;
			animator.SetBool("run", false);
		}

		//�ȴ� ���� ��� �Լ�
		RunSoundTimer -= Time.deltaTime;
		if (RunSoundTimer <= 0f)
		{
			StartCoroutine(GameManagerInB.instance.audioControllerInB.playGrassWalkSound());
			RunSoundTimer = RunSoundInterval;
		}
	}
}
