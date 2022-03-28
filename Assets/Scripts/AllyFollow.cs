using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyFollow : MonoBehaviour
{
    public bool followingPlayer;
//  public bool toggleState = false;
    public Transform target;
    NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        followingPlayer = false;
        agent = GetComponent<NavMeshAgent>();
        //target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void followPlayerToggle()
    {
        //Debug.Log("followPlayerToggle triggered");
        if (followingPlayer == false)
        {
            followingPlayer = true;
        }
        else
        {
            followingPlayer = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (followingPlayer == true)
        {
            //Debug.Log("Ally destination set");
            agent.SetDestination(target.position);
        }
    }

}
