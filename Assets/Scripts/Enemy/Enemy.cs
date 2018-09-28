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

        // Indetify look target (make sure only the head is looking)
        target = FindObjectOfType<Camera>().transform.position;




        // Initialize circling state.
        if (Vector3.Distance(gameObject.transform.position, target) <= distanceToEngage && currentState != States.Attack)
        {
            currentState = States.Circling;
        }

        // Attack function
        if (currentState == States.Attack)
        {
            // Get target

            // Path to target
            agent.SetDestination(target);
            // When within range start attacking.
        }

        // Circle state functions
        if(currentState == States.Circling)
        {


            // Adjust speed for circling state.

            // Look at the target
            Vector3 relativePosition = target - gameObject.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePosition);
            transform.rotation = rotation;

            // Circle around the target
            Vector3 direction;
            direction = Vector3.Cross(relativePosition, Vector3.up);
            agent.SetDestination(gameObject.transform.position + direction / 3);

            // Count to an interval and after that begin to attack.

            StartCoroutine(InitializeAttack());

        }

        


    }

    IEnumerator InitializeAttack()
    {

        float randomTime;
        randomTime = Random.Range(2, 5);

        yield return new WaitForSeconds(randomTime);

        currentState = States.Attack;
        
    }


}
