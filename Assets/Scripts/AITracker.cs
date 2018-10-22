using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITracker : MonoBehaviour
{
    public GameObject player;
    private float trackerMovement = 20f;
    public bool FlipFlop = false;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(ChangeTrackerDirection(1));
    }

    private void Update()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateAroundPlayer();
    }

    void RotateAroundPlayer()
    {
        transform.RotateAround(player.transform.position, Vector3.up, trackerMovement * Time.deltaTime);
    }

    IEnumerator ChangeTrackerDirection(float waitTime)
    {
        FlipFlop = !FlipFlop;
        print("It's Been A Second");
        if(FlipFlop == true)
        {
            print("MoveRight");
            trackerMovement = 20.0f;
        }
        else
        {
            print("MoveLeft");
            trackerMovement = -20.0f;
        }
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(ChangeTrackerDirection(5f));
    }
}
