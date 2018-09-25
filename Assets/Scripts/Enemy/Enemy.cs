using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float distanceToEngage = 10f;
    public float distanceToCircle = 5f;
    public NavMeshAgent agent;
    public Vector3 target;
    public string targetID;

    public enum States
    {
        Circling, Attack, Idle
    }

    public States currentState;

    private void Awake()
    {
        targetID = Resources.Load("Prefabs/Characters/PlayerCharacter").name as string;
    }

    void Start()
    {
        Debug.Log(targetID);

        agent = gameObject.GetComponent<NavMeshAgent>();
        // Default state.
        currentState = States.Idle;
    }

    
    void Update()
    {
        // Indetify target
        target = GameObject.Find(targetID).GetComponent<Transform>().position;
        

        // Initialize circling state.
        if(Vector3.Distance(gameObject.transform.position, target) <= distanceToEngage)
        {
            currentState = States.Circling;
        }

        // Initialize attack state.
        if(currentState == States.Circling)
        {

        }

        // Circle state functions
        if(currentState == States.Circling)
        {
            // Look at the target
            Vector3 relativePosition = target - gameObject.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePosition);
            transform.rotation = rotation;

            // Circle around the target


        }




    }



}
