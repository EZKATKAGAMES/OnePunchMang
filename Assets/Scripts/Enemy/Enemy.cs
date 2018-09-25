using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float distanceToEngage = 10f;
    public float distanceTilCircle = 5f;
    public NavMeshAgent agent;
    public Vector3 target;
    public string targetID;

    #region Circular Oscilation
    public float rotationSpeed = (2 * Mathf.PI) / 5; // 5 = seconds to complete.
    float angle = 0;
    float radius = 5;

    float x, z;



    #endregion

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
        angle += rotationSpeed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
        x = Mathf.Cos(angle) * radius;
        z = Mathf.Sin(angle) * radius;

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
