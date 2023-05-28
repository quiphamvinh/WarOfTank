using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoxInfo
{
    public GameObject boxPrefab;
    public int spawnCount;
}

public class SpawnBoxManager : MonoBehaviour
{
    public BoxInfo[] boxInfos;
    public Transform[] spawnPoints;

    private List<Transform> availableSpawnPoints;
    private int currentSpawnIndex = 0;

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points specified!");
            return;
        }

        if (boxInfos.Length == 0)
        {
            Debug.LogError("No box info specified!");
            return;
        }

        availableSpawnPoints = new List<Transform>(spawnPoints);

        SpawnBoxes();
    }

    private void SpawnBoxes()
    {
        List<Transform> selectedSpawnPoints = new List<Transform>();

        for (int i = 0; i < 5; i++)
        {
            if (availableSpawnPoints.Count == 0)
            {
                break;
            }

            int spawnIndex = Random.Range(0, availableSpawnPoints.Count);
            Transform spawnPoint = availableSpawnPoints[spawnIndex];
            selectedSpawnPoints.Add(spawnPoint);
            availableSpawnPoints.RemoveAt(spawnIndex);
        }

        int totalSpawnCount = 0;

        foreach (BoxInfo boxInfo in boxInfos)
        {
            int spawnCount = boxInfo.spawnCount;
            GameObject boxPrefab = boxInfo.boxPrefab;

            for (int i = 0; i < spawnCount; i++)
            {
                if (totalSpawnCount >= 5)
                {
                    return;
                }

                Transform spawnPoint = selectedSpawnPoints[totalSpawnCount];
                Instantiate(boxPrefab, spawnPoint.position + Random.insideUnitSphere * 2f, Quaternion.identity);
                totalSpawnCount++;
            }
        }

        currentSpawnIndex++;

        if (currentSpawnIndex < spawnPoints.Length)
        {
            if (totalSpawnCount >= 5)
            {
                availableSpawnPoints = new List<Transform>(spawnPoints);
            }
        }
    }
}