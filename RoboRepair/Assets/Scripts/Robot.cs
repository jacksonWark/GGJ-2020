using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour, IInteractable
{

    private string infoString;
    private float breakTime;
    private int reward;

    //Type of break
    // { 0 = HEAD, 1 = ARMS, 2 = LEGS, 3 = COGS}
    private int breakType;

    private bool isBroken;

    // Start is called before the first frame update
    void Start()
    {
        isBroken = false;
        breakTime = Random.Range(10f, 601f);
        BreakTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Break()
    {
        breakType = Random.Range(0, 4);
        reward = Random.Range(5, 101);
        infoString = breakType.ToString() + " " + reward.ToString();
        //instantiate prefab with trigger collider
        isBroken = true;
       
    }

    public string Detect()
    {
        return infoString;
    }

    public void Interact()
    {
        isBroken = false;
        breakTime = Random.Range(10f, 601f);
        BreakTimer();
    }

    private IEnumerator BreakTimer()
    {
        //Wait for set amount of time before breaking
        yield return new WaitForSeconds(breakTime);

        Break();
    }
}
