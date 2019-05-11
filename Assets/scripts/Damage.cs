using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damageAmount = 0;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<Health>() != null)
        {
            collision.gameObject.GetComponentInParent<Health>().ApplyDamage(damageAmount);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject);
        if (collision.gameObject.GetComponentInParent<Health>() != null)
        {
            collision.gameObject.GetComponentInParent<Health>().ApplyDamage(damageAmount);
        }
    }
}
