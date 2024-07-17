using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat : MonoBehaviour
{
    public Transform player;
    public float followDistance;
    public float cat_speed;
    private float run;
    SpriteRenderer spriteRenderer;
    Animator anim;
    Animator playerAnimator;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        anim = GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
        run = 1.0f;
    }

    void FixedUpdate()
    {
        Vector2 direction = player.position - transform.position;
        float distance = direction.magnitude;

        if(distance>followDistance)
        {
            Vector2 moveDir = direction.normalized * cat_speed * run * Time.deltaTime;
            transform.position = (Vector2)transform.position + moveDir;

            bool PlayerRun = playerAnimator.GetBool("run");
            anim.SetBool("cat-run", PlayerRun);
            if (PlayerRun == true) run = 1.7f;
            else run = 1.0f;

            float speed = moveDir.magnitude / Time.deltaTime;
            anim.SetFloat("cat-speed", speed);

            if (moveDir.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (moveDir.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            anim.SetFloat("cat-speed", 0);
            anim.SetBool("cat-run", false);
        }
    }
}
