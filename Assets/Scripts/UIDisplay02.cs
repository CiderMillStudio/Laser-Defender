using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay02 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scorePanelText;

    [SerializeField] Slider healthSlider;

    ScoreKeeper scoreKeeper;

    Health healthScript;

    Player player;

    int healthRemaining;

    int initialHealth;

    int score;

    HealthSliderFill healthSliderFill;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        player = FindObjectOfType<Player>();
        healthScript = player.GetComponent<Health>();

        initialHealth = healthScript.GetHealth();
        healthSlider.value = 1f;
        healthSliderFill = FindObjectOfType<HealthSliderFill>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateScore();

        

    }

    private void UpdateHealth()
    {
        healthRemaining = healthScript.GetHealth();
        healthSlider.value = (float)healthRemaining/initialHealth;

    }

    void UpdateScore()
    {
        score = scoreKeeper.GetScore();
        scorePanelText.text = score.ToString();
    }


    public void TurnOffSliderFill()
    {
        healthSliderFill.InactivateFill();
    }
}
