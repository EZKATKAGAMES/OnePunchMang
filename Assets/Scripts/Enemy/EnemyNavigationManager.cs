using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//////////////////////////////////////////////////////////
//
// This script will handle all AI navigation for enemies.
//
//////////////////////////////////////////////////////////
public class EnemyNavigationManager : MonoBehaviour
{
    public NavMeshAgent agent;
    [HideInInspector]
    public EnemyStatsManager stats;
    
    // States that affect movement.
    public enum AIState { SeekingTowardsTarget, CirclingTarget, Retreating, Combat}
    public AIState currentState;
    public string determineBuild;


    public string playerID;

    // (may) be used as AI State Tansition Conditions, (distance, hp, ect)
    float distanceToTarget; // distance from the AI object and the target object.
    float distanceToAttackInitialize = 3.2f; // distance before attackstate is forced.
    float retreatDistance; // distance travelled during a retreat.
    Vector3 target;
    


    void Awake()
    {
        stats = gameObject.GetComponent<EnemyStatsManager>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        // Determine the figher class... AI actions based on weightclass.
        determineBuild = this.gameObject.tag.ToString();
        playerID = Resources.Load("Prefabs/Characters/PlayerCharacter").name as string;
    }

    
    void Start()
    {
        currentState = AIState.SeekingTowardsTarget;
    }

    
    void Update()
    {
        UpdateAIState();
        AIStateBehaviours();

        target = GameObject.Find(playerID).transform.position;

        // Record distance at all times.
        distanceToTarget = Vector3.Distance(transform.position, target);
        Debug.Log(Vector3.Distance(transform.position, target));
        
    }

    void UpdateAIState()
    {
        // Behaviours may differ in weighting and action depending on the fighter class.
        
        RaycastHit hit;
        Ray ray = new Ray(transform.position, target);

        Debug.DrawLine(gameObject.transform.position, target);



        /// Defualt: Lightweight Fighter.
        if (determineBuild == null)
        {
            determineBuild = "Light";
        }

        if (determineBuild == "Light")
        {
            // Get closer to the target.
            currentState = AIState.SeekingTowardsTarget;
            // When close enough, begin circling target.
            if(distanceToTarget < 6.3f && currentState != AIState.Retreating)
            {
                currentState = AIState.CirclingTarget;
            }

            // Raycast Checking Player
            if (Physics.Raycast(ray, out hit, 1000, 1 << 8))
            {
                   
            }

            // If we are close enough to start attacking.
            if(currentState == AIState.CirclingTarget && distanceToTarget <= distanceToAttackInitialize)
            {
                currentState = AIState.Combat;
            }

        }

        if (determineBuild == "Medium")
        {
            // Get closer to the target.
            currentState = AIState.SeekingTowardsTarget;


        }

        if (determineBuild == "Heavy")
        {
            // Get closer to the target.
            currentState = AIState.SeekingTowardsTarget;


        }


    }

    void AIStateBehaviours()
    {
        // Determine what happens during each state.

        if(currentState == AIState.SeekingTowardsTarget)
        {
            // Adjust stats if necessary.
            agent.speed = stats.movementSpeed;
            agent.stoppingDistance = 6f;

            // Acquire desination target
            agent.SetDestination(target);
        }

        if(currentState == AIState.CirclingTarget)
        {
            // Animation Manager: Change stance to defensive.



            // Adjust stats
            agent.speed = stats.movementSpeed /2;
            float[] rotationAngles = { -180f, 180f };
            int randomNum;
            // Choose direction.
            randomNum = Random.Range(0, 2);
            // Rotation
            transform.RotateAround(target, Vector3.up, rotationAngles[randomNum] * Time.deltaTime * stats.circleSpeed);

            // Slowly move closer the the player.
            agent.stoppingDistance = 3.5f;

        

        }

        if(currentState == AIState.Retreating)
        {
            // Animation Manager: Blocking
            // Animation Manager: leap back.

            agent.stoppingDistance = 0f;
            Vector3 retreatTo;
            retreatTo = GameObject.Find("RetreatLocation").transform.position;
            // Go to new destination a little further back
            agent.SetDestination(retreatTo);

            if(distanceToTarget > 4)
            {
                currentState = AIState.CirclingTarget;
            }
            

        }

        if(currentState == AIState.Combat)
        {
            // Block when not hitting
            EnemyBoneIntegrity.BoneHealth.reduceIncomingDamage = true;

            // Block anim

            
            
        }



    }

    
    
}
