using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    //Public
    public GameObject enemy;
    public float maxAttackRange = 5.0f;

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
        float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position); //Determine Distance

        if (distanceToEnemy > maxAttackRange)
        {
            aiAgent.destination = enemy.transform.position;     //Chase Player
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
        aiAgent.destination = enemy.transform.position + Random.onUnitSphere * 15;
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
}
