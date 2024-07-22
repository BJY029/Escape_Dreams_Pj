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

    //시작과 동시에 초기화 하는 목록들
    private void Awake()
	{
		run = 1.0f;
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	//기본적인 update 함수로 매 프레임 호출된다.
	//프레임마다 작업량이 다르므로 호출 주기가 일정하지 않다.
	private void Update()
	{
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
	}

	//함수 호출 간격이 일정하도록 보장하는 update 함수다.
	//따라서 물리 계산 및 업데이트, Ray 처리에 주로 사용된다.
	private void FixedUpdate()
	{
		//플레이어 움직임 구현
		Vector2 nextVec = inputVec * Speed * run * Time.fixedDeltaTime;
		rb.MovePosition(rb.position + nextVec);

		//플레이어의 이동방향에 맞게 스프라이트가 바라보는 방향 수정
		if (inputVec.x > 0) spriteRenderer.flipX = false;
		else if (inputVec.x < 0) spriteRenderer.flipX = true;
	}

	//Update가 완전히 끝난 후 프레임당 한 번 호출되는 함수다.
	//플레이어의 움직임을 Update에서 완료 후, 이동한 위치에 따라 카메라의 위치를 수정하는
	//방식을 구현할 때 사용
	private void LateUpdate()
	{
		//애니메이션을 위한 연산
		animator.SetFloat("Speed", inputVec.magnitude);
	}
}