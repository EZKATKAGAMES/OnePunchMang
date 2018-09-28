using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckforCollision : MonoBehaviour
{

    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        // On collision with enemy layer.
        // Compare power.
        
        if(collision.gameObject.layer == 10)
        {
            Debug.Log("reeeeeeee");
            PowerRating.PWR.CompareRating(0,0);
        }

    }
}
