using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public GameObject currentCheckpoint;
    private Transform checkpointLocation;
    private GameObject player;
    private GameObject startingPoint;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startingPoint = GameObject.FindGameObjectWithTag("StartPoint");
        currentCheckpoint = startingPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RespawnPlayer()
    {
        checkpointLocation = currentCheckpoint.transform;
        player.transform.position = checkpointLocation.position;
    }
}
