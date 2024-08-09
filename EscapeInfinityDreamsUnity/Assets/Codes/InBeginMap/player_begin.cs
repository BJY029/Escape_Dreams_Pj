using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class player_begin : MonoBehaviour
{
    private Vector2 inputVec;
    public float Speed;
    private float run;
    private float direction;
    

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        run = 1.0f;
        direction = 1.0f;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        animator.SetBool("IsAlive", true);
    }

    public void InitAll()
    {
        spriteRenderer.flipX = true;
    }

    private void Update()
    {
        UpdateColliderSize();

        inputVec.x = Input.GetAxisRaw("Horizontal");
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
        animator.SetFloat("Speed", inputVec.magnitude);
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec * Speed * run * direction * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);

        if (inputVec.x > 0) spriteRenderer.flipX = false;
        else if (inputVec.x < 0) spriteRenderer.flipX = true;

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
