using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text scoretext;
    public CreditTracker tracker;
    
    // Start is called before the first frame update
    void Awake()
    {
        scoretext = FindObjectOfType<Text>();
        tracker = FindObjectOfType<CreditTracker>();
        scoretext.text = "Credits: " + tracker.GetCreditBalance().ToString("D6");
    }

    //exit the game completely
    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
