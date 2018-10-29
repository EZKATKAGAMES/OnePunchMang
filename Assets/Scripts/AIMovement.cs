using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    //public Collider CollisionPath;
    public GameObject player;
    public float rotationSpeed = 10.0f;
    public GameObject colliderTracker;
    private NavMeshAgent aiAgent;
    private Vector3 moveLocation;
    private RaycastHit m_Hit;

    // Use this for initialization
    void Start()
    {
        aiAgent = GetComponent<NavMeshAgent>();     //Get navmesh agent
        moveLocation = new Vector3(Random.onUnitSphere.x, 0f, Random.onUnitSphere.z) * 1.5f + colliderTracker.transform.position;   //Set an initial move location
    }

    //Debug the move location
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(moveLocation, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        FacePlayer();
        Defend();
    }

    //Trigger Collision to see what side of the enemy we are hitting
    //Upgrade this system to use Raycast Box [This is to improve it for the final release]
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "FrontTracker")
        {
            colliderTracker = col.gameObject;
            moveLocation = new Vector3(Random.onUnitSphere.x, 0f, Random.onUnitSphere.z) * 1.5f + colliderTracker.transform.position;
            Debug.Log("Front hit");
        }
        
        if (col.gameObject.tag == "RearTracker")
        {
            colliderTracker = col.gameObject;
            moveLocation = new Vector3(Random.onUnitSphere.x, 0f, Random.onUnitSphere.z) * 1.5f + colliderTracker.transform.position;
            Debug.Log("Rear");
        }

        if (col.gameObject.tag == "LeftTracker")
        {
            colliderTracker = col.gameObject;
            moveLocation = new Vector3(Random.onUnitSphere.x, 0f, Random.onUnitSphere.z) * 1.5f + colliderTracker.transform.position;
            Debug.Log("Left");
        }

        if (col.gameObject.tag == "RightTracker")
        {
            colliderTracker = col.gameObject;
            moveLocation = new Vector3(Random.onUnitSphere.x, 0f, Random.onUnitSphere.z) * 1.5f + colliderTracker.transform.position;
            Debug.Log("Right");
        }
    }


    private void Defend()
    {
        if (Vector3.Distance(transform.position, moveLocation) > 1.0f)
        {
            aiAgent.speed = 2.5f;
            aiAgent.destination = moveLocation;
        }
        else
        {
            aiAgent.speed = 0.0f;
            while (Vector3.Distance(transform.position, moveLocation) < 1f)
            {
                moveLocation = new Vector3(Random.onUnitSphere.x, 0f, Random.onUnitSphere.z) * 1.5f + colliderTracker.transform.position;   //Generate a new location when close enough to the previous one
            }
        }
    }

    //Face player smoothly
    private void FacePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
