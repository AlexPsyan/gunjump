using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float recoil;
    public float deathLevel = -10.6f;
    public GameObject bulletPrefab;
    private Rigidbody2D playerRb;
    private GameManager gameManager;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive && !gameManager.paused)
        {
            // Set direction to mouse position
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Detect left click
            if (Input.GetMouseButtonDown(0))
            {
                if (gameManager.mode == 1)
                {
                    Fire();
                }
                if (gameManager.mode == 0)
                {
                    if (gameManager.ammo > 0)
                    {
                        Fire();
                        gameManager.UpdateAmmo(-1);
                    }
                }
            }
        }

        if (transform.position.y < deathLevel)
        {
            gameManager.lives = 0;
            gameManager.livesText.text = "Lives: " + gameManager.lives;
            gameManager.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            gameManager.LoseLife();
        }
    }

    private void Fire()
    {
        // Create bullet
        Instantiate(bulletPrefab, transform.position, transform.rotation);

        // Recoil
        gameManager.GetMouseDirection();
        playerRb.AddForce(new Vector2(-gameManager.mouseDirection.x, -gameManager.mouseDirection.y).normalized * recoil);
    }
}
