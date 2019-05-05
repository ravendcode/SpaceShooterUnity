using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] asteroids;
    [SerializeField] int asteroidCount = 3;
    [SerializeField] float startWait = 1f;
    [SerializeField] float spawnWait = 0.5f;
    [SerializeField] float waveWait = 4f;
    [SerializeField] float delayLoadGameOver = 1f;
    [SerializeField] string textForRestart = "Press R For Restart";

    [Header("GUI")]
    [SerializeField] Text scoreText;
    [SerializeField] Text restartText;
    [SerializeField] Text gameOverText;

    int score = 0;
    bool isGameOver;
    bool isRestart;
 

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {
        if (isRestart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            if (CheckStatusGameOver())
            {
                break;
            }
            for (int i = 0; i < asteroidCount; i++)
            {
                if (CheckStatusGameOver())
                {
                    break;
                }
                GameObject asteroid = asteroids[Random.Range(0, asteroids.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-6, 6), 0, 16);
                Instantiate(asteroid, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            if (CheckStatusGameOver())
            {
                break;
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    bool CheckStatusGameOver()
    {
        if (isGameOver)
        {
            restartText.text = textForRestart;
            isRestart = true;
            return true;
        }
        return false;
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateScore();
    }

    public void GameOver()
    {
        StartCoroutine(WaitAndLoadGameOver());
    }

    IEnumerator WaitAndLoadGameOver()
    {
        yield return new WaitForSeconds(delayLoadGameOver);
        gameOverText.text = "Game Over";
        isGameOver = true;
    }
}
