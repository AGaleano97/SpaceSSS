using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;

    public Vector3 spawnValues;

    public int hazardCount;

    public float spawnWait;

    public float startWait;

    public float waveWait;

    public Text ScoreText;

    public Text restartText;

    public Text gameOverText;

    public Text winText;

    public Text creditText;

    public Text hardText;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    private bool gameOver;

    private bool restart;

    private int score;

    void Start ()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        creditText.text = "";
        hardText.text = "Press 'H' for hard mode!";
        score = 0;
        UpdateScore ();
        StartCoroutine (SpawnWaves ());
    }

    void Update ()
    {
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.T))
            {
                Application.LoadLevel (Application.loadedLevel);
            }
        }
    }
    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds (startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);
            if (gameOver)
            {
                restartText.text = "Press 'T' to Try Again";
                restart = true;
                break;
            }
        }
    }

    public void Addscore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore ();
    }

    void UpdateScore ()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            winText.text = "You Win!";
            creditText.text = "Game created by: Alejandro Galeano";
            musicSource.clip = musicClipOne;
            musicSource.Play();
            gameOver = true;
            restart = true;
        }
    }

    public void GameOver ()
    {
        gameOverText.text = "Game Over!";
        creditText.text = "Game created by: Alejandro Galeano";
        musicSource.clip = musicClipTwo;
        musicSource.Play();
        gameOver = true;
    }
}
