using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    
    [SerializeField] int health = 50;
    
    [Header("Particle Effects")]
    [SerializeField] ParticleSystem explodeShip1;
    [SerializeField] ParticleSystem explodeShip2;

    [Header("Camera Effects")]
    [SerializeField] bool applyCameraShake;

    CameraShake cameraShake;
    [SerializeField] GameObject redOverlayOnHit;
    [SerializeField] float unReddenSpeed = 0.1f;

    AudioSource audioSource;

    [SerializeField] AudioClip deathExplosionSound;
    [SerializeField] [Range(0f,1f)] float volumeScale = 0.1f;
    [SerializeField] AudioClip hitExplosionSound;
    [SerializeField] [Range(0f, 1f)] float hitVolumeScale = 0.1f;
    
    ScoreKeeper scoreKeeper;

    UIDisplay02 uiDisplay;

    LevelManager levelManager;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        redOverlayOnHit.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        
        uiDisplay = FindObjectOfType<UIDisplay02>();
        levelManager = FindObjectOfType<LevelManager>(); //moo
    }

    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        int damageDealtByCollider = other.GetComponent<DamageDealer>().GetDamage();

        if (damageDealer != null)
        {
                       
            if (gameObject.tag == "Player" && other.tag == "Projectile Enemy")
            {
                damageDealer.Hit();
                TakeDamage(damageDealtByCollider);
                AudioSource.PlayClipAtPoint(hitExplosionSound, Camera.main.transform.position, hitVolumeScale);
                ParticleSystem explode1Instance = Instantiate(explodeShip1, transform.position, Quaternion.identity);
                explode1Instance.Play();
                Destroy(explode1Instance.gameObject, explode1Instance.main.duration + explode1Instance.main.startLifetime.constantMax);
                
                
                
            }
            else if (gameObject.tag == "Enemy" && other.tag == "Projectile Player")
            {
                damageDealer.Hit();
                TakeDamage(damageDealtByCollider);
                ParticleSystem explode1Instance = Instantiate(explodeShip1, transform.position, Quaternion.identity);
                explode1Instance.Play();
                Destroy(explode1Instance.gameObject, explode1Instance.main.duration + explode1Instance.main.startLifetime.constantMax);
            }
            else if (gameObject.tag == "Player" && other.tag == "Enemy")
            {
                damageDealer.Hit();
                TakeDamage(damageDealtByCollider);
                ParticleSystem explode1Instance = Instantiate(explodeShip1, other.transform.position, Quaternion.identity);
                explode1Instance.Play();
                Destroy(explode1Instance.gameObject, explode1Instance.main.duration + explode1Instance.main.startLifetime.constantMax);


            }
            else
            {
                Debug.Log("Something Else Happened!!! (see OnTriggerEnter2D in Health script)");
            }
        }
    }

    void TakeDamage(int damage)
    {
            if(cameraShake != null && applyCameraShake)
            {
                cameraShake.PlayCameraShake();
                StartCoroutine(ReddenScreenOnHit());
                //StartCoroutine(ReddenScreenOnHit());

            }
            
            health -= damage; //take damage
            if (health <= 0)
            {
                if (GetComponent<EnemyShip>() != null)
                {
                    scoreKeeper.ModifyScore(10);
                    Debug.Log(scoreKeeper.GetScore());
                }
                if (GetComponent<Player>() != null)
                {
                    //scoreKeeper.ModifyScore(-100); //save for when I have "multiple lives" implemented.
                     Debug.Log(scoreKeeper.GetScore());
                    uiDisplay.TurnOffSliderFill();
                    levelManager.LoadGameOver();

                }

                Destroy(gameObject);
                ParticleSystem explode2Instance = Instantiate(explodeShip2, transform.position, Quaternion.identity);
                ParticleSystem explode1Instance = Instantiate(explodeShip1, transform.position, Quaternion.identity);
                explode1Instance.Play();
                explode2Instance.Play();
                Destroy(explode2Instance.gameObject, explode2Instance.main.duration + explode2Instance.main.startLifetime.constantMax);
                Destroy(explode1Instance.gameObject, explode1Instance.main.duration + explode1Instance.main.startLifetime.constantMax);
                AudioSource.PlayClipAtPoint(deathExplosionSound, Camera.main.transform.position, volumeScale);
                

            }
    }

    IEnumerator ReddenScreenOnHit()
    {
        GameObject redOverlayInstance = Instantiate(redOverlayOnHit, new Vector3(0,0,0), Quaternion.Euler(new Vector3 (0,0,270)));
        redOverlayInstance.SetActive(true);
        SpriteRenderer redSpriteRenderer = redOverlayInstance.GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(0.2f);
        Color initialColor = redSpriteRenderer.color;
        float initialAlpha = redSpriteRenderer.color.a;
        float alphaTimer = initialAlpha * 100;
        for (int i = 0; i<alphaTimer; i++)
        {
            redSpriteRenderer.color = new Color (redSpriteRenderer.color.r, redSpriteRenderer.color.g, redSpriteRenderer.color.b, initialAlpha - i * unReddenSpeed);
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(redOverlayInstance, 3f);
    }


    public int GetHealth()
    {
        return health;
    }


}
