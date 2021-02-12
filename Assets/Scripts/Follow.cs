using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector2 minCamPos, maxCamPos;
    
    public float smoothTime;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothTime);
        float posY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smoothTime);

        transform.position = new Vector3(
            Mathf.Clamp(posX, minCamPos.x, maxCamPos.x), 
            Mathf.Clamp(posY, minCamPos.y, maxCamPos.y), 
            transform.position.z);
    }
}
