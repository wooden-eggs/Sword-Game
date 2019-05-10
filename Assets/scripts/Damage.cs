using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damageAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)                    // used for things like bullets, which are triggers.  
    {
        //Destroy(gameObject);
        if (collision.gameObject.GetComponentInParent<Health>() != null)
        {
            collision.gameObject.GetComponentInParent<Health>().ApplyDamage(damageAmount);
        }
    }
}
