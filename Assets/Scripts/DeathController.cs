using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    public bool isActive;
    private Canvas c;    

    public PauseController pc;

    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<Canvas>();
        isActive = false;                                    // By default the game is not paused.
        c.enabled = false;
    }

    public void GameOver()
    {
        // Showing the canvas and stoping the game.
        isActive = !isActive;
        c.enabled = isActive;
        Time.timeScale = isActive ? 0 : 1;

        // The pause menu cannot be accesed now!
        pc.pausable = false;
    }
}
