using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///////////////////////////////////////////////////////////////////////////
//
// This script will calculate the power rating of your attack
// based on the current velocity of your character / character components.
//
//////////////////////////////////////////////////////////////////////////
public class PowerRating : MonoBehaviour
{
    public static PowerRating PWR = null;
    public float currentPlayerPowerRating; // Currently Updated Score.
    float playerPowerRating; // Used for calcutations.

    
    float playerEnemyRating; // Used for calcutations.


    private float enemyBasePowerRating = 25;

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
        float playerBasePowerRating = 50;
        #region Score Modifiers
        float hookBonus = 10;
        float powerStraightBonus = 30;
        float comboBonus = Combat.currentCombo;
        #endregion

        // PWR is equal to base before multipliers are applied.
        PWR_ = playerBasePowerRating;

        // Successive Hits increase powerscore.



        // LEAN & CROUCH & DODGE acts a multiplier altering the score.
        // Negate these scores from the current powerscore
        // after the next hit since the bonus was applied. (active only for that hit)

        // If we are leaning left or right.
        if(Combat.leaningZaxis == true)
        {
            Debug.Log("hook performed");
            PWR_ += hookBonus;
        }
        // If we are leaning forward or back
        if(Combat.leaningZaxis == true)
        {
            Debug.Log("Power Straigt!!");
            PWR_ += powerStraightBonus;
        }


        currentPlayerPowerRating = PWR_;

    }

    public void EnemyPowerRating(float PWR)
    {
        float lightEnemyPwrscore = enemyBasePowerRating * 2;
        float mediumEnemyPwrscore = enemyBasePowerRating * 2.5f;
        float heavyEnemyPwrscore = enemyBasePowerRating * 3;


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

        }

        if (gameObject.CompareTag("Medium"))
        {

        }

        if (gameObject.CompareTag("Heavy"))
        {

        }

    }   
    
}
