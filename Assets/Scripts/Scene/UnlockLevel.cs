using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UnlockLevel : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] LoadingScreenController loadingScreenController;

    void Start()
    {
        loadingScreenController = FindObjectOfType<LoadingScreenController>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < loadingScreenController.levelsUnlocked; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void LoadLevel(int levelIndex)
    {
        GameManager.numberOfCoins = 0;
        loadingScreenController.UpdateLevelUnlock(levelIndex);
        loadingScreenController.LoadScene(levelIndex);
    }
}