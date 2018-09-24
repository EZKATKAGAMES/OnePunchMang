using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///////////////////////////////////////////////////////////////////////////
//
// This script will perform the attack animation and enable colliders
// that check for collisions and compare scores.
//
//////////////////////////////////////////////////////////////////////////
public class Combat : MonoBehaviour
{
    public static Combat CombatRef = null;


    public bool leaningZaxis; // Left and Right leaning.
    public bool leaningXaxis; // Forward and backward leaning.

    public bool rightPunch;
    public bool leftPunch;

    public bool blocking;

    public float chargePercent = 0;
    public float chargeMultiplier = 50;
    // Get individual animators
    private Animator[] animations;
    

    private void Awake()
    {
        animations = gameObject.GetComponentsInChildren<Animator>();
        
    }

    void Start()
    {
        Cursor.visible = false;

        
    }

    
    void Update()
    {
        SetPowerRating();
        PunchingAndBlock();

        // When Performing a charged straight or right.
        if(Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
        {
            chargePercent += Time.deltaTime * chargeMultiplier;
        }
        else
        {
            chargePercent = 0;
        }

    }

    #region Punching & Block activation

    void PunchingAndBlock()
    {
        foreach (Animator components in animations)
        {
            Debug.Log(components.name);
            #region Punch
            // Right hand
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                components.SetBool("RightStraight", true);
                StartCoroutine(rightPunchCD());
            }

            // Left hand
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                components.SetBool("LeftStraight", true);
                StartCoroutine(leftPunchCD());
            }
            #endregion

            #region Block

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                blocking = true;
            }

            #endregion
        }
    }

    IEnumerator leftPunchCD()
    {
        yield return new WaitForSeconds(0.1f);
        animations[0].SetBool("LeftStraight", false);
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(leftPunchCD());
    }

    IEnumerator rightPunchCD()
    {
        yield return new WaitForSeconds(0.1f);
        animations[1].SetBool("RightStraight", false);
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(rightPunchCD());
    }

    #endregion

    #region Block activation



    #endregion

    void SetPowerRating()
    {
        // When Performing a straight right or left.
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            PowerRating.PWR.AssignPowerRating();
            Debug.Log(PowerRating.PWR.currentPlayerPowerRating);
        }
    }

    void chargeAttack()
    {    
        PowerRating.PWR.AssignPowerRating();
        Debug.Log(PowerRating.PWR.currentPlayerPowerRating);
    }



}
