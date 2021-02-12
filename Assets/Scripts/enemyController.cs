using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    // Speed attributes
    public float maxSpeed = 1f;
    public float speed = 1f;

    // Physics and animations.
    private Rigidbody2D physics;
    //private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        physics.AddForce(Vector2.right * speed);

        // Limiting maximum speed, otherwise crazy things can happen.
        float limitedSpeed = Mathf.Clamp(physics.velocity.x, -maxSpeed, maxSpeed);
        physics.velocity = new Vector2(limitedSpeed, physics.velocity.y);

        if (physics.velocity.x >= -0.01f && physics.velocity.x <= 0.01f)
        {
            speed = -speed;                                                 // Inverting direction.
            physics.velocity = new Vector2(speed, physics.velocity.y);      // Aplying the inverted direction.
        }

        // Changing enemy visual direction to reflect reality.
        if (speed >= 0.0f)
            transform.localScale = new Vector3(-1f, 1f, 1f);            // Left
        else if (speed < 0.0f)
            transform.localScale = new Vector3(1f, 1f, 1f);             // Right
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Have I collided with the player?
        if (col.gameObject.tag == "Player")
        {
            float yOffset = 0.5f;

            // Is the player above me?
            if (transform.position.y + yOffset < col.transform.position.y)
            {
                col.SendMessageUpwards("EnemyJump");                                   // Invokes the method inside the player script.
                Destroy(gameObject);
            }
            else
            {
                // Player hit!
                col.SendMessageUpwards("EnemyKnockback", transform.position.x);
                col.SendMessageUpwards("TakeDamage", 1);
            }
        }
    }
}
