using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditTracker : MonoBehaviour
{

    public int score;
    public static CreditTracker instance;

    //If there is a score tracker, replace it with this one that is not deleted on scene change
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void addCredits(int credits)
    {

    }

    public void spendCredits(int credits)
    {

    }

    public void GameOver(int endScore)
    {
        score = endScore;
        SceneManager.LoadScene("GameOver");
    }
}
