using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private Rigidbody2D ammoRb;
    public GameManager gameManager;
    public int ammoValue;
    public float xPos;
    [SerializeField] private float ySpawnPos = 8;

    public void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Wall (L)").GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Wall (R)").GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Ceiling").GetComponent<Collider2D>());
    }

    public void Awake()
    {
        ammoRb = GetComponent<Rigidbody2D>();
        transform.position = RandomSpawnPos();
        if (xPos < 0) 
        {
            ammoRb.AddForce(new Vector2(200, 0));
        }
        else
        {
            ammoRb.AddForce(new Vector2(-200, 0));
        }
    }

    public void Update()
    {
        if (gameManager.isModeScreen)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.UpdateAmmo(ammoValue);
            Destroy(gameObject);
        }
    }

    Vector2 RandomSpawnPos()
    {
        xPos = Random.Range(0, 2);
        
        if (xPos < 0.5)
        {
            xPos = -19;
        }
        else
        {
            xPos = 19;
        }
        
        return new Vector2(xPos, Random.Range(-ySpawnPos, ySpawnPos));
    }
}
