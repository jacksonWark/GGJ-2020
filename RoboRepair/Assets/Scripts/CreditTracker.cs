using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditTracker : MonoBehaviour
{

    public int credits;
    public static CreditTracker instance;
    public Text credText;

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

    private void Start()
    {
        credText.text = "$1000";
        credits = 1000;
    }

    public void AddCredits(int cred)
    {
        credits += cred;
        credText.text = "$" + credits.ToString();
    }

    public void SpendCredits(int cred)
    {
        credits -= cred;
        credText.text = "$" + credits.ToString();
    }

    public int GetCreditBalance()
    {
        return credits;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
