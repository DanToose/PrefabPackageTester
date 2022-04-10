using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewZoneCheck : MonoBehaviour
{
    public GameObject parent;

    public Vector3 guardPosition;
    public Transform target;
    private float sightRange;
    private RaycastHit hitThing;
    public bool inLOS = false;
    public bool hasBeenInLOS = false;
    private NavmeshAgentScript parentObject;

    public Vector3 direction;

    public LayerMask hitLayers;

    public GameObject DebugSphere;
 

    

    // Start is called before the first frame update
    void Start()
    {
        hitLayers = LayerMask.GetMask("Player") | LayerMask.GetMask("Default") | LayerMask.GetMask("Environment");
        target = GameObject.FindGameObjectWithTag("Player").transform;
        sightRange = parent.GetComponent<NavmeshAgentScript>().sightRange;
        parentObject = parent.GetComponent<NavmeshAgentScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerBody")
        {
            Debug.Log("Player is in enemy view zone");
            RayCastCheck();

            if (inLOS == true)
            {
                hasBeenInLOS = true;
                parent.gameObject.GetComponent<NavmeshAgentScript>().AIState = 1; // HEAD TOWARDS PLAYER

            }
            else if(hasBeenInLOS)
            {
                hasBeenInLOS = false;
                parent.gameObject.GetComponent<NavmeshAgentScript>().AIState = 2; // HEAD TO LAST PLAYER SEEN
                parentObject.lastSeenAt = target.transform.position;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerBody")
        {
            Debug.Log("Player left enemy view zone");
            inLOS = false;
            hasBeenInLOS = false;

            if (parent.gameObject.GetComponent<NavmeshAgentScript>().AIState == 1)
            {
                parent.gameObject.GetComponent<NavmeshAgentScript>().AIState = 2;
            }
/*            else
            {
                parent.gameObject.GetComponent<NavmeshAgentScript>().AIState = 3;
            } */
        }
    }

    private void RayCastCheck()
    {
        guardPosition = parent.transform.position;
        guardPosition.y += 0.417f;

        direction = (target.transform.position - guardPosition).normalized; //direction FROM guard towards player

       
        Ray g_ray = new Ray(guardPosition, direction);
        Debug.DrawRay(g_ray.origin, g_ray.direction * sightRange); //sightRange was 15

        if (Physics.Raycast(guardPosition, direction * sightRange, out hitThing, sightRange, hitLayers))
        {
            
            string tag = hitThing.collider.tag;
            string name = hitThing.collider.gameObject.name;

            //DebugSphere.transform.position = hitThing.collider.transform.position;


            //Debug.Log("Object = " + name + " tag = " + tag);
            if (hitThing.collider.tag != "PlayerBody")
            {
                Debug.Log("tag" + tag + "Object =" + name + " - Not hitting PlayerBody");
                inLOS = false;
            }
            else
            {
                Debug.Log("tag" + tag + "Object =" + name + " - HITTING PLAYER BODY!!");
                inLOS = true;
            }
        }
        else
        {
            inLOS = false;
            DebugSphere.transform.position =  Vector3.zero;
        }
    }
}
