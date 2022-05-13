using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    //public GameObject[] CPList;

    public List<GameObject> CPList = new List<GameObject>();
    public GameObject currentCheckpoint;
    private Transform checkpointLocation;
    private GameObject player;
    private GameObject startingPoint;

    public Material activeMaterial;
    public Material inactiveMaterial;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
        startingPoint = GameObject.FindGameObjectWithTag("StartPoint");
        currentCheckpoint = startingPoint;

        CPList.AddRange(GameObject.FindGameObjectsWithTag("CheckPoint"));

        InitialSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitialSpawn()
    {
        player.gameObject.transform.position = startingPoint.gameObject.transform.position;
    }

    public void RespawnPlayer()
    {
        checkpointLocation = currentCheckpoint.transform;
        player.transform.position = checkpointLocation.position;
    }

    public void UpdateCheckPoints()
    {
        Debug.Log("UpdateCheckPoints was called");
        foreach (GameObject l in CPList)
        {
            if (l.gameObject.GetComponent<CheckPoint>().isCheckpoint == false)
            {
                l.gameObject.GetComponent<MeshRenderer>().material = inactiveMaterial;
            }
            else
            {
                l.gameObject.GetComponent<MeshRenderer>().material = activeMaterial;
            }
        }
    }
}
