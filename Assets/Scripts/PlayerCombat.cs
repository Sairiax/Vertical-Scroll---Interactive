using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    private PlayerController movement;

    private bool attacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && movement.grounded)
            Attack();

        // If the animation attack has finished
        if (attacking && animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack"))
        {
            attacking = false;
            movement.isMovementAllowed = true;
        }
    }

    void Attack()
    {
        attacking = true;
        movement.isMovementAllowed = false;
        Debug.Log("ATTACKK!!!!");


        // Play attack animation.
        animator.SetTrigger("Attack");

        // Detect enemies in range of damage.

        // Damage the enemies.
    }
}
