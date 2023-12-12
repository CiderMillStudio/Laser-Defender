using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] float moveSpeed = 0.1f;

    Vector2 minBounds;
    Vector2 maxBounds;

    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    [SerializeField] GameObject playerLaserBullet;

    [SerializeField] float timeBetweenPlayerBullets = 0.1f;
    
    Coroutine firingContinuously;

    CameraShake cameraShake;

    AudioSource audioSource;

    [SerializeField] List<AudioClip> laserSounds;
    [SerializeField] [Range(0f,1f)] float shootingVolume = 0.5f;


    void Update()
    {
        Move();
    }

    void Start()
    {
        InitBounds();
        cameraShake = FindObjectOfType<CameraShake>();
        audioSource = GetComponent<AudioSource>();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2 (0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2 (1,1));

    }
    void Move()
    {
        Vector2 delta = moveInput * Time.deltaTime * moveSpeed;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        bool isFiring = value.isPressed;
        if (isFiring)
        {
            firingContinuously = StartCoroutine(MachineGunBullets());
        }
        else
        {
            StopCoroutine(firingContinuously);
        }
    }

    IEnumerator MachineGunBullets()
    {
        for (int i = 0; i<float.MaxValue; i++)
        {
            Instantiate(playerLaserBullet, transform.position, Quaternion.identity);
            audioSource.PlayOneShot(laserSounds[GenerateRandomLaserSoundIndex()], shootingVolume);
            // playerLaserBullet.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, playerLaserBullet.GetComponent<Laser>().GetLaserSpeed());
            // Debug.Log(playerLaserBullet.GetComponent<Laser>().GetLaserSpeed());
            yield return new WaitForSeconds(timeBetweenPlayerBullets);
        }
    }

    int GenerateRandomLaserSoundIndex()
    {
        int numOfSounds = laserSounds.Count;
        
        int randomNumber = Random.Range(0, numOfSounds);
        Debug.Log("numofsounds = "+ numOfSounds + ", and randomnumber = " + randomNumber);
        return randomNumber;
        
        
    }
}
