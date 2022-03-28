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
    private bool inLOS;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        sightRange = parent.GetComponent<NavmeshAgentScript>().sightRange;
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
                parent.gameObject.GetComponent<NavmeshAgentScript>().AIState = 1;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerBody")
        {
            Debug.Log("Player left enemy view zone");
            inLOS = false;

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

        Vector3 direction = (target.transform.position - guardPosition).normalized; //direction FROM guard towards player
        Ray g_ray = new Ray(guardPosition, direction);
        Debug.DrawRay(g_ray.origin, g_ray.direction * sightRange); //sightRange was 15

        int layerMask = 1 << 3;
        layerMask = ~layerMask;

        if (Physics.Raycast(guardPosition, direction * sightRange, out hitThing, layerMask))
        {
            string tag = hitThing.collider.tag;
            string name = hitThing.collider.gameObject.name;
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
    }
}
