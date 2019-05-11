using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    //***** walljump is dependant on using ground layers, if you do not touch ground you will not be able to walljump*****\\
    //saving collider
    private BoxCollider2D col;
    //checks if you can jump again
    private bool onWall;
    //for collision checks
    LayerMask mask;
    private float colliderWidth;
    private Rigidbody2D rb;
    //set wall jump speed
    public float jumpspeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        colliderWidth = col.bounds.extents.x;
        mask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 jump = Vector2.zero;
        jump.y = Input.GetAxis("Jump");
        onWall = isClimbing();
        if (jump.y > 0 && onWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump.y * jumpspeed);
        }
    }
    //checks wether player is touching a wall
    private bool isClimbing()
    {
        if (Physics2D.Raycast(transform.position, Vector3.right, colliderWidth + 0.2f, mask)|| Physics2D.Raycast(transform.position, Vector3.left, colliderWidth + 0.1f, mask))
            return true;
        return false;
    }
}
