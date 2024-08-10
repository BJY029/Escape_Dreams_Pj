using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class PlayerControllerInB : MonoBehaviour
{
	private Vector2 inputVec;
	public float Speed;
	private float run;
	private float direction;
	private float flag;

	Rigidbody2D rb;
	public Animator animator;	
	SpriteRenderer spriteRenderer;
	private BoxCollider2D boxCollider;

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

		animator.SetBool("IsAlive", true);
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
		animator.SetFloat("Speed", inputVec.magnitude);
	}

	private void FixedUpdate()
	{
		Vector2 nextVec = inputVec * flag * Speed * run * direction * Time.fixedDeltaTime;
		rb.MovePosition(rb.position + nextVec);

		if (inputVec.x > 0) spriteRenderer.flipX = false;
		else if(inputVec.x < 0)spriteRenderer.flipX = true;
		
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
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.CompareTag("WareWolfSpawnTime"))
		{
			collision.gameObject.SetActive(false);
		}
	}
}
