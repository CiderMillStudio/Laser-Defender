using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D playerLaserRigidBody;
    [SerializeField] float playerLaserSpeed = 6f;
    void Start()
    {
        playerLaserRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerLaserRigidBody.velocity = new Vector2 (0f, playerLaserSpeed);
        if (transform.position.y > 10)
        {
            Destroy(gameObject);
        }
    }


    public float GetPlayerLaserSpeed()
    {
        return playerLaserSpeed;
    }
}
