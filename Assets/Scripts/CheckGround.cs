using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private PlayerController player;
    private Rigidbody2D playerPhysics;

    // Start is called before the first frame update
    public void Start()
    {
        player = GetComponentInParent<PlayerController>();
        playerPhysics = player.GetComponent<Rigidbody2D>();
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        /* You have collide with the ground aka you are standing on the ground. */
        if (col.gameObject.tag == "Ground")
            player.grounded = true;
        else if (col.gameObject.tag == "Platform")
        {
            player.grounded = true;
            playerPhysics.velocity = new Vector3(0f, 0f, 0f);
            player.transform.parent = col.transform;            // My new parent is the platform, otherwise I don't move with it.
        }
    }

    public void OnCollisionExit2D(Collision2D col)
    {
        /* You are no longer touching the ground which probably means 
         * you are in the air. */
        if (col.gameObject.tag == "Ground")
            player.grounded = false;
        else if (col.gameObject.tag == "Platform")
        {
            player.grounded = false;
            player.transform.parent = null;                     // Bye bye parent I don't need you anymore!.
        }
    }
}
