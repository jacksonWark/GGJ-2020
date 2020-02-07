using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour, IInteractable
{
    SphereCollider trigger;
    WaypointController waypointController;
    Animator animator;

    private string infoString;
    private float breakTime;
    private int reward;

    // For random movement
    private bool isIdle = true;
    private float timer = 0;

    //Type of break
    // { 0 = HEAD, 1 = ARMS, 2 = LEGS, 3 = COGS, 4 = NOT BROKEN}
    private int breakType = 4;

    private void Awake()
    {
        trigger = GetComponent<SphereCollider>();
        waypointController = GetComponent<WaypointController>();
        animator = GetComponent<Animator>();
        trigger.enabled = false;
    }

    // Start is called before the first frame update. Generate a random interval, and start a timer that tells robot when to break
    void Start()
    {
        //breakTime = Random.Range(10f, 60f);
        breakTime = 5;
        StartCoroutine(BreakTimer());
    }

    // Update is called once per frame. If we are broken, move randomly, otherwise patrol along waypoints
    void Update()
    {
        if (breakType == 4) waypointController.Move();
        else RandomMovement();
    }

    private void FixedUpdate()
    {
        if (breakType != 4) // and player not nearby
        {
            timer -= Time.deltaTime;
        }
    }

    private void Break()
    {
        // Change animation to idle
        animator.SetBool("isIdle", true);
        // Pick random type of break
        breakType = Random.Range(0, 4);
        // Set random reward for repair - TODO make this parameterized
        reward = Random.Range(50, 301);
        // Create string to broadcast information to player UI
        infoString = breakType.ToString() + " " + reward.ToString();
        //enable trigger collider so player can detect robot
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
            // Set to not broken
            breakType = 4;
            // Create new random interval until next break
            breakTime = Random.Range(10f, 601f);
            // Disable detection collider
            trigger.enabled = false;
            // Start break interval timer
            StartCoroutine(BreakTimer());
            // Set animation back to walking
            animator.SetBool("isIdle", false);
            return true;
        }
        else return false;
    }

    private void RandomMovement()
    {
        if (!isIdle)
        {
            if (timer <= 0)
            {
                // Set state to idle, change animation, and wait a random amount of time
                isIdle = true;
                animator.SetBool("isIdle", true);
                timer = Random.Range(1, 6);
            }
        }
        else
        {
            if (timer <= 0)
            {
                // Set state to moving, turn in a random direction, change animation, and move for a random amount of time
                transform.rotation = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);
                timer = Random.Range(1, 6);
                animator.SetBool("isIdle", false);
                isIdle = false;
            }
        }
    }

    private IEnumerator BreakTimer()
    {
        //Debug.Log("Start Timer");
        //Wait for set amount of time before breaking
        yield return new WaitForSeconds(breakTime);
        //Debug.Log("End Timer");
        Break();
    }
}
