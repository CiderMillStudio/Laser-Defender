using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] WaveConfigSO currentWave;

    [SerializeField] List<WaveConfigSO> waveList;

    [SerializeField] float timeBetweenWaves = 10f;

    [SerializeField] bool isLooping;


    void Start()
    {
        StartCoroutine(LoopThroughWaves());
    }


    IEnumerator SpawnEnemies(WaveConfigSO wave)
    {
        for (int i = 0; i < wave.GetEnemyCount(); i++)
        {
            GameObject enemy = wave.GetEnemyPrefab(i);
            Instantiate(enemy, wave.GetStartingWaypoint().position, Quaternion.identity, transform);
            yield return new WaitForSeconds(wave.GetRandomSpawnTime());    
        }

    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }


    IEnumerator LoopThroughWaves()
    {
        do
        {
            for (int i = 0; i < waveList.Count; i++)
            {
            currentWave = waveList[i];
            StartCoroutine(SpawnEnemies(waveList[i]));
            yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        while(isLooping);
    }


}


