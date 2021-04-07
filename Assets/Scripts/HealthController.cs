using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    // Health attributes
    [Range(1, 10)] public int health;
    [Range(1, 10)] public int numOfHearts;

    // UI
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public DeathController deathSystem;

    void FixedUpdate()
    {
        // Handling the display of the health
        for (int i = 0; i < hearts.Length; i++)
        {
            /* Impossible to have more health than number of hearts. */
            if (health > numOfHearts)
                health = numOfHearts;

            if (i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;

            if (i < numOfHearts)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
            deathSystem.GameOver();
    }
}
