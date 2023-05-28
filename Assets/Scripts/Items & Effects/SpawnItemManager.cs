using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInfo
{
    public GameObject itemPrefab;
    public float spawnRate;
}

public class SpawnItemManager : MonoBehaviour
{
    public ItemInfo[] itemInfos;
    public float spawnTime = 2f;
    public Transform[] spawnPoints;
    public bool spawnOnStart = false;

    public int spawnCountPerSpawn = 4;
    public int totalSpawnCount = 5;

    private float totalSpawnRate;
    private int currentSpawnCount = 0;

    private List<Transform> availableSpawnPoints;

    void Start()
    {
        totalSpawnRate = 0f;
        foreach (ItemInfo itemInfo in itemInfos)
        {
            totalSpawnRate += itemInfo.spawnRate;
        }
        availableSpawnPoints = new List<Transform>(spawnPoints);

        if (spawnOnStart)
        {
            StartCoroutine(SpawnItem());
        }
    }

    public IEnumerator SpawnItem()
    {
        while (currentSpawnCount < totalSpawnCount)
        {
            int numOfSpawn = Mathf.Min(spawnCountPerSpawn, availableSpawnPoints.Count);

            for (int i = 0; i < numOfSpawn; i++)
            {
                float randomSpawnRate = Random.Range(0f, totalSpawnRate);

                GameObject selectedItemPrefab = null;
                float itemSpawnRateSum = 0f;

                for (int j = 0; j < itemInfos.Length; j++)
                {
                    itemSpawnRateSum += itemInfos[j].spawnRate;

                    if (randomSpawnRate < itemSpawnRateSum)
                    {
                        selectedItemPrefab = itemInfos[j].itemPrefab;
                        break;
                    }
                }

                if (selectedItemPrefab != null)
                {
                    Transform spawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];

                    Instantiate(selectedItemPrefab, spawnPoint.position, Quaternion.identity);

                    availableSpawnPoints.Remove(spawnPoint);
                }
            }

            currentSpawnCount++;

            if (currentSpawnCount < totalSpawnCount)
            {
                yield return new WaitForSeconds(spawnTime);
            }
        }
    }
}