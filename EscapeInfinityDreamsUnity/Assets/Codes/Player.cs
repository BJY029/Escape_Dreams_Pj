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
	private float flag;
	public float direction;
	public float acc;
    Rigidbody2D rb;
	Animator animator;
	SpriteRenderer spriteRenderer;
	public abnorbalManager abnorbalManager;
	public Light2D GlobalLight;
	private ShadowCaster2D shadowCaster;

	public UiSystem uiSystem; //uisystem���� �ڷ�ƾ�� ���� ������ ������ ���� ���� ����

    //���۰� ���ÿ� �ʱ�ȭ �ϴ� ��ϵ�
    private void Awake()
	{
		run = 1.0f;
		flag = 1.0f;
		direction = 1.0f;
		acc = 1.0f;
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		shadowCaster = GetComponent<ShadowCaster2D>();
	}

	private void Start()
	{
		shadowCaster.enabled = true;
	}
	//�⺻���� update �Լ��� �� ������ ȣ��ȴ�.
	//�����Ӹ��� �۾����� �ٸ��Ƿ� ȣ�� �ֱⰡ �������� �ʴ�.
	private void Update()
	{
		//�ڷ�ƾ�� �������̸�
		if (uiSystem.isBedCoroutineRunning == true)
		{
			//�̺�Ʈ �ý��� ��Ȱ��ȭ = �Է� ����
			uiSystem.eventSystem.enabled = false;
			flag = 0.0f; //�̵� �߿� ��ȣ�ۿ� �� ���� ���� ���� �߰� �÷���
			animator.Play("Idle_0");//���߿� �� �ڴ� �ִϸ��̼��� �߰��Ǹ� ����
			return;
		}

		uiSystem.eventSystem.enabled = true;
		flag = 1.0f;//�ٽ� �ӵ��� ������� ����
		//Ű����� �Է¹��� ���� Vector.x���� ����
		inputVec.x = Input.GetAxisRaw("Horizontal");

		//�޸��� Ű�� ������ run ���� 2�� ����
		//Run �ִϸ��̼� true�� ����
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
		
	}

	//�Լ� ȣ�� ������ �����ϵ��� �����ϴ� update �Լ���.
	//���� ���� ��� �� ������Ʈ, Ray ó���� �ַ� ���ȴ�.
	private void FixedUpdate()
	{
		//�÷��̾� ������ ����
		//flag�� ��ȣ�ۿ�� �ش� �÷��̾��� �̵��� ���߱� ����
		Vector2 nextVec = inputVec * Speed * acc * run * flag * direction * Time.fixedDeltaTime;
		rb.MovePosition(rb.position + nextVec);

		//�÷��̾��� �̵����⤱�� �°� ��������Ʈ�� �ٶ󺸴� ���� ����
		if (direction < 0)	//�̵� ������ �ݴ��� ���, �ٶ󺸴� ���⵵ �ݴ�� ����
		{
			if (inputVec.x > 0) spriteRenderer.flipX = true;
			else if (inputVec.x < 0) spriteRenderer.flipX = false;
		}
		else
		{
			if (inputVec.x > 0) spriteRenderer.flipX = false;
			else if (inputVec.x < 0) spriteRenderer.flipX = true;
		}
	}

	//Update�� ������ ���� �� �����Ӵ� �� �� ȣ��Ǵ� �Լ���.
	//�÷��̾��� �������� Update���� �Ϸ� ��, �̵��� ��ġ�� ���� ī�޶��� ��ġ�� �����ϴ�
	//����� ������ �� ���
	private void LateUpdate()
	{
		//�ִϸ��̼��� ���� ����
		animator.SetFloat("Speed", inputVec.magnitude);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//flag�� 22 �϶��� ����(�� ���� ȿ��)
		if (abnorbalManager.flag == 23)
		{
			//�÷��̾��� ��ġ�� ����, Room_1 �϶��� ���� ����ǵ��� ����
			if (collision.CompareTag("mainMap") || collision.CompareTag("Room_1"))
			{
				GlobalLight.color = Color.red;
			}
			else if (collision.CompareTag("Room_0"))
			{
				GlobalLight.color = Color.white;
			}
		}
		if(abnorbalManager.flag == 24) //�¿� ������ ���
		{
			if (collision.CompareTag("mainMap") || collision.CompareTag("Room_1"))
			{
				direction = -1.0f; //�ش� �ʿ����� ������ �ݴ�� ����
			}
			else if (collision.CompareTag("Room_0"))
			{
				direction = 1.0f; //�⺻ �濡���� �̻����� ���� x
			}
		}
		if (abnorbalManager.flag == 25) //�¿� ������ ���
		{
			if (collision.CompareTag("mainMap") || collision.CompareTag("Room_1"))
			{
				acc = 2.0f;
			}
			else if (collision.CompareTag("Room_0"))
			{
				acc = 1.0f;
			}
		}
		if (abnorbalManager.flag == 26) //�׸��� ������ ���
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
	}
}