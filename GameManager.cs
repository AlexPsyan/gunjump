using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] targetPrefabs;
    public GameObject[] ammoPrefabs;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI ammoText;
    public Button modeSelectButton;
    public GameObject difSelectScreen;
    public GameObject modeSelectScreen;
    public GameObject pauseScreen;
    public GameObject player;
    public Vector3 mouseDirection;
    public Vector2 spawnpoint = new Vector2(0, 5);
    public float targetSpawnRate;
    private float easyTargetMinSpawnRate = 0.75f;
    private float easyTargetMaxSpawnRate = 1.25f;
    private float medTargetMinSpawnRate = 0.5f;
    private float medTargetMaxSpawnRate = 1f;
    private float hardTargetMinSpawnRate = 0.25f;
    private float hardTargetMaxSpawnRate = 0.75f;
    public float ammoSpawnRate;
    private float ammoMinSpawnRate = 5;
    private float ammoMaxSpawnRate = 10;
    public bool isGameActive = true;
    public bool isModeScreen;
    public bool paused;
    private int score;
    public int lives;
    public int ammo;
    public int startAmmo;
    public int dif;
    public int mode;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangePaused();
        }
    }

    public void GetMouseDirection()
    {
        if (!paused)
        {
            mouseDirection = Input.mousePosition;
            mouseDirection.z = 0.0f;
            mouseDirection = Camera.main.ScreenToWorldPoint(mouseDirection);
            mouseDirection = mouseDirection - transform.position;
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            targetSpawnRate = RandomTargetTime();
            yield return new WaitForSeconds(targetSpawnRate);
            int index = Random.Range(0, targetPrefabs.Length);
            Instantiate(targetPrefabs[index]);
        }
    }

    IEnumerator SpawnAmmo()
    {
        while (isGameActive)
        {
            ammoSpawnRate = RandomAmmoTime();
            yield return new WaitForSeconds(ammoSpawnRate);
            int index = Random.Range(0, ammoPrefabs.Length);
            Instantiate(ammoPrefabs[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        if (isGameActive)
        {
            score += scoreToAdd;
            scoreText.text = "Score: " + score;
        }
    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void LoseLife()
    {
        if (isGameActive)
        {
            lives--;
            livesText.text = "Lives: " + lives;

            if (lives < 1)
            {
                GameOver();
            }
        }
    }

    public void UpdateAmmo(int ammoToChange)
    {
        if (isGameActive)
        {
            ammo += ammoToChange;
            ammoText.text = "Ammo: " + ammo;
        }
    }

    public void GameOver()
    {
        modeSelectButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void ModeSelect()
    {
        isModeScreen = true;
        player.transform.position = spawnpoint;
        player.gameObject.SetActive(false);
        modeSelectScreen.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);
        ammoText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        modeSelectButton.gameObject.SetActive(false);
    }

    public void StartGame(int difficulty)
    {
        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        player.transform.position = spawnpoint;

        isGameActive = true;
        lives = 3;
        livesText.text = "Lives: " + lives;
        score = 0;
        scoreText.text = "Score: " + score;
        if (mode == 0)
        {
            ammoText.gameObject.SetActive(true);
            ammo = startAmmo;
            ammoText.text = "Ammo: " + ammo;
        }
        dif = difficulty;

        StartCoroutine(SpawnTarget());
        if (mode == 0)
        {
            StartCoroutine(SpawnAmmo());
        }
        UpdateScore(0);

        difSelectScreen.gameObject.SetActive(false);
    }

    float RandomTargetTime()
    {
        if (dif == 0) //easy
        {
            return Random.Range(easyTargetMinSpawnRate, easyTargetMaxSpawnRate);
        }
        if (dif == 1) //med
        {
            return Random.Range(medTargetMinSpawnRate, medTargetMaxSpawnRate);
        }
        else //hard (difficulty 2)
        {
            return Random.Range(hardTargetMinSpawnRate, hardTargetMaxSpawnRate);
        }
    }

    float RandomAmmoTime()
    {
        return Random.Range(ammoMinSpawnRate, ammoMaxSpawnRate);
    }
}
