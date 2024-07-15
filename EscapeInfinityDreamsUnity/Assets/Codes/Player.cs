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

	private void Awake()
	{
		run = 1.0f;
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		inputVec.x = Input.GetAxisRaw("Horizontal");
		inputVec.y = Input.GetAxisRaw("Vertical");

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

	private void FixedUpdate()
	{
		Vector2 nextVec = inputVec * Speed * run * Time.fixedDeltaTime;
		rb.MovePosition(rb.position + nextVec);
		spriteRenderer.flipX = inputVec.x >= 0 ? false : true;
	}

	private void LateUpdate()
	{
		animator.SetFloat("Speed", inputVec.magnitude);
	}
}