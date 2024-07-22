using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float Speed;
	private float run;
    Rigidbody2D rb;
	Animator animator;
	SpriteRenderer spriteRenderer;

    //���۰� ���ÿ� �ʱ�ȭ �ϴ� ��ϵ�
    private void Awake()
	{
		run = 1.0f;
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	//�⺻���� update �Լ��� �� ������ ȣ��ȴ�.
	//�����Ӹ��� �۾����� �ٸ��Ƿ� ȣ�� �ֱⰡ �������� �ʴ�.
	private void Update()
	{
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
		Vector2 nextVec = inputVec * Speed * run * Time.fixedDeltaTime;
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