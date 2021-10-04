using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float bulletForce = 5;
    private Rigidbody2D bulletRb;
    
    void Awake()
    {
        bulletRb = GetComponent<Rigidbody2D>();

        // Set direction to mouse location
        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;

        // Fire
        bulletRb.velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * bulletForce;
    }
}
