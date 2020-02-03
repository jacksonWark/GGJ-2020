using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour, IInteractable
{
    SphereCollider trigger;

    private string infoString;
    private float breakTime;
    private int reward;

    //Type of break
    // { 0 = HEAD, 1 = ARMS, 2 = LEGS, 3 = COGS, 4 = NOT BROKEN}
    private int breakType;

    private void Awake()
    {
        trigger = GetComponent<SphereCollider>();
        trigger.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        breakType = 4;
        breakTime = Random.Range(10f, 60f);
        StartCoroutine(BreakTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Break()
    {
        breakType = Random.Range(0, 4);
        reward = Random.Range(5, 301);
        infoString = breakType.ToString() + " " + reward.ToString();
        //enable trigger collider
        trigger.enabled = true;
    }

    public string Detect()
    {
        return infoString;
    }

    public bool Interact()
    {
        if (breakType != 4)
        {
            breakType = 4;
            breakTime = Random.Range(10f, 601f);
            trigger.enabled = false;
            StartCoroutine(BreakTimer());
            return true;
        }
        else return false;
    }

    private IEnumerator BreakTimer()
    {
        Debug.Log("Start Timer");
        //Wait for set amount of time before breaking
        yield return new WaitForSeconds(breakTime);
        Debug.Log("End Timer");
        Break();
    }
}
