using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public bool pausable = true;
    public bool isActive;
    private Canvas c;

    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<Canvas>();
        isActive = false;                                    // By default the game is not paused.
        c.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pausable && Input.GetKeyDown(KeyCode.P))
        {
            isActive = !isActive;
            c.enabled = isActive;
            Time.timeScale = isActive ? 0 : 1;
        }
    }
}
