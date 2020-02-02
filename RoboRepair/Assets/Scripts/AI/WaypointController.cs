using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    private Transform targetWaypoint;
    private int targetWaypointIndex = 0;
    private float minDistance = 0.7f;
    private int lastWaypointIndex;
    public float movementSpeed = 3.0f;
    public float rotationSpeed = 2.0f;
    //public Vector3 lastLocation;

    // Start is called before the first frame update
    void Start()
    {
        lastWaypointIndex = waypoints.Count - 1;
        targetWaypoint = waypoints[targetWaypointIndex];
        //lastLocation = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float movementStep = movementSpeed * Time.deltaTime;
        float rotationStep = rotationSpeed * Time.deltaTime;
        

        Vector3 directionToTarget = targetWaypoint.position - transform.position;
        Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);        
        //transform.rotation = rotationToTarget;

        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        CheckDistanceToWaypoint(distance);        

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movementStep);

        
    }

    void CheckDistanceToWaypoint(float currentDistance)
    {
        //Debug.Log("DtW:" + currentDistance);
        if(currentDistance <= minDistance)
        {
            targetWaypointIndex++;
            UpdateTargetWaypoint();
        }
    }

    void UpdateTargetWaypoint()
    {
        if (targetWaypointIndex > lastWaypointIndex)
        {
            targetWaypointIndex = 0;
        }
        //Debug.Log("targetIndex:" + targetWaypointIndex);
        //Debug.Log("lastIndex:" + lastWaypointIndex);
        targetWaypoint = waypoints[targetWaypointIndex];
    }
}
