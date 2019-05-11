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
    private int dashDir;
    
    //saving body
    private Rigidbody2D rb;

    private float dashTime;
    private bool isDashing = false;
    private bool onCooldown = false;
    private bool Fire1axisInUse;
    [HideInInspector] public bool activeDashing = false;
    private LayerMask mask;
    RaycastHit2D collisionBuddy;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mask = LayerMask.GetMask("Secret");
    }

    void FixedUpdate()
    {
        if(Input.GetAxis("Fire1")!=0 && !Fire1axisInUse && !isDashing && !onCooldown)
        {
            dashDir = DoDash(dashSpeed);
            Fire1axisInUse = true;
            activeDashing = true;
        }
        if (Input.GetAxis("Fire1") == 0)
            Fire1axisInUse = false;

        if (dashTime>0)
        {
            rb.velocity = new Vector2(dashDir * dashSpeed, 0);
            dashTime -= Time.deltaTime;
        }
        if(dashTime <= 0 && isDashing)
        {
            rb.velocity = Vector2.zero;
            isDashing = false;
            onCooldown = true;
            activeDashing = false;
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
    private int DoDash(float speed)
    {
        cooldownCounter = cooldown;
        dashTime = dashDistance / dashSpeed;
        isDashing = true;
        if (gameObject.GetComponent<RightLeft>().facingRight)
            return 1;
        return -1;
    }
}
