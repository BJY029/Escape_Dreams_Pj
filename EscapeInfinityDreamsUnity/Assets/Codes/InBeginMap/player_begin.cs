using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class player_begin : MonoBehaviour
{
    private Vector2 inputVec;
    public float Speed;
    private float direction;

	private bool isWalking;
	public float WalkSoundInterval = 0.5f;
	private float WalkSoundTimer;
    public AudioControllerInBEGIN audioSource;

	Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Awake()
    { 
        direction = 1.0f;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        animator.SetBool("IsAlive", true);
		isWalking = false;
	}

    public void InitAll()
    {
        spriteRenderer.flipX = true;
    }

    private void Update()
    {
        UpdateColliderSize();

        inputVec.x = Input.GetAxisRaw("Horizontal");

		//우선 걷거나 뛰는지를 확인한다.
		if (Mathf.Abs(inputVec.x) > 0.1f)
		{
            isWalking = true;	
        }
		else
		{
			isWalking = false;
		}

		//걷는다면
		if (isWalking)
		{
			//걷기 주기 마다 효과음을 재생한다(0.5초)
			WalkSoundTimer -= Time.deltaTime;
			if (WalkSoundTimer <= 0f)
			{
				StartCoroutine(audioSource.playGrassWalkSound());
				WalkSoundTimer = WalkSoundInterval;
			}
		}
		else
		{
			WalkSoundTimer = 0f;
		}

		animator.SetFloat("Speed", inputVec.magnitude);


    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec * Speed * direction * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);

        if (inputVec.x > 0) spriteRenderer.flipX = true;
        else if (inputVec.x < 0) spriteRenderer.flipX = false;

    }

    void UpdateColliderSize()
    {
        if (spriteRenderer != null && boxCollider != null)
        {
            boxCollider.size = spriteRenderer.bounds.size;
            boxCollider.offset = spriteRenderer.bounds.center - transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
