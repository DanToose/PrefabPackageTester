using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFireSpell : MonoBehaviour
{
    public GameObject PlayerEntity;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerEntity == null)
        {
            Debug.Log("WARNING - No Player assigned for Collectable Fire Spell!");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PlayerBody")
        {
            PlayerEntity.GetComponent<PlayerInventory>().fireSpellCount = +1;
        }
    }
}
