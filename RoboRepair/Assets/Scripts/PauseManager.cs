using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    GameObject[] pauseObjects;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        HidePaused();
    }

    // Update is called once per frame
    void Update()
    {
        //On pressing the escape key pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                ShowPaused();
            }
        }
    }

    //Display pause menu
    void ShowPaused()
    {
        foreach(GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hide pause menu
    void HidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //resume game
    public void Resume()
    {
        if( Time.timeScale == 0)
            {
                Time.timeScale = 1;
                HidePaused();
            }
    }

    //exit the game completely
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartOver()
    {
        SceneManager.LoadScene("MainScene");
    }
}
