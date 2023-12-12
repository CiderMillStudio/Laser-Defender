using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    Rigidbody2D enemyLaserRigidbody;
    //[SerializeField] float enemyLaserSpeed = 6f;
    [SerializeField] float timeBeforeBulletGoesPoof = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
        Destroy(gameObject, timeBeforeBulletGoesPoof);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
