using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

WaveConfigSO waveConfig;
EnemySpawner enemySpawner;
List<Transform> waypoints;

int waypointIndex = 0;

    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        waveConfig = enemySpawner.GetCurrentWave();
    }
    void Start()
    {
        waypoints = waveConfig.GetWayPoints();
        Transform startingPoint = waypoints[waypointIndex];
        transform.position = startingPoint.position;
    }



    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            
            Vector3 targetPoint = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, delta);

            if (transform.position == targetPoint)
            {
                waypointIndex++;
            }
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
