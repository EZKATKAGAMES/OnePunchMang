using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//////////////////////////////////////////////
//
// Attach this to the head of enemy prefab.
// Looks at the camera, or other target.
//
//////////////////////////////////////////////
public class EnemyLookAtPlayer : MonoBehaviour
{
    Vector3 lookTarget;
    private void Update()
    {
        lookTarget = FindObjectOfType<Camera>().transform.position;
        Vector3 relativePosition = lookTarget - gameObject.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePosition);
        transform.rotation = rotation;
    }
}
