using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    //Dashing numbers
    public float dashSpeed = 0;
    public float dashDistance = 0;
    public float cooldown = 0;
    private float cooldownCounter = 0; 
    private int dashDir; //Diraction of the dash

    //saving body
    private Rigidbody2D rb;

    private float dashTime; //the time you change the velocity (Distance / speed)
    [HideInInspector]public bool isDashing= false; 
    private bool onCooldown = false;
    private bool Fire1axisInUse; //flag so the fire1 Axis acts like button down

    //Slaming
    public float slamSpeed = 0;
    [HideInInspector] public bool isSlaming = false;

    private LayerMask mask;
    RaycastHit2D collisionBuddy;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mask = LayerMask.GetMask("Secret");
        isSlaming = false;
    }

    void FixedUpdate()
    {
        //you can dash only if you pressed the fire1 button, you didnt pressed down, you are not already dashing 
        //and you are not on cooldown
        if (Input.GetAxis("Fire1") != 0 && Input.GetAxis("Vertical") > -0.01 && !Fire1axisInUse && !isDashing && !onCooldown)
        {
            dashDir = DoDash(dashSpeed);
            Fire1axisInUse = true;
            GetComponent<BoxCollider2D>().size = new Vector2(0.3f, 0.07f);
            GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.01f);
        }

        //Slaming
        //you can Slam if you are in air and pressed Fire1 + down
        if (!gameObject.GetComponent<Jump>().grounded)
        {
            if (Input.GetAxis("Fire1") != 0 && Input.GetAxis("Vertical") < 0 && !Fire1axisInUse && !isSlaming)
            {
                isSlaming = true;
                Fire1axisInUse = true;
            }
            if (isSlaming)
                rb.velocity = new Vector2(0, -slamSpeed);
        }
        //as long as you are in the air and you started slaming you will slam until you hit the ground
        else
        {
            isSlaming = false;
        }

        //reset the flag
        if (Input.GetAxis("Fire1") == 0)
        {
            Fire1axisInUse = false;
        }

        //Change dash Velocity for the duration of dash Time
        if (dashTime>0)
        {
            rb.velocity = new Vector2(dashDir * dashSpeed, 0);
            dashTime -= Time.deltaTime;
        }

        //When the timer ends, set cooldown and stop the dash
        if(dashTime <= 0 && isDashing)
        {
            rb.velocity = Vector2.zero;
            isDashing = false;
            gameObject.GetComponent<Animator>().SetBool("isDashing", false);
            onCooldown = true;
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.02f, -0.02f);
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.07f, 0.3f);
        }

        if(onCooldown)
        {
            cooldownCounter -= Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (cooldownCounter <= 0)
        {
            onCooldown = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)  
    {

        if (collision.gameObject.name.Equals("SecretWall"))
        {
            if (isDashing)
                collision.gameObject.GetComponentInParent<Collider2D>().enabled = false;
        }
    }

    //Calculate the dashing time, set the cooldown and set the diraction of the dash
    private int DoDash(float speed)
    {
        cooldownCounter = cooldown;
        dashTime = dashDistance / dashSpeed;
        isDashing = true;
        gameObject.GetComponent<Animator>().SetBool("isDashing", true);
        if (gameObject.GetComponent<RightLeft>().facingRight)
            return 1;
        return -1;
    }
}
