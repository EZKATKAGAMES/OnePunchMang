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
    public float powerRating;

    private float playerBasePowerRating = 50;
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

    public void PlayerPowerRating(float? PWR)
    {
        // This score will be calculated from values measured from the player

        // This score will then be passed onto the player attack handler.

        // LEAN & CROUCH & DODGE acts a multiplier altering the score.


        // Set Base Power Score.

        // IF current player velocity is > 0 use it as a multiplier.

        // Check if LEAN action is used. & apply calculations if it is.

        // Return the 

    }

    public void EnemyPowerRating()
    {

    }
}
