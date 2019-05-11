using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    //***** jump is dependant on using ground layers, if you do not touch ground you will not be able to jump*****\\
    //saving collider
    private BoxCollider2D col;
    //checks if you can jump again
    public bool grounded;
    LayerMask mask;
    private bool lastFrameReleaseButton = false;
    private Rigidbody2D rb;
    //set jump speed and how fast you fall when stopped pressing
    public float jumpspeed = 10;
    public float jumpDecay = 0.5f;
    private float colliderHeight;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        colliderHeight = col.bounds.extents.y;
        mask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 jump = Vector2.zero;
        jump.y = Input.GetAxis("Jump");
        //checks if you touch a ground layer object
        //if you want to enable wall climbing switch between two statements
        //grounded = col.IsTouchingLayers(mask);
        grounded = isGrounded();
        if (jump.y > 0 && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump.y * jumpspeed);
            lastFrameReleaseButton = false;
        }
        if (jump.y == 0 && rb.velocity.y > 0 && !lastFrameReleaseButton)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpDecay);
            lastFrameReleaseButton = true;
        }

        //I've added a jumping animation for gerzon
        if(grounded)
            gameObject.GetComponent<Animator>().SetBool("inAir", false);
        else
            gameObject.GetComponent<Animator>().SetBool("inAir", true);
    }
    private bool isGrounded ()
    {
        return Physics2D.Raycast(transform.position, Vector3.down, colliderHeight + 0.2f, mask);
    }
}
