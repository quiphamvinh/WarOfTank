using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Pass : MonoBehaviour
{
    public void pass()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
        {
           PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
        }
    }
    public void LoadScene(int levelIndex)
    {
        GameManager.numberOfCoins = 0;
        SceneManager.LoadScene(levelIndex);
    }
}
