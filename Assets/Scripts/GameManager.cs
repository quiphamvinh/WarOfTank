using Pathfinding.Util;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameManager Manager;
    AudioManager audioManager;

    [Header("Game UI")]
    public GameObject gameWin;
    public GameObject gamePause;
    public GameObject UIPlayer;
    public GameObject gameOver;
    private bool isGameWon = false;

    [Header("Player")]
    public GameObject[] playerPrefab;
    int tankIndex;

    [Header("Coin")]
    public TextMeshProUGUI txtCoins;
    public TextMeshProUGUI txtCoinGameWin;
    public TextMeshProUGUI txtCoinGameOver;
    public static int numberOfCoins;

    [Header("Finish Gate")]
    public GameObject FinishGate;
    public GameObject SpanwnGate;

    [Header("Boss")]
    public GameObject BossPre;
    public GameObject BossSpawn;
    public int maxBoss;
    public bool spawnBoss = true;


    [Header("Enemy")]
    public GameObject enemyPrefab;
    public Transform[] enemySpawnPoints;
    public int maxEnemies;
    public int enemiesPerWave = 2;
    public float spawnDelay = 1f;
    private int numEnemiesSpawned = 0;
    private int numEnemiesKilled = 0;
    private bool isSpawning = false;
    private int enemiesToSpawn = 0;


    [Header("EnemyTurret")]
    public GameObject turretPre;
    public Transform[] spawnPosturret;
    public bool spawnTurret = true;

    [Header("Item")]
    public SpawnItemManager spawnItemManager;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        if (!PlayerPrefs.HasKey("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", 1);
        }
    }
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        Manager = FindObjectOfType<GameManager>();
        StartCoroutine(SpawnItems());
        tankIndex = PlayerPrefs.GetInt("SelectedTank", 0);
        CreatePlayer(tankIndex);
        numberOfCoins = PlayerPrefs.GetInt("NumberOfCoins", 0);
        gameOver.SetActive(false);
        gameWin.SetActive(false);
        gamePause.SetActive(false);
        UIPlayer.SetActive(true);
        StartSpawnEnemies();
        SpawnTurretEnemy();
    }
    void Update()
    {
        txtCoins.text = numberOfCoins.ToString();
        txtCoinGameWin.text = "Coin: " + numberOfCoins.ToString();
        txtCoinGameOver.text = "Coin: " + numberOfCoins.ToString();
    }
    void CreatePlayer(int index)
    {
        GameObject newPlayer = Instantiate(playerPrefab[index], transform.position, Quaternion.identity);
        Camera.main.GetComponent<CameraFollow>().SetTarget(newPlayer.transform);
    }
    public void SelectTank(int index)
    {
        PlayerPrefs.SetInt("SelectedTank", index);

        GameObject currentPlayer = GameObject.FindGameObjectWithTag("Player");
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }

        CreatePlayer(index);
    }
    public void PlayerDestroyed()
    {
        StopSpawnEnemies();
        if (!isGameWon)
        {
            ShowGameOver();
        }
    }
    public void ShowGameOver()
    {
        audioManager.PlaySFX(audioManager.losegame);
        Time.timeScale = 0f;
        UIPlayer.SetActive(false);
        gameOver.SetActive(true);
    }
    public void ShowGameWin()
    {
        audioManager.PlaySFX(audioManager.winlevel);
        UIPlayer.SetActive(false);
        gameWin.SetActive(true);
        isGameWon = true;
        Time.timeScale = 0f;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        Time.timeScale = 0f;     
        gamePause.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        gamePause.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isGameWon = false;
    }

    public void NextLevel()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt("levelsUnlocked", Mathf.Max(levelIndex, PlayerPrefs.GetInt("levelsUnlocked")));

        SceneManager.LoadScene(levelIndex);
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator SpawnEnemies()
    {
        isSpawning = true;

        enemiesToSpawn = enemiesPerWave;

        while (numEnemiesSpawned < maxEnemies && isSpawning)
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                int spawnIndex = Random.Range(0, enemySpawnPoints.Length);
                Instantiate(enemyPrefab, enemySpawnPoints[spawnIndex].position, Quaternion.identity);
                numEnemiesSpawned++;
                yield return new WaitForSeconds(spawnDelay);
            }

            enemiesToSpawn = Mathf.Min(maxEnemies - numEnemiesKilled - numEnemiesSpawned, enemiesPerWave);
        }


        if (numEnemiesKilled >= maxEnemies)
        {
            GameManager.instance.ShowGameOver();
        }
    }
    public bool IsGameWin()
    {
        return (numEnemiesKilled >= maxEnemies);
    }
    public void SpawnBoss()
    {
        if(maxBoss > 0 && spawnBoss)
        {
            Instantiate(BossPre, BossSpawn.transform.position, Quaternion.identity);
            maxBoss--;
        }
        else
        {
            Instantiate(FinishGate, SpanwnGate.transform.position, Quaternion.identity);

        }
    }

    public void SpawnTurretEnemy()
    {
        if (spawnTurret)
        {
            Instantiate(turretPre, spawnPosturret[0].transform.position, Quaternion.identity);
            Instantiate(turretPre, spawnPosturret[1].transform.position, Quaternion.identity);
            Instantiate(turretPre, spawnPosturret[2].transform.position, Quaternion.identity);
        }
    }

    public void StartSpawnEnemies()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnEnemies());
        }
    }
    public void StopSpawnEnemies()
    {
        isSpawning = false;
        StopAllCoroutines();
        SpawnBoss();
    }

    public void NotifyEnemyKilled()
    {
        numEnemiesKilled++;
        enemiesToSpawn--;

        if (enemiesToSpawn == 0)
        {
            StartSpawnEnemies();
        }
    }
    public void ResetKillCount()
    {
        KillCountManager.SetCurrentKillCount(SceneManager.GetActiveScene().buildIndex, 0);
    }

    IEnumerator SpawnItems()
    {
        yield return new WaitForSeconds(2f);
        spawnItemManager.StartCoroutine(spawnItemManager.SpawnItem());
    }
}