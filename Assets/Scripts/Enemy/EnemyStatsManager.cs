using System.Collections;
using System.Collections.Generic;
using UnityEngine;
////////////////////////////////////////////
//
// Assign stats & manage the stats scaling
//
////////////////////////////////////////////
public class EnemyStatsManager : MonoBehaviour
{
    [HideInInspector]
    public EnemyNavigationManager reference;

    public static EnemyStatsManager statRef = null;

    #region Variables
    [Header("Health")]
    public float health;
    public float health2;
    [Header("Movement")]
    public float movementSpeed;
    public float circleSpeed;
    [Header("Combat")]
    public float blockStamina;
    public float attackSpeed;
    public int powerScore;

    // Scaling Modifiers
    float timer;
    int scaleID;
    int lDefeated, mDefeated, hDefeated; // Record of fighters defeated by fight class.

    #region multipliers/???
    /// The multiplier is based
    /// on the killcount divided by MULTIPLIER
    /// 20 * 100kc / 80 = 5 (for Light).
    /// The > the multiplier the smaller the bonus;
    /// After 100 kills this would make the MS = 25;
    int l_MoveSpeedMultiplier = 80;


    #endregion


    #endregion


    private void Awake()
    {
        if(statRef == null)
        {
            statRef = this;
        }
    }

    void Start()
    {
        reference = gameObject.GetComponent<EnemyNavigationManager>();
        AssignStats();
    }

    void Update()
    {
        ScaleStats(); 
    }

    public void AssignStats()
    {
        switch (reference.determineBuild)
        {
            default:
                health = 100f;
                health2 = 100f;
                movementSpeed = 5f;
                circleSpeed = 8f;
                blockStamina = 20f;
                attackSpeed = 2.5f;
                scaleID = 0;
                powerScore = 30;
                break;

            case "Light":
                health = 100f;
                health2 = 100f;
                movementSpeed = 4.5f;
                circleSpeed = 0.3f;
                blockStamina = 20f;
                attackSpeed = 2.5f;
                scaleID = 0;
                powerScore = 30;
                break;

            case "Medium":
                health = 150f;
                health2 = 150f;
                movementSpeed = 3f;
                circleSpeed = 0.5f;
                blockStamina = 45f;
                attackSpeed = 1f;
                scaleID = 1;
                powerScore = 45;
                break;

            case "Heavy":
                health = 200f;
                health2 = 200f;
                movementSpeed = 1.5f;
                circleSpeed = 0.1f;
                blockStamina = 80f;
                attackSpeed = 0.6f;
                scaleID = 2;
                powerScore = 50;
                break;

        }
    }

    void ScaleStats()
    {
        timer = Time.fixedTime;

        // Another switch statements
        switch (scaleID)
        {
            // Light: Run stat scaling for a light fighters
            case 0:

                if(lDefeated > 0)
                {
                    movementSpeed = movementSpeed * lDefeated / l_MoveSpeedMultiplier;
                }
               
                // FINISH LATER NOT IMPORTANT NOW.
                break;
        }
    }
}
