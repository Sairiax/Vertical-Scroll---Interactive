using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Speed attributes
    public float maxSpeed = 3.0f;
    public float speed = 2.0f;

    // Is on the ground?
    public bool grounded;

    // Jump Attributes
    public float jumpPower = 6.5f;
    private bool jump;
    private bool doubleJump = false;
        
    // Physics and animations.
    private Rigidbody2D physics;
    private Animator anim;

    public bool isMovementAllowed = true;
    private SpriteRenderer rd;

    // Map System
    public LevelSystem spawner;

    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rd = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /* Animation control */
        anim.SetFloat("speed", Mathf.Abs(physics.velocity.x));                      // Important don't take into account the sign.
        anim.SetBool("grounded", grounded);

        // While standing on the ground I can jump without problems.
        if (grounded)
            doubleJump = true;

        /* Jump button pressed. */
        // It has to be done here because in the fixedUpdate
        // maybe ignore the button press due to the way fixed
        // update is executed, not always only with the physics.
        if (Input.GetKeyDown(KeyCode.UpArrow))
            if (grounded)
                jump = true;
            else if (doubleJump)
            {
                jump = true;
                doubleJump = false;
            }

        if (transform.position.y > 4.5f)
        {
            transform.position = new Vector3(0, 0, 0);
            // Spawn map
            spawner.changeMap();
        }
    }

    /**
     * Is better than the previous one to work with physics, for
     * instance the movement of the player, here we do not need to
     * multiply by time.deltaTime to correct the framerate.
     */ 
    public void FixedUpdate()
    {
        Vector3 fixedVelocity = physics.velocity;
        fixedVelocity.x *= 0.75f;

        if (grounded)
            physics.velocity = fixedVelocity;

        // Detect when player would like to move horizontaly.
        float h = Input.GetAxis("Horizontal");
        h = !isMovementAllowed ? 0 : h;                                          // Disabling the movement if it's needed.

        // Changing player visual direction to reflect reality.
        if (h < 0.0f)
            transform.localScale = new Vector3(-1f, 1f, 1f);            // Left
        else if (h > 0.0f)
            transform.localScale = new Vector3(1f, 1f, 1f);             // Right

        // Move the player horizontaly, with speed and with h direction.
        physics.AddForce(Vector2.right * speed * h);

        // Limiting maximum speed, otherwise crazy things can happen.
        float limitedSpeed = Mathf.Clamp(physics.velocity.x, -maxSpeed, maxSpeed);
        physics.velocity = new Vector2(limitedSpeed, physics.velocity.y);

        //Debug.Log(physics.velocity.x);

        // Applying the JUMP if it is needed
        if (jump)
        {
            physics.velocity = new Vector2(physics.velocity.x, 0);
            physics.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }
    }

    public void EnemyJump()
    {
        jump = true;
    }

    public void EnemyKnockback(float enemyPosition)
    {
        float side;

        rd.color = Color.red;                                                 // dyeing the player with a redish color.
        isMovementAllowed = false;                                                  // Player is now confused (unable to react)
        jump = true;

        // Knockback damage!
        side = Mathf.Sign(enemyPosition - transform.position.x);
        physics.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse);

        Invoke("React", 0.7f);                                                       // After 0.7s the player will be able to move again.
    }

    public void React()
    {
        isMovementAllowed = true;
        rd.color = Color.white;                                              // Reverting the dyeing.
    }
}