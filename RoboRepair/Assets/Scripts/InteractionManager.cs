using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{

    public CreditTracker creditTracker;
    // UI manager?
    public Text UIText;
    public Text badText;
    public Text[] parts = new Text[4];

    //public enum partTypes : int { HEAD, ARMS, LEGS, COGS }
    int[] inventory;
    public int invSize;
    int itemCount;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new int[] { 0, 0, 0, 0 };
        itemCount = 0;
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

    private void OnTriggerStay(Collider other)
    {
        IInteractable subject = other.gameObject.GetComponent<IInteractable>();
        if (subject != null)
        {
            string infoString = subject.Detect();
            string[] info = infoString.Split(' ');
            if(info[0] == "0") { UIText.text = "Head"; }
            else if(info[0] == "1") { UIText.text = "Arm"; }
            else if(info[0] == "2") { UIText.text = "Leg"; }
            else { UIText.text = "Cog"; }

            UIText.text += "     $" + info[1] + "\n\nPress E to ";

            if (other.tag == "store") { UIText.text += "buy"; }
            else { UIText.text += "repair"; }

            int num = Convert.ToInt32(info[1]);
            int index = Convert.ToInt32(info[0]);

            //Display/enable UI text
            UIText.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (other.gameObject.tag == "store")
                {
                    //Debug.Log(num.ToString() + " " + creditTracker.getCreditBalance().ToString());
                    if (itemCount > 9)
                    {
                        badText.text = "Not enough space to carry!";
                    }
                    else
                    {
                        if (creditTracker.GetCreditBalance() >= num)
                        {
                            subject.Interact();
                            creditTracker.SpendCredits(num);
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
                    if (inventory[index] > 0)
                    {
                        if (subject.Interact())
                        {
                            creditTracker.AddCredits(num);
                            inventory[index]--;
                            itemCount--;
                            RefreshUI();
                            UIText.text = "";
                        }
                    }
                    else
                    {
                        //activate badText
                        badText.text = "You need more parts!";
                        badText.enabled = true;
                    }
                }   
            }
        }
    }

    private void OnTriggerExit()
    {
        //disable UIText
        //UIText.enabled = false;
        //badText.enabled = false;
        UIText.text = "";
        badText.text = "";
    }


    void RefreshUI()
    {
        int i = 0;
        foreach (Text t in parts)
        {
            t.text = inventory[i].ToString();
            i++;
        }
    }

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
