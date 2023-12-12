using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "WaveConfig", fileName = "New WaveConfig")] //this is an ATTRIBUTE!

public class WaveConfigSO : ScriptableObject
{
   [SerializeField] Transform pathPrefab; //this represents the parent object "Path 1", "Path 2", etc..., each storing unique waypoints (elements) that can be accessed in the Pathfinder script.
   [SerializeField] float moveSpeed = 5f;
   [SerializeField] List<GameObject> enemyPrefabs; //this is where we'll store enemies that will be spawned by the EnemySpawner script
   
    [SerializeField] float timeDelay = 0.8f;
    [SerializeField] float spawnTimeVariance = 0.5f;

    [SerializeField] float minimumSpawnTime = 0.2f;
   public Transform GetStartingWaypoint()
   {
        return pathPrefab.GetChild(0);
   }

   public List<Transform> GetWayPoints()
   {
        List<Transform> waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;
   }

   public float GetMoveSpeed()
   {
    return moveSpeed;
   } 

   public int GetEnemyCount() // allows other scripts (enemyspawner script) to obtain the number of enemies
   {
        return enemyPrefabs.Count;
   }

   public GameObject GetEnemyPrefab(int index)
   {
        return enemyPrefabs[index];
   }

   public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeDelay - spawnTimeVariance, timeDelay + spawnTimeVariance);

        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);

    }



}
