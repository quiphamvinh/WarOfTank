using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class KillCount : MonoBehaviour
{
    public int requiredKillCount;
    public int minusBoss; // -numkilledBoss
    public TextMeshProUGUI killCountText;

    public GameManager gameManager;

    private int currentSceneIndex;   


    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        KillCountManager.SetCurrentKillCount(currentSceneIndex, 0);
        CheckKillCount();
    }

    void OnEnable()
    {
        Enemy_Health.OnEnemyDie += AddKillCount;
    }

    void OnDisable()
    {
        Enemy_Health.OnEnemyDie -= AddKillCount;
    }

    public void AddKillCount(Enemy_Health enemy)
    {
        KillCountManager.IncreaseCurrentKillCount(currentSceneIndex);
        CheckKillCount();
    }

    void OnDestroy()
    {
        KillCountManager.SetCurrentKillCount(currentSceneIndex, 0);
    }

    private void CheckKillCount()
    {
        int currentKillCount = KillCountManager.GetCurrentKillCount(currentSceneIndex);

        if (killCountText != null)
        {
            killCountText.text = $"{currentKillCount}/{requiredKillCount}";
        }

        if (currentKillCount >= requiredKillCount - minusBoss)
        {
            //gameManager.ShowGameWin();
            gameManager.StopSpawnEnemies();
        }
    }
}