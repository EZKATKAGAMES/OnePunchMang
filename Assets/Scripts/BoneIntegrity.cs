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
    public static BoneIntegrity BoneHealth;

    public float leftArmHp;
    public float rightArmHp;
    public static Collider leftArm;
    public static Collider rightArm;

  
    public bool reduceIncomingDamage;

    public Collider[] limbColliders;

    private Animator[] animations;
    

    public void Awake()
    {
        BoneHealth = this;
        animations = GetComponents<Animator>();
        limbColliders = GetComponentsInChildren<Collider>();
    }

   public void Start()
    {
        
    }
    
    public void Update()
    {
        if(reduceIncomingDamage == true)
        {

        }  
    }

    public void TakeDamage(BoxCollider limbs, float amount)
    {
        if(limbs.gameObject.tag == "Left")
        {
            leftArmHp -= amount;
        }

        if(limbs.gameObject.tag == "Right")
        {
            rightArmHp -= amount;
        }
        

    }

    public void LimbBreak(Collider col, int limbID)
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
