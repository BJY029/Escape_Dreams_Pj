using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float Speed;
	private float run;
	private float flag;
    Rigidbody2D rb;
	Animator animator;
	SpriteRenderer spriteRenderer;

	public UiSystem uiSystem; //uisystem���� �ڷ�ƾ�� ���� ������ ������ ���� ���� ����

    //���۰� ���ÿ� �ʱ�ȭ �ϴ� ��ϵ�
    private void Awake()
	{
		run = 1.0f;
		flag = 1.0f;
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
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
		Vector2 nextVec = inputVec * Speed * run * flag * Time.fixedDeltaTime;
		rb.MovePosition(rb.position + nextVec);

		//�÷��̾��� �̵����⿡ �°� ��������Ʈ�� �ٶ󺸴� ���� ����
		if (inputVec.x > 0) spriteRenderer.flipX = false;
		else if (inputVec.x < 0) spriteRenderer.flipX = true;
	}

	//Update�� ������ ���� �� �����Ӵ� �� �� ȣ��Ǵ� �Լ���.
	//�÷��̾��� �������� Update���� �Ϸ� ��, �̵��� ��ġ�� ���� ī�޶��� ��ġ�� �����ϴ�
	//����� ������ �� ���
	private void LateUpdate()
	{
		//�ִϸ��̼��� ���� ����
		animator.SetFloat("Speed", inputVec.magnitude);
	}
}