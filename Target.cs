using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody2D targetRb;
    public GameObject player;
    public GameManager gameManager;
    public int pointValue;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxTorque = 10;
    [SerializeField] private float ySpawnPos = 11;

    public void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Wall (L)").GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Wall (R)").GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Ceiling").GetComponent<Collider2D>());
    }

    public void Awake()
    {
        targetRb = GetComponent<Rigidbody2D>();
        transform.position = RandomSpawnPos();
        targetRb.AddForce((player.transform.position - transform.position) * RandomSpeed());
        targetRb.AddTorque(RandomTorque());
        Time.timeScale = 0.8f;
    }

    public void Update()
    {
        if (gameManager.isModeScreen)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
        }
    }

    float RandomSpeed()
    {
        return Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector2 RandomSpawnPos()
    {
        float xPos = Random.Range(0, 2);

        if (xPos < 0.5)
        { xPos = -19; }
        else
        { xPos = 19; }

        return new Vector2(xPos, Random.Range(-ySpawnPos, ySpawnPos));
    }
}
