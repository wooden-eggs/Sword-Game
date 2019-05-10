using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float healthPoints = 1f;
    public float respawnHealthPoints = 1f;      //base health points

    public bool isImmune = false;

    public string SceneToLoad = "";

    private Vector3 respawnPosition;
    private Quaternion respawnRotation;

    public Slider HealthBar;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.value = healthPoints * 100 / respawnHealthPoints;
        HealthBar.GetComponentInChildren<Text>().text = "Health - " + healthPoints + " / " + respawnHealthPoints;
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
        if (healthPoints <= 0)
        {               // if the object is 'dead'
            transform.position = respawnPosition;   // reset the player to respawn position
            transform.rotation = respawnRotation;
            healthPoints = respawnHealthPoints; // give the player full health again
            HealthBar.value = healthPoints * 100 / respawnHealthPoints;
            HealthBar.GetComponentInChildren<Text>().text = "Health - " + healthPoints + " / " + respawnHealthPoints;
        }
    }
    public void ApplyDamage(float amount)
    {
        if (!isImmune)
        {
            healthPoints = healthPoints - amount;
            HealthBar.value = healthPoints * 100 / respawnHealthPoints;
            HealthBar.GetComponentInChildren<Text>().text = "Health - " + healthPoints + " / " + respawnHealthPoints;
        }
    }

    public void ApplyHeal(float amount)
    {
        healthPoints = healthPoints + amount;
    }

    public void updateRespawn(Vector3 newRespawnPosition, Quaternion newRespawnRotation)
    {
        respawnPosition = newRespawnPosition;
        respawnRotation = newRespawnRotation;
    }
}
