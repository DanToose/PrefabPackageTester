using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool instaDeathAttacker;
    public float attackRate;
    public float damageMulitplier = 1.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        attackRate = 0.05;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (instaDeathAttacker == true)
            {
                other.gameObject.GetComponent<PlayerHealth>().playerDeath();
            }
            else
            {
                timer = timer + Time.deltaTime;

                if (timer >= attackRate)
                {
                    health = health - 1 * damageMultiplier;
                    timer = 0.0f;
                }
            }
            
        }
    }
}
