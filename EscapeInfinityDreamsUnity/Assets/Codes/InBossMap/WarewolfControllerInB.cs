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

		//���۰� ���ÿ� �ش� ��������Ʈ�� Ȱ��ȭ ���� �Ѵ�.
		wareWolf.gameObject.SetActive(false);
		startChasing = false;
		canRespawn = false;
		flag = 1.0f;
		isRunning = false;

	}

	public void InitAll()
	{
		//�����ΰ��� ��ġ�� �ʱ�ȭ�Ѵ�.
		wareWolf.transform.position = WareWolfLocation.transform.position;
		wareWolf.gameObject.SetActive(false);
	}

	private void Update()
	{
		//�ֱ������� �����ΰ��� collider�� ������Ʈ ���ش�.
		UpdateColliderSize();

		//������� ��Ȳ�� �ƴϸ� ������� �ʴ´�.
		if (startChasing == false) return;

		//���� ���
		Vector2 direction = (GameManagerInB.instance.player.transform.position - transform.position).normalized;

		//�Ÿ� ���
		float distanceToPlayer = Vector2.Distance(transform.position, GameManagerInB.instance.player.transform.position);

		
		//���� �Ÿ��� ���� �������� ũ��
		if(distanceToPlayer > stopDistance)
		{
			//��� ����´�.
			movement = direction;
			isRunning = true;
		}
		else
		//���� ������ �����ϸ�
		{
			//�������� �����.
			movement = Vector2.zero;
			isRunning = false;
		}

		//�����ӿ� ���� �ִϸ��̼� ����
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
		//���� ������ ����
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

	//�÷��̾ Ư�� ��ġ�� ������ ��� ȣ��ȴ�.
	public void activeSprite()
	{
		//�����ΰ��� Ȱ��ȭ �Ѵ�.
		//�ڷ�ƾ�� ����Ϸ��� �ش� ��������Ʈ�� Ȱ��ȭ �Ǿ� �־�� �Ѵ�.
		wareWolf.gameObject.SetActive(true);
		//�ڷ�ƾ ȣ��
		StartCoroutine(activeWolf());
	}

	//���� collider�� ��Ƽ� ó�����ϴ� ��Ȳ�� �� ȣ��ȴ�.
	public void Execute()
	{
		StartCoroutine(ExecutePlayer());
	}

	//���� Ȱ��ȭ �ڷ�ƾ
	IEnumerator activeWolf()
	{
		//�ڷ�ƾ�� ����Ǵ� ����, �÷��̾� �������� �����ϱ� ���� �÷���
		isActiveWolfRun = true;

		//ī�޶� ��ȯ
		GameManagerInB.instance.wolfZoomInCameraInB.SwitchToZoomCamera();
		//��ȯ �� ���
		yield return new WaitForSeconds(cameraChangeTime);

		//Ư�� �ִϸ��̼� ���
		animator.SetBool("Bark", true);
		GameManagerInB.instance.audioControllerInB.PlayRoar();
		//�ִϸ��̼� ��� �� ���
		yield return new WaitForSeconds(1.0f);

		//�ִϸ��̼� �ٽ� Idle�� ����
		animator.SetBool("Bark", false);
		//���� �ð� ���
		yield return new WaitForSeconds(1.0f);

		//�ٽ� ���� ī�޶�� ����
		GameManagerInB.instance.wolfZoomInCameraInB.SwitchToDefaultCamera();
		//���� �Ǵ� ���� ���
		yield return new WaitForSeconds(cameraChangeTime);

		//�÷��� ����
		isActiveWolfRun = false;
		startChasing = true;
	}

	//�÷��̾� ��� ȿ�� �ڷ�ƾ
	IEnumerator ExecutePlayer()
	{
		//�ش� �ڷ�ƾ�� ���� �߿� �ٸ� �Լ��� �����ϱ� ���� �ڷ�ƾ
		isExecuting = true;

		//ī�޶� ��ȯ
		GameManagerInB.instance.wolfZoomInCameraInB.SwitchToZoomCamera();
		//��ȯ �� ���

		//Ư�� �ִϸ��̼� ���
		animator.SetBool("Bark", true);
		StartCoroutine(GameManagerInB.instance.audioControllerInB.attackPlay());
		//�ִϸ��̼� ��� �� ���
		yield return new WaitForSeconds(1.0f);
		animator.SetBool("Bark", false);

		//�÷��̾�� ī�޶� ���� �ϴ� �Լ� ȣ��
		GameManagerInB.instance.wolfZoomInCameraInB.SwitchToZoomCameraToPlayer();

		

		//�÷��̾� ��� �ִϸ��̼� ����
		GameManagerInB.instance.playerController.animator.SetBool("IsAlive", false);
		GameManagerInB.instance.playerController.animator.Play("die_0");
		//�� ȿ�� ����
		yield return GameManagerInB.instance.lightControllerInB.PlayerDeadLight();
		//yield return new WaitForSeconds(2.0f);

		//����� �ȳ� UI ����
		GameManagerInB.instance.UIControllerInB.activeDeadStateUI();

		//�ð� ����
		Time.timeScale = 0f;

		canRespawn = true;
		//isExecuting = false;
	}


	//collider ����� ������Ʈ �ϴ� �Լ�
	void UpdateColliderSize()
	{
		if (boxCollider != null && spriteRenderer != null)
		{
			//bounds.size = ��� ������ ��ü ũ�⸦ ��Ÿ���� Vector3. max - min�� ���� ����.
			//�ݶ��̴��� ũ�⸦ ��������Ʈ ũ��� �����Ѵ�.
			boxCollider.size = spriteRenderer.bounds.size;
			//bounds.center = ��� ������ �߽� ��ġ�� ��Ÿ���� Vector3
			//�ݶ��̴��� ��ġ�� �����Ѵ�.
			boxCollider.offset = spriteRenderer.bounds.center - transform.position;
		}
	}
}
