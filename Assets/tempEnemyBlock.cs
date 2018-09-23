using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempEnemyBlock : MonoBehaviour
{

    public bool blocking = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Debug.Log("ouchie");
        }
    }
}
