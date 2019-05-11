using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public float healAmount = 0;
    public int numberOfPotions = 0;
    private bool Fire2axisInUse;

    void Update()
    {
        if(gameObject.GetComponent<Health>() != null)
        {
            if (numberOfPotions > 0)
            {
                if (Input.GetAxis("Fire2") != 0 && !Fire2axisInUse)
                {
                    if (gameObject.GetComponent<Health>().healthPoints != gameObject.GetComponent<Health>().respawnHealthPoints)
                    {
                        gameObject.GetComponent<Health>().ApplyHeal(healAmount);
                        Fire2axisInUse = true;
                        numberOfPotions--;
                    }
                }
                if (Input.GetAxis("Fire2") == 0)
                    Fire2axisInUse = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)  
    {
        if (numberOfPotions > 0)
        {
            if (collision.gameObject.GetComponentInParent<Health>() != null)
            {
                if (collision.gameObject.GetComponentInParent<Health>().healthPoints != collision.gameObject.GetComponentInParent<Health>().respawnHealthPoints)
                {
                    collision.gameObject.GetComponentInParent<Health>().ApplyHeal(healAmount);
                    numberOfPotions--;
                }
            }
            if(numberOfPotions == 0)
                Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<Health>() != null)
            if (numberOfPotions == 0)
                Destroy(gameObject);
    }
}
