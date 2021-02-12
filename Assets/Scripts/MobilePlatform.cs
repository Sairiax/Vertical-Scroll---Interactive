using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    public Transform target;
    public float speed;

    private bool toStart = true;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 goal;

    void Start()
    {
        if (target != null)
        {
            startPosition = transform.position;
            endPosition = target.position;
            goal = endPosition;
            target.parent = null;                                        // Avoid move towards a moving object because is my child!
        }
    }

    void FixedUpdate()
    {
        if (target != null)
            if (transform.position != goal)
                transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
            else
            {
                // Changing the destiny!.
                goal = toStart ? startPosition : endPosition;
                toStart = !toStart;
            }
        else
            Debug.Log("Mobile platform without target!");
    }
}
