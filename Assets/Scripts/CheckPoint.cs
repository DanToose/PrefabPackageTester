using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool isStartpoint;
    public GameObject player;
    private Transform thisPoint;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (isStartpoint)
        {
            thisPoint.transform.position = player.gameObject.transform.position;
        }
        //thisPoint = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //isCheckpoint = true;
            player.gameObject.GetComponent<Respawner>().currentCheckpoint = gameObject;
        }
    }
}
