using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float healthPoints = 1f;
    public float respawnHealthPoints = 1f;      //base health points

    public enum Armour { AddedHealth,ReducedDamage}
    public Armour armourMethod = Armour.AddedHealth;
    public float armourPoints = 0f;

    public bool isImmune = false;

    public string SceneToLoad = "";

    private Vector3 respawnPosition;
    private Quaternion respawnRotation;

    public Slider HealthBar;
    public Image armour;

    // Start is called before the first frame update
    void Start()
    {
        // store initial position as respawn location
        respawnPosition = transform.position;
        respawnRotation = transform.rotation;

        if (SceneToLoad == "") // default to current scene 
        {
            SceneToLoad = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Update the UI
        if (armourPoints == 0)
            armour.gameObject.SetActive(false);
        else
            armour.gameObject.SetActive(true);
        armour.GetComponentInChildren<Text>().text = "Armour" + "\n" + armourPoints;
        HealthBar.value = healthPoints * 100 / respawnHealthPoints;
        HealthBar.GetComponentInChildren<Text>().text = "Health - " + healthPoints + " / " + respawnHealthPoints;

        // if the object is 'dead'
        if (healthPoints <= 0)
        {
            transform.position = respawnPosition;   // reset the player to respawn position
            transform.rotation = respawnRotation;
            healthPoints = respawnHealthPoints; // give the player full health again
        }
    }

    public void ApplyDamage(float amount)
    {
        if (!isImmune)
        {
            switch(armourMethod)
            {
                case Armour.AddedHealth:
                    if (armourPoints == 0)
                    {
                        healthPoints = healthPoints - amount;
                    }
                    else if (amount <= armourPoints)
                    {
                        armourPoints = armourPoints - amount;
                    }
                    else
                    {
                        healthPoints = healthPoints + armourPoints - amount;
                        armourPoints = 0;
                    }
                    break;
                case Armour.ReducedDamage:
                    if(amount- armourPoints > 0)
                        healthPoints = healthPoints - (amount - armourPoints);
                    break;
            }
        }
    }

    public void ApplyHeal(float amount)
    {
        if (healthPoints + amount < respawnHealthPoints)
            healthPoints = healthPoints + amount;
        else
            healthPoints = respawnHealthPoints;
    }

    public void AddArmour(float amount)
    {
        armourPoints = armourPoints + amount;
    }

    public void updateRespawn(Vector3 newRespawnPosition, Quaternion newRespawnRotation)
    {
        respawnPosition = newRespawnPosition;
        respawnRotation = newRespawnRotation;
    }
}
