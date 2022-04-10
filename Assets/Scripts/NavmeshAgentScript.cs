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
    private float seenDist;
    public int AIState;

    public Vector3 guardPosition;
    private float diff;
    public float sightRange;
    private RaycastHit hitThing;
    public bool inLoS;
    private bool hadChased;
    public Vector3 lastSeenAt;
    public float delay = 3f;

    bool isDebugLog = false;

    // This enemy uses an integer to flag the AI state:
    // 0 = Stay still and look around
    // 1 = Head to the player and raycast to check LOS again
    // 2 = Head to player's last know location.
    // 3 = Patrol
    // 4 = Check for LoS

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

    void DelayedSwitch()
    {
        AIState = 3;

    }

    // Update is called once per frame
    void Update () 
    {
        if(isDebugLog)
            Debug.Log(AIState);

        guardPosition = transform.position;

        if (AIState == 1)
        {
            float diff = Vector3.Distance(guardPosition, target.transform.position);
            if (diff <= sightRange)  // if the player is within the guard's maximum vision range
            {
                agent.SetDestination(target.position);
                lastSeenAt = target.transform.position;

                //hadChased = true;
            }
            else
            {
                //AIState = 2;
            }
        }

        if (AIState == 2) // HEAD TO LAST PLACE PLAYER WAS SEEN
        {

            //dist = Vector3.Distance(lastSeenAt, transform.position);
            seenDist = Vector3.Distance(lastSeenAt, guardPosition);
            if (seenDist > 0.1)
            {
                agent.SetDestination(lastSeenAt);
                Invoke("DelayedSwitch", delay);
            }
            else if (seenDist <= 0.1)
            {
                //hadChased = false;
                AIState = 3;
                seenDist = 100;
            }
        }
            
        if (AIState == 3) // ON PATROL
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

/*       if (AIState == 4) // AI CHECK FOR LOS FOR SIGHTING
       {
           Vector3 direction = (target.transform.position - guardPosition).normalized; //direction FROM guard towards player
           Ray g_ray = new Ray(guardPosition, direction);
           Debug.DrawRay(g_ray.origin, g_ray.direction * sightRange); //sightRange was 15

           int layerMask = 1 << 3;
           layerMask = ~layerMask;

           if (Physics.Raycast(guardPosition, direction * diff, out hitThing, layerMask))
           {
               string tag = hitThing.collider.tag;
               string name = hitThing.collider.gameObject.name;
               //Debug.Log("Object = " + name + " tag = " + tag);
               if (hitThing.collider.tag != "PlayerBody")
               {
                   Debug.Log("tag" + tag + "Object =" + name + " Not hitting PlayerBody");
                   inLoS = false;
                   AIState = 3;
               }
               else
               {
                   inLoS = true;
                   AIState = 1;
                   //hadChased = true;
                   //agent.SetDestination(target.position);
                   lastSeenAt = target.transform.position;
               }
           }
       }
*/