using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///////////////////////////////////////////////////////////////////////////
//
// This script tracks the current health of your two arms.
// executes actions taking damage.
//
// (feature idea) Reduce the speed/or strengh of hits based on
// the health of your arms.
//
//////////////////////////////////////////////////////////////////////////


public class EnemyBoneIntegrity : MonoBehaviour
{
    public static EnemyBoneIntegrity BoneHealth;
    public EnemyStatsManager hpRef;

    public float leftArmHp;
    public float rightArmHp;

    

    public bool reduceIncomingDamage;

    public Collider[] limbColliders;

    private Animator[] animations;


    public void Awake()
    {
        BoneHealth = this;
        animations = GetComponents<Animator>();
        limbColliders = GetComponentsInChildren<Collider>();

        
    }

    public virtual void Start()
    {
        leftArmHp = hpRef.health;
        rightArmHp = hpRef.health2;
    }

    public virtual void Update()
    {
        if (reduceIncomingDamage == true)
        {

        }
    }

    public virtual void TakeDamage(BoxCollider limbs, float amount)
    {
        if (limbs.gameObject.tag == "Left")
        {
            leftArmHp -= amount;
        }

        if (limbs.gameObject.tag == "Right")
        {
            rightArmHp -= amount;
        }


    }

    public virtual void LimbBreak(Collider col, int limbID)
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
        if (limbID == 0)
        {
            //animations.SetBool("", true);
        }
        else if (limbID == 1)
        {
            //animations.SetBool("", true);
        }
    }
}
