using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth;
    public Text healthText;
    public Respawner respawn;
    public float respawnDelay = 3.0f;
    public float playerMaxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerMaxHealth;
        healthText.text = "Health: " + playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + playerHealth;

            if (playerHealth <= 0)
            {
                playerHealth = 0;
                playerDeath();
            }
        }
    }

    public void playerDeath()
    {
        // death stuff
        // Set a delay
        Invoke("RespawnFromDeath", respawnDelay);

    }

    void RespawnFromDeath()
    {
        respawn.RespawnPlayer();
        playerHealth = playerMaxHealth;
    }
}
