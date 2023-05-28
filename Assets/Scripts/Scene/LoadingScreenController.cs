using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider sliderLoad;
    public int levelsUnlocked;

    void Start()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);
    }

    public void LoadScene(int index)
    {
        StartCoroutine(LoadScene_Coroutine(index));
    }
    public IEnumerator LoadScene_Coroutine(int index)
    {
        sliderLoad.value = 0;
        LoadingScreen.SetActive(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = false;

        float progress = 0;
        float waitTime = 0.0009f;

        while (!asyncOperation.isDone)
        {
            float targetProgress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            progress = Mathf.MoveTowards(progress, targetProgress, Time.deltaTime);

            yield return new WaitForSeconds(waitTime);
            sliderLoad.value = progress;

            if (asyncOperation.progress >= 0.9f && progress >= 0.9f)
            {
                sliderLoad.value = 1f;
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void UpdateLevelUnlock(int level)
    {
        if (level > levelsUnlocked)
        {
            levelsUnlocked = level;
            PlayerPrefs.SetInt("levelsUnlocked", levelsUnlocked);
            PlayerPrefs.Save();
        }
    }
}
