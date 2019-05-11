﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    //***** jump is dependant on using ground layers, if you do not touch ground you will not be able to jump*****\\
    //saving collider
    private BoxCollider2D col;
    //checks if you can jump again
    private bool grounded;
    private float colliderHeight;
    LayerMask mask;
    private bool lastFrameReleaseButton = false;
    private Rigidbody2D rb;
    //set jump speed and how fast you fall when stopped pressing
    public float jumpspeed = 15;
    public float jumpDecay = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        mask = LayerMask.GetMask("Ground");
        colliderHeight = col.bounds.extents.y;
        print(colliderHeight);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 jump = Vector2.zero;
        jump.y = Input.GetAxis("Jump");
        //checks if there is a ground layer object below you
        grounded = isGrounded();
        //make player jump if conditions are met
        if (jump.y > 0 && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump.y * jumpspeed);
            lastFrameReleaseButton = false;
        }
        //make player start falling when he stops pressing jump
        if (jump.y == 0 && rb.velocity.y > 0 && !lastFrameReleaseButton)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpDecay);
            lastFrameReleaseButton = true;
        }
    }
    public bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector3.down, colliderHeight+0.2f,mask);
    }
}
