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
        credText.text = "$1";
        credits = 1;
    }

    public void addCredits(int cred)
    {
        credits += cred;
        credText.text = "$" + credits.ToString();
    }

    public void spendCredits(int cred)
    {
        credits -= cred;
        credText.text = "$" + credits.ToString();
    }

    public int getCreditBalance()
    {
        return credits;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
