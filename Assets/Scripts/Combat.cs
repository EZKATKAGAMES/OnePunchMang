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

    public float chargePercent = 0;
    public float chargeMultiplier = 50;

    private Animator animations;

    private void Awake()
    {
        animations = gameObject.GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Cursor.visible = false;
        Debug.Log(animations.gameObject.name);
    }

    
    void Update()
    {
        SetPowerRating();

        RightPunch();
        LeftPunch();

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

    #region Punching activation
    void RightPunch()
    {
        // Enable collider

        
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // Play animation
            animations.SetBool("RightStraight", true);
            StartCoroutine(rightPunchCD());
        }
    }

    void LeftPunch()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animations.SetBool("LeftStraight", true);
            StartCoroutine(leftPunchCD());
        }
    }

    IEnumerator leftPunchCD()
    {
        yield return new WaitForSeconds(0.1f);
        animations.SetBool("LeftStraight", false);
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(leftPunchCD());
    }

    IEnumerator rightPunchCD()
    {
        yield return new WaitForSeconds(0.1f);
        animations.SetBool("RightStraight", false);
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(rightPunchCD());
    }

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
