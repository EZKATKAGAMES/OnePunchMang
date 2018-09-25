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
    private SphereCollider[] hands;

    private void Awake()
    {
        animations = gameObject.GetComponentsInChildren<Animator>();
        hands = gameObject.GetComponentsInChildren<SphereCollider>();
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
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

            #region Block

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Mouse0)
                || Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Mouse1))
            {
                blocking = true;
            }
            else
            {
                blocking = false;
            }

            if (blocking && Input.GetKeyDown(KeyCode.Mouse1))
            {
                // Play block animation
                //components.SetBool("RightBlock", true);
                // enable collider

                // Reduce incoming damage.
                //BoneIntegrity.BoneHealth.reduceIncomingDamage = true;
                // Stop block if we lift off of the mouse button
                StartCoroutine(rightBlockReset());
            }

            if (blocking && Input.GetKeyDown(KeyCode.Mouse0))
            {
                // Play block animation
                //components.SetBool("LeftBlock", true);
                // Reduce incoming damage.
                //BoneIntegrity.BoneHealth.reduceIncomingDamage = true;
                // Stop block if we lift off of the mouse button
                StartCoroutine(leftBlockReset());
            }

            #endregion

            #region Punch
            // Right hand
            if (!blocking && Input.GetKeyDown(KeyCode.Mouse1))
            {
                components.SetBool("RightStraight", true);
                // enable collider

                StartCoroutine(rightPunchCD());
            }

            // Left hand
            if (!blocking && Input.GetKeyDown(KeyCode.Mouse0))
            {
                components.SetBool("LeftStraight", true);
                StartCoroutine(leftPunchCD());
            }
            #endregion
        }
    }

    IEnumerator rightBlockReset()
    {
        yield return new WaitForSeconds(0);
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            // Release block.
            blocking = false;
        }
        StopCoroutine(rightBlockReset());
    }

    IEnumerator leftBlockReset()
    {
        yield return new WaitForSeconds(0);
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            // Release block.
            blocking = false;
        }
        StopCoroutine(leftBlockReset());
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
