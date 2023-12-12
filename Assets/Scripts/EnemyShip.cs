using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{

    [SerializeField] GameObject enemyLaserBullet;

    [SerializeField] float bulletFrequencyPerSecond = 3f;
    [SerializeField] float bulletFrequencyVariance = 0.6f;
    [SerializeField] float bulletSpeedEnemy = 4f;
    [SerializeField] List<AudioClip> enemyLaserSounds;
    AudioSource audioSource;
    bool enemyCanShoot;
    [SerializeField] [Range(0f,1f)] float volumeScale = 0.3f;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ShootAndWait());
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ShootAndWait()
    {
        for (int i = 0; i<1000; i++)
        {
            if (IsEnemyInView())
            {
                GameObject bulletInstance = Instantiate(enemyLaserBullet, transform.position, Quaternion.identity);
                bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletSpeedEnemy);
                audioSource.PlayOneShot(enemyLaserSounds[GenerateRandomLaserSound()], volumeScale);
                yield return new WaitForSeconds(GetTimeBetweenBullets());
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
            }
            
        }
    }

    float GetTimeBetweenBullets()
    {
        float variableTimeBetweenBullets = Random.Range(bulletFrequencyPerSecond - bulletFrequencyVariance, bulletFrequencyPerSecond + bulletFrequencyVariance);
        float frequency = 1/variableTimeBetweenBullets;
        return frequency;
    }
    
    
    int GenerateRandomLaserSound()
    {
        int numOfSounds = enemyLaserSounds.Count;
        
        int randomNumber = Random.Range(0, numOfSounds);
        //Debug.Log("numofsounds = "+ numOfSounds + ", and randomnumber = " + randomNumber);
        return randomNumber;
        
        
    }

    bool IsEnemyInView()
    {
        Vector3 viewportMax = Camera.main.ViewportToWorldPoint(new Vector3 (1,1,0));
        Vector3 viewportMin = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        if (transform.position.x < viewportMax.x && transform.position.x > viewportMin.x)
        {
            return true;
        }
        else {return false;}
    }
}
