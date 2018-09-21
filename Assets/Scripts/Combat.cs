using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public static bool leaningZaxis; // Left and Right leaning.
    public static bool leaningXaxis; // Forward and backward leaning.
    public static float chargePercent;
    public static int currentCombo;


    public float chargeMultiplier = 100 / 3;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // When Performing a straight right or left.
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            PowerRating.PWR.AssignPowerRating();
            Debug.Log(PowerRating.PWR.currentPlayerPowerRating);

        }

        // When Performing a charged straight or right.
        if(Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
        {
            chargePercent += Time.deltaTime * chargeMultiplier;
        }

    }

    void chargeAttack()
    {
        
        PowerRating.PWR.AssignPowerRating();
        Debug.Log(PowerRating.PWR.currentPlayerPowerRating);
    }



}
