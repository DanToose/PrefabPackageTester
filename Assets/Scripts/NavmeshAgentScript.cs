using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshAgentScript : MonoBehaviour {

    public Transform target;
    NavMeshAgent agent;
    public GameObject patrolTarget1;
    public GameObject patrolTarget2;
    public GameObject patrolTarget3;
    public GameObject patrolTarget4;
    public List<GameObject> waypoints;
    private Transform currentDestination;
    private int PatrolPoint;
    private float dist;
    public int AIState;

    public Vector3 guardPosition;
    private float diff;
    public float sightRange;
    private RaycastHit hitThing;
    public bool inLoS;
    private Vector3 lastSeenAt;

    // This enemy uses an integer to flag the AI state:
    // 0 = Stay still and look around
    // 1 = Head to the player and raycast to them for LOS check.
    // 2 = Head to player's last know location.
    // 3 = Patrol

    // Use this for initialization
    void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        PatrolPoint = 0;
        waypoints.Add(patrolTarget1);
        waypoints.Add(patrolTarget2);
        waypoints.Add(patrolTarget3);
        waypoints.Add(patrolTarget4);
    }
	
	// Update is called once per frame
	void Update () 
    {
        guardPosition = transform.position;

        if (AIState == 1)
        {
            float diff = Vector3.Distance(guardPosition, target.transform.position); //distance from guard to player
            if (diff <= sightRange)  // if the player is within the guard's maximum vision range
            {
                Vector3 direction = (target.transform.position - guardPosition).normalized; //direction FROM guard towards player

                Ray g_ray = new Ray(guardPosition, direction);
                Debug.DrawRay(g_ray.origin, g_ray.direction * sightRange); //sightRange was 15
                if (Physics.Raycast(guardPosition, direction * diff, out hitThing))
                {
                    string tag = hitThing.collider.tag;
                    string name = hitThing.collider.gameObject.name;
                    Debug.Log("tag" + tag + "Object =" +name);
                    if (hitThing.collider.tag != "Player")
                    {
                        inLoS = false;
                    }
                    else
                    {
                        inLoS = true;
                        agent.SetDestination(target.position);
                        lastSeenAt = target.transform.position;
                    }
                }
            }
            else
            {
                    inLoS =  false;
                    AIState = 2; // short hack to enter patrol

            }
        }

        if (AIState == 2)
        {

            dist = Vector3.Distance(lastSeenAt, transform.position);
            if (dist > 0.1)
            {
                agent.SetDestination(lastSeenAt);
            }
            else if (dist <= 0.1)
            {
                AIState = 3;
            }
        }
            
        if (AIState == 3)
        {
            currentDestination = waypoints[PatrolPoint].transform;
            dist = Vector3.Distance(currentDestination.position, transform.position);

            if (dist > 0.2)
            {
                agent.SetDestination(currentDestination.position);
            }
            else if (dist <= 0.2 && PatrolPoint == 3)
            {
                PatrolPoint = 0;
            }

            else if (dist <= 0.2 && PatrolPoint < 3)
            {
                PatrolPoint++;
            }
        }
	}
}
