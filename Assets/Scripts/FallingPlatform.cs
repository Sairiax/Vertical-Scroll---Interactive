using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D physics;
    private BoxCollider2D col;

    public float fallDelay = 1f;
    public float respawnDelay = 5f;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        physics = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Invoke("Fall", fallDelay);
            Invoke("Respawn", fallDelay + respawnDelay);
        }
    }

    void Fall()
    {
        physics.isKinematic = false;
        col.isTrigger = true;
    }

    void Respawn()
    {
        physics.isKinematic = true;
        col.isTrigger = false;
        physics.velocity = Vector3.zero;            // new Vector3(0f, 0f, 0f);
        transform.position = initialPosition;
    }
}
