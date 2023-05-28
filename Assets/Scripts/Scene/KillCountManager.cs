using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KillCountManager
{
    private const string CURRENT_KILL_COUNT_KEY_PREFIX = "CurrentKillCount_";

    public static void SetCurrentKillCount(int sceneIndex, int killCount)
    {
        PlayerPrefs.SetInt(CURRENT_KILL_COUNT_KEY_PREFIX + sceneIndex, killCount);
    }

    public static int GetCurrentKillCount(int sceneIndex)
    {
        return PlayerPrefs.GetInt(CURRENT_KILL_COUNT_KEY_PREFIX + sceneIndex, 0);
    }

    public static void IncreaseCurrentKillCount(int sceneIndex)
    {
        int currentKillCount = GetCurrentKillCount(sceneIndex) + 1;
        SetCurrentKillCount(sceneIndex, currentKillCount);
    }
}
