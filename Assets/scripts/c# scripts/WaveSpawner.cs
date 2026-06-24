using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject spawnFireEffect; 
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f;
    public UIManager uiManager;

    private int waveNumber = 0;
    private bool spawning = false;

    void Update()
    {
        if (!spawning && GameObject.FindGameObjectsWithTag("enemy").Length == 0)
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        spawning = true;
        waveNumber++;

        if (uiManager != null)
            uiManager.UpdateWave(waveNumber);

        yield return new WaitForSeconds(timeBetweenWaves);

        int enemiesToSpawn = waveNumber * 2;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            
            if (spawnFireEffect != null)
            {
                GameObject fire = Instantiate(spawnFireEffect, spawnPoint.position, Quaternion.identity);
                Destroy(fire, 2f); 
            }

            yield return new WaitForSeconds(0.5f);

            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(0.5f);
        }

        spawning = false;
    }
}
