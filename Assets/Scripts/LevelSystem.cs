using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private Random rnd = new Random();
    public GameObject[] maps;
    private GameObject currentMap;

    void Start()
    {
        changeMap();
    }

    public void changeMap()
    {
        int x = Random.Range(0, maps.Length);
        Destroy(currentMap);
        currentMap = Instantiate(maps[x], transform.position, transform.rotation);
    }
}
