using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakGround : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public BoxCollider2D trigger;
    private Color color;

    void Start()
    {
        color = gameObject.GetComponent<SpriteRenderer>().color;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            if(collision.gameObject.GetComponent<Dash>() != null)
            {
                if(collision.gameObject.GetComponent<Dash>().isSlaming)
                {
                    boxCollider.isTrigger = true;
                    color.a = 0.1f;
                    gameObject.GetComponent<SpriteRenderer>().color = color;
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        boxCollider.isTrigger = false;
        color.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }
}
