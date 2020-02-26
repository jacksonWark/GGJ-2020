using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{

    public CreditTracker creditTracker;
    public AudioSource audioSource;
    // UI manager?
    public Text UIText;
    public Text badText;
    public Text[] parts = new Text[4];

    //public enum partTypes : int { HEAD, ARMS, LEGS, COGS }
    int[] inventory = new int[] { 0, 0, 0, 0 };
    public int invSize;
    int itemCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //inventory = new int[] { 0, 0, 0, 0 };
        //itemCount = 0;
        //UIText.enabled = false;
        //badText.enabled = false;
        foreach (Text t in parts)
        {
            t.text = "0";
        }
    }

    private void Update()
    {
        Drop();
    }

    /// <summary>
    /// Happens when we enter a store/robot detection collider and while we stay in it.
    /// Handles recieving UI info and interacting with both stores and robots. Potentially refactor to make easier to read.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        IInteractable subject = other.gameObject.GetComponent<IInteractable>();
        if (subject != null)
        {
            // Get info from robot or store
            string infoString = subject.Detect();
            // Prepare info to displayed on UI
            string[] info = infoString.Split(' ');
            if(info[0] == "0") { UIText.text = "Head"; }
            else if(info[0] == "1") { UIText.text = "Arm"; }
            else if(info[0] == "2") { UIText.text = "Leg"; }
            else { UIText.text = "Cog"; }

            UIText.text += "     $" + info[1] + "\nPress E to ";

            if (other.tag == "store") { UIText.text += "buy"; }
            else { UIText.text += "repair"; }

            // Prepare info to be used later
            int num = Convert.ToInt32(info[1]);
            int index = Convert.ToInt32(info[0]);

            //Display/enable UI text
            UIText.enabled = true;

            if (Input.GetKeyUp(KeyCode.E))
            {
                // Is this a store or a robot
                if (other.gameObject.tag == "store")
                {
                    //Debug.Log(num.ToString() + " " + creditTracker.getCreditBalance().ToString());
                    // Do we have enough inventory space
                    if (itemCount > 9)
                    {
                        badText.text = "Not enough space to carry!";
                    }
                    else
                    {
                        // Do we have enough money
                        if (creditTracker.GetCreditBalance() >= num)
                        {
                            // Remove credits and add parts to inventory
                            subject.Interact();
                            creditTracker.SpendCredits(num);
                            audioSource.Play();
                            inventory[index]++;
                            itemCount++;
                            RefreshUI();
                        }
                        else
                        {
                            badText.text = "You need more money!";
                            badText.enabled = true;
                        }
                    }
                }
                else
                {
                    // Do we have enough on the appropriate part
                    if (inventory[index] > 0)
                    {
                        // Were there no errors on the robot side of things
                        if (subject.Interact())
                        {
                            // Remove parts from inventory and add credits
                            creditTracker.AddCredits(num);
                            audioSource.Play();
                            inventory[index]--;
                            itemCount--;
                            RefreshUI();
                            // Blank UI text since robot will be changing states shortly
                            UIText.text = "";
                        }
                    }
                    else
                    {
                        badText.text = "You need more parts!";
                        badText.enabled = true;
                    }
                }   
            }
        }
    }

    /// <summary>
    /// Happens when we leave the detection collider of a robot or store. Blanks UI elements
    /// </summary>
    private void OnTriggerExit()
    {
        //disable UIText
        //UIText.enabled = false;
        //badText.enabled = false;
        UIText.text = "";
        badText.text = "";
    }

    /// <summary>
    /// Refresh UI for counting parts in inventory
    /// </summary>
    void RefreshUI()
    {
        int i = 0;
        foreach (Text t in parts)
        {
            t.text = inventory[i].ToString();
            i++;
        }
    }

    /// <summary>
    /// Drop a specific part and refresh the number being displayed on the counter
    /// </summary>
    void Drop()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (inventory[0] > 0)
            {
                inventory[0]--;
                itemCount--;
                RefreshUI();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if (inventory[1] > 0)
            {
                inventory[1]--;
                itemCount--;
                RefreshUI();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            if (inventory[2] > 0)
            {
                inventory[2]--;
                itemCount--;
                RefreshUI();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            if (inventory[3] > 0)
            {
                inventory[3]--;
                itemCount--;
                RefreshUI();
            }
        }

    }
}
