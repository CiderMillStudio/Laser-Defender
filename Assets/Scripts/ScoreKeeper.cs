using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    // Start is called before the first frame update
    int score = 0;

    
    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        int numScoreKeepers = FindObjectsOfType<ScoreKeeper>().Length;
        if (numScoreKeepers > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        
    }

    public int GetScore()
    {
        return score;
    }

    public void ModifyScore(int modifier)
    {
        score += modifier;
        Mathf.Clamp(score, 0, int.MaxValue);
    }

    public void ResetScore()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScoreKeeper()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
