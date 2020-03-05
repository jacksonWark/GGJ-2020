using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour, IInteractable
{
    #region Fields
    // Type of part
    // { 0 = HEAD, 1 = ARMS, 2 = LEGS, 3 = COGS}
    public int type;

    private string infoString;

    private int price;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        price = Random.Range(5, 101);
        infoString = type.ToString() + " " + price.ToString();
    }

    public string Detect()
    {
        return infoString;
    }

    public bool Interact()
    {
        return true;
    }

    public int GetPrice()
    {
        return price;
    }

}
