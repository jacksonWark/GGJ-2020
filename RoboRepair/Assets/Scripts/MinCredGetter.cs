using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinCredGetter : MonoBehaviour
{

    public Store[] stores;
    public CreditTracker creditTracker;

    // Start is called before the first frame update
    void Start()
    {
        int min = 1000;
        foreach ( Store s in stores )
        {
            if (s.GetPrice() < min) min = s.GetPrice();
        }

        Debug.Log("MIN CRED = " + min.ToString());

        creditTracker.SetMinCred(min);
    }

}
