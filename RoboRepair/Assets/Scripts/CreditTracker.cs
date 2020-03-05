using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditTracker : MonoBehaviour
{

    private int credits;
    public static CreditTracker instance;
    public Text credText;
    private int minCredits;

    public bool NoCredits { get { return credits < minCredits; } }

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

    public void SetMinCred(int cred)
    {
        minCredits = cred;
        Debug.Log("MIN CRED = " + cred.ToString());
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
