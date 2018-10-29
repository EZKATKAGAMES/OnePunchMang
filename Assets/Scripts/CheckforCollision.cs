using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///////////////////////////////////////////////////////////////////
////
//// This script calls rating checks when the enemy detects a hit from its arms
//// 
///////////////////////////////////////////////////////////////////
public class CheckforCollision : MonoBehaviour
{
    public enum LeftOrRight { Left, Right };
    public LeftOrRight LeftorRight;

    public string currentFigherClass;
    public float useScore;
    public static CheckforCollision collisionRef = null;

    private void Awake()
    {
        if(collisionRef == null)
        {
            collisionRef = this;
        }

        //if(gameObject.GetComponentInParent<GameObject>().CompareTag("Light"))
        //{
        //    useScore = PowerRating.PWR.currentLightEnemyPwrscore;
        //}
        //if (gameObject.GetComponentInParent<GameObject>().CompareTag("Medium"))
        //{
        //    useScore = PowerRating.PWR.currentMediumEnemyPwrscore;
        //}
        //if (gameObject.GetComponentInParent<GameObject>().CompareTag("Heavy"))
        //{
        //    useScore = PowerRating.PWR.currentHeavyEnemyPwrscore;
        //}

        

    }



    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.layer == 9) // ARMS Layer
        {

            // Compare the rating of this enemy to the player.
            PowerRating.PWR.CompareRating(useScore, PowerRating.PWR.currentPlayerPowerRating);

            #region Limb Breaking
            // Finding the limb ID so that we can call limb break correctly
            int limbID;
            if (gameObject.tag == "Left")
                limbID = 0;
            if (gameObject.tag == "Right")
                limbID = 1;
            
            
            

            #endregion
        }

    }

}
