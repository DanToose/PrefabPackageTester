using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewZoneCheck : MonoBehaviour
{
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (parent.)
            
            
            Debug.Log("Player entered trigger zone");
            parent.gameObject.GetComponent<NavmeshAgentScript>().AIState = 1;
        }
    }
}
