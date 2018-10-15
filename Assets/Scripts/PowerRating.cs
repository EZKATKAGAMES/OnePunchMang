using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///////////////////////////////////////////////////////////////////////////
//
// This script will calculate the power rating of your attack
// And call the outcomes after comparing scores.
//
//////////////////////////////////////////////////////////////////////////
public class PowerRating : MonoBehaviour
{
    public static PowerRating PWR = null;
    public float currentPlayerPowerRating; // Currently Updated Score.
    public float currentLightEnemyPwrscore;
    public float currentMediumEnemyPwrscore;
    public float currentHeavyEnemyPwrscore;

    float playerPowerRating; // Used for calcutations.

    
    float playerEnemyRating; // Used for calcutations.

    public bool winExchange = false;

    

    private void Awake()
    {
        #region no duplicates
        if (PWR == null)
        {
            PWR = this;
        }
        else if (PWR != this)
        {
            Destroy(gameObject);
        }
        #endregion
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void PlayerPowerRating(float PWR_)
    {
        float playerBasePowerRating = 2;
        #region Score Modifiers
        float hookBonus = 10;
        float powerStraightBonus = 30;
        #endregion

        // PWR is equal to base before multipliers are applied.
        PWR_ = playerBasePowerRating;

        
        // LEAN & CROUCH & DODGE acts a multiplier altering the score.
        // Negate these scores from the current powerscore
        // after the next hit since the bonus was applied. (active only for that hit)

        // If we are leaning left or right.
/*
        if(Combat.CombatRef.leaningZaxis == true)
        {
            Debug.Log("hook performed");
            PWR_ += hookBonus;
        }
        // If we are leaning forward or back
        if(Combat.CombatRef.leaningZaxis == true)
        {
            Debug.Log("Power Straigt!!");
            PWR_ += powerStraightBonus;
        }
*/

        currentPlayerPowerRating = PWR_;

    }

    public void EnemyPowerRating(float PWR)
    {
        // Multiply the power depending on what tag enemy is.

        float enemyBasePowerRating = 25;

        PWR = enemyBasePowerRating;

        if (gameObject.tag == "Light")
        {
            currentLightEnemyPwrscore = PWR * 2;
        }
        else if (gameObject.tag == "Medium")
        {
            currentMediumEnemyPwrscore = PWR * 2.5f;
        }
        else if (gameObject.tag == "Heavy")
        {
            currentHeavyEnemyPwrscore = PWR * 3;
        }

        
        

        
    }

    public void AssignPowerRating()
    {
        // Check entity Type.
        if (gameObject.CompareTag("Player"))
        {
            // Overwrite Score.
            PlayerPowerRating(currentPlayerPowerRating);

        }

        if (gameObject.CompareTag("Light"))
        {
            EnemyPowerRating(currentLightEnemyPwrscore);
        }

        if (gameObject.CompareTag("Medium"))
        {
            EnemyPowerRating(currentMediumEnemyPwrscore);
        }

        if (gameObject.CompareTag("Heavy"))
        {
            EnemyPowerRating(currentHeavyEnemyPwrscore);
        }

    }


    public void CompareRating(float score1, float score2)
    {
        float losingScore = Mathf.Min(score1, score2);
        float winningScore = Mathf.Max(score1, score2);

        if(losingScore < winningScore)
        {
            // All damage Applicaiton here.

            // Player takes damage.

            if(PWR.currentPlayerPowerRating < winningScore)
            {

            }


            // Enemy takes damage.

            

            
        }

        // If we are hit and not blocking & did not parry.
        if (Combat.CombatRef.blocking == false && currentPlayerPowerRating < winningScore)
        {
            // die in one hit
        }
        

        // Apply damage onto enemy.
        if (currentPlayerPowerRating >= winningScore)
        {
            BoxCollider damageTaker = CheckforCollision.collisionRef.gameObject.GetComponent<BoxCollider>();
            if (CheckforCollision.collisionRef.gameObject.tag == "Left")
                damageTaker = GameObject.Find("FpLeft").GetComponent<BoxCollider>();
            if (CheckforCollision.collisionRef.gameObject.tag == "Right")
                damageTaker = GameObject.Find("FpRight").GetComponent<BoxCollider>();

            EnemyBoneIntegrity.BoneHealth.TakeDamage(damageTaker, winningScore);



        }


    }
    
}
