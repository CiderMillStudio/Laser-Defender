using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{   

    ScoreKeeper scoreKeeper;

    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void LoadGame()
    {
        
        
        SceneManager.LoadScene("Level 1");
        scoreKeeper.ResetScore();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        Invoke("LoadingGameOver", 1.5f);
    }

    void LoadingGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

   
}
