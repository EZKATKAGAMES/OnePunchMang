using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///////////////////////////////////////////////////////////////////////////
//
// This script tracks the current health of your two arms.
// executes actions like death (game over).
//
// (feature idea) Reduce the speed/or strengh of hits based on
// the health of your arms.
//
//////////////////////////////////////////////////////////////////////////


public class BoneIntegrity : MonoBehaviour
{
    public float leftArmHp = 100f;
    public float rightArmHp = 100f;
    public static Collider leftArm;
    public static Collider rightArm;

    private Animator animations;


    private void Awake()
    {
        animations = GetComponent<Animator>();
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void LimbBreak(Collider col, float limbID)
    {
        /// Temporary
        MeshRenderer rend;
        rend = col.GetComponent<MeshRenderer>();
        rend.enabled = false;
        /// Temporary



        // Get the specific collider (passed when calling function)

        // Disable
        col.enabled = false;
        // Play sfx
        
        // Set anim bool.
        if(limbID == 0)
        {
            //animations.SetBool("", true);
        }
        else if(limbID == 1)
        {
            //animations.SetBool("", true);
        }
    }
}
