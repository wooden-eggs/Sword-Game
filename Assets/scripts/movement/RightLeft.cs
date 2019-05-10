using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLeft : MonoBehaviour
{
    //player speed
    public float speed = 10;
    [Range(0.0f,1.0f)]
    public float cutMoveSpeed = 0;
    //saving body
    private Rigidbody2D rb;
    //Saves the sprite renderer component
    private SpriteRenderer spriteRenderer;

    [HideInInspector] public bool facingRight = true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxisRaw("Horizontal");
        if (move.x != 0)
        {
            rb.velocity = new Vector2(move.x * speed, rb.velocity.y);
        }
        Debug.DrawLine(rb.position,rb.position + rb.velocity,Color.green);

        if (move.x > 0 && !facingRight)
            Flip();
        else if (move.x < 0 && facingRight)
            Flip();

        if(Mathf.Abs(move.x) == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x*cutMoveSpeed, rb.velocity.y);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}
