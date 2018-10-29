using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    //Public
    public GameObject tracker;
	public GameObject player;
    public float maxAttackRange = 5.0f;
	public float rotationSpeed = 10.0f;
	
    //Private
    private int currentEnemyState = 0;          //Start with defend
    enum enemyState { Defend, Attack, Dodge};
    private NavMeshAgent aiAgent;
  
    // Use this for initialization
    void Start()
    {
        aiAgent = GetComponent<NavMeshAgent>();
		
    }

    // Update is called once per frame
    void Update()
    {
		FacePlayer()
        EnemyBehaviour();
    }

    void EnemyBehaviour()
    {
        switch(currentEnemyState)
        {
            case 2:
                Defend();
                break;
            case 1:
                Attack();
                break;
            default:
                Defend();
                break;
        }
    }

    void Defend()
    {
        float distanceToEnemy = Vector3.Distance(tracker.transform.position, transform.position); //Determine Distance

        if (distanceToEnemy > maxAttackRange)
        {
            aiAgent.SetDestination = tracker.transform.position; //Chase Player
			
     
        }
        else
        {
            //aiAgent.destination = transform.position;         //Stop chasing 
            StartCoroutine(GenerateNewDestination());
            
        }


        print(distanceToEnemy);
    }

    IEnumerator GenerateNewDestination()
    {
        aiAgent.destination = tracker.transform.position + Random.onUnitSphere * 15;
        yield return new WaitForSeconds(2f);
    }

    void Attack()
    {
        //print("Attack");
    }


    void Dodge()
    {
       // print("Dodge");
    }
	
	
	void OnTriggerStay(Collider col)
	{
		if(col.gameObject.tag == "stateChanger"){
			
			// Switch states to attack when we are inside this collider
			currentEnemyState = 1; // Attacking state.
			
		}
	}
	
	private void FacePlayer()
	{
			Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
	}
}
