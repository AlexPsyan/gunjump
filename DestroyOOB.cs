using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOOB : MonoBehaviour
{
    public float rightBounds = 30;
    public float topBounds = 20;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > rightBounds)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < -rightBounds)
        {
            Destroy(gameObject);
        }
        if (transform.position.y > topBounds)
        {
            Destroy(gameObject);
        }
        if (transform.position.y < -topBounds)
        {
            Destroy(gameObject);
        }
    }
}
