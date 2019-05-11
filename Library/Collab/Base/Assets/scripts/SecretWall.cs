using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretWall : MonoBehaviour
{
    //checks if player is dashing
    private bool isDashing = false;
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    //checks if dashing to remorph the wall
    private void Update()
    {
        if (!player.GetComponentInParent<Dash>().activeDashing)
            GetComponent<BoxCollider2D>().enabled = true;

    }
    //if player is dashing while colliding lets him pass through
    void OnCollisionEnter2D(Collision2D collision)
    {
        isDashing = collision.gameObject.GetComponentInParent<Dash>().activeDashing;
        if (collision.gameObject.name.Equals("Player") && isDashing)
            GetComponent<BoxCollider2D>().enabled = false;
    }
    //if player is dashing while staying in touch with wall lets him pass through
    private void OnCollisionStay2D(Collision2D collision)
    {
        isDashing = collision.gameObject.GetComponentInParent<Dash>().activeDashing;
        if (collision.gameObject.name.Equals("Player")&&isDashing)
                GetComponent<BoxCollider2D>().enabled = false;
    }
}
