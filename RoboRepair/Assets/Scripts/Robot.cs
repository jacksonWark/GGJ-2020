using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour, IInteractable
{
    SphereCollider trigger;
    WaypointController waypointController;
    Animator animator;
    public AudioSource brokenSound;
    public AudioSource breakSound;
    public AudioSource repairSound;

    private string infoString;
    private float breakTimer;
    private int reward;
    private int tempReward;
    private int rewardStep;
    private float rewardTimer;

    public int MaxReward = 201;
    public int MinReward = 30;
    public float MaxBreakTime = 61;
    public float MinBreakTime = 25;

    // For random movement
    private bool isIdle = false;
    private float moveTimer = 0;

    // For detection
    private float detectTimer = 0;
    private bool changeFlag = false;

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
        breakTimer = Random.Range(MinBreakTime, MaxBreakTime);
        //breakTimer = 5;
        StartCoroutine(BreakTimer());
    }

    // FixedUpdate is called once per physics step. If we are broken, move randomly, otherwise patrol along waypoints
    private void FixedUpdate()
    {
        if (detectTimer > 0)
        {
            if (!isIdle)
            {
                isIdle = true;
                animator.SetBool("isIdle", true);
                changeFlag = true;
            }
            detectTimer -= Time.deltaTime;
        }
        else
        {
            if (changeFlag)
            {
                isIdle = !isIdle;
                if (isIdle) animator.SetBool("isIdle", true);
                else animator.SetBool("isIdle", false);
                changeFlag = false;
            }
        }

        

        if (breakType != 4 && detectTimer <= 0)
        {
            moveTimer -= Time.deltaTime;
            CalculateReward();
            RandomMovement();
        }

        if (breakType == 4) waypointController.Move();
    }

    private void Break()
    {
        breakSound.Play();
        // Change animation to idle
        isIdle = true;
        animator.SetBool("isIdle", true);
        // Pick random type of break
        breakType = Random.Range(0, 4);
        // Set random reward for repair
        tempReward = reward = Random.Range(MinReward, MaxReward);
        rewardStep = reward / 120;
        // Create string to broadcast information to player UI
        infoString = breakType.ToString() + " " + reward.ToString();
        //enable trigger collider so player can detect robot
        trigger.enabled = true;
        rewardTimer = 0;
    }

    public string Detect()
    {
        detectTimer = 2f;
        return infoString;
    }

    public bool Interact()
    {
        if (breakType != 4)
        {
            // Set to not broken
            breakType = 4;
            // Play repair sound
            repairSound.Play();
            // Create new random interval until next break
            breakTimer = Random.Range(MinBreakTime, MaxBreakTime);
            // Disable detection collider
            trigger.enabled = false;
            // Start break interval timer
            StartCoroutine(BreakTimer());
            // Set animation back to walking
            isIdle = false;
            animator.SetBool("isIdle", false);

            rewardTimer = 0;

            return true;
        }
        else return false;
    }

    private void RandomMovement()
    {
        if (!isIdle)
        {
            if (moveTimer <= 0)
            {
                // Set state to idle, change animation, and wait a random amount of time
                isIdle = true;
                animator.SetBool("isIdle", true);
                moveTimer = Random.Range(1, 6);
            }
        }
        else
        {
            if (moveTimer <= 0)
            {
                // Set state to moving, turn in a random direction, change animation, and move for a random amount of time

                transform.eulerAngles = transform.rotation.eulerAngles + new Vector3(0, Random.Range(90.0f, 270.0f), 0);
                moveTimer = Random.Range(1, 6);
                isIdle = false;
                animator.SetBool("isIdle", false);
                brokenSound.Play();
            }
        }
    }

    private void CalculateReward()
    {
        rewardTimer += Time.deltaTime;

        if (rewardTimer > 10)
        {
            if (reward - (rewardStep * (int)rewardTimer) >= 5)
            {
                tempReward = reward - (rewardStep* (int) rewardTimer);
                infoString = breakType.ToString() + " " + tempReward.ToString();
            }
            Debug.Log("Reward is " + tempReward.ToString());
        }
    }

    private IEnumerator BreakTimer()
    {
        //Debug.Log("Start Timer");
        //Wait for set amount of time before breaking
        yield return new WaitForSeconds(breakTimer);
        //Debug.Log("End Timer");
        Break();
    }
}
