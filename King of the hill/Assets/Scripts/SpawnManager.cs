using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handles spawning waves of Enemies and Powerups
/// </summary>
public class SpawnManager : MonoBehaviour
{
    public static int WaveNumber { get; private set; }
    [SerializeField] private GameUI gameUI;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject powerupPrefab;
    private float spawnRange = 9.0f;
    private int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        WaveNumber = 1;
        SpawnEnemyWave(WaveNumber);
        SpawnPowerup();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyBehaviour>().Length;
        if (enemyCount == 0)
        {
            WaveNumber++;
            SpawnEnemyWave(WaveNumber);
            SpawnPowerup();
        }
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        gameUI.SetCurrentWave();
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private void SpawnPowerup()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(spawnPosX, 0.5f, spawnPosZ);
        return spawnPosition;
    }
}
