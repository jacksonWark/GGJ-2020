using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour, IInteractable
{
    #region Fields
    //public enum StoreType : int { HEAD, ARMS, LEGS, COGS}

    public int type;

    private string infoString;

    private int price;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        price = Random.Range(5, 101);
        infoString = type.ToString() + " " + price.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string Detect()
    {
        return infoString;
    }

    public bool Interact()
    {
        return true;
    }

}
