using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    ScoreKeeper scoreKeeper;
    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        Debug.Log(scoreKeeper.GetScore());
        scoreText.text = "Score:\n" + scoreKeeper.GetScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

}
