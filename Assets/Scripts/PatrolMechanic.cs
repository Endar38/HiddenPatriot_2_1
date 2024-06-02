using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrolMechanic : MonoBehaviour
{

    public Transform patrolPointsHolder;
    public float movementSpeed = 2.0f;
    public float rotationSpeed = 5.0f;
    public bool rotate = true;
    public bool backtrack = true;
    public bool loop;

    private Rigidbody2D rb;
    private List<Transform> patrolPoints;
    private Transform nextPatrolPoint;
    private int index;

    //Getting a refrence to the RigidBody2D and getting all the transforms on the patrolPointsHolder object
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        patrolPoints = new List<Transform>();

        // Get all the transforms on the patrolPointsHolder object
        foreach (Transform child in patrolPointsHolder)
        {
            patrolPoints.Add(child);
        }

        // Set the initial rotation based on the direction to the first patrol point
        if (patrolPoints.Count > 0)
        {
            Vector2 directionToFirstPatrolPoint = patrolPoints[0].position - transform.position;
            float angleToFirstPatrolPoint = Mathf.Atan2(directionToFirstPatrolPoint.y, directionToFirstPatrolPoint.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angleToFirstPatrolPoint, Vector3.forward);
        }
    }


    //Calling the PatrolLogic methode every frame
    private void FixedUpdate()
    {
        PatrolLogic();
    }

    //All of the logic for the patrol mechanics and rotation
    private void PatrolLogic()
    {
        nextPatrolPoint = patrolPoints[index];
        if (Vector2.Distance(transform.position, nextPatrolPoint.position) < 0.1f)
        {
            index++;

            if (index == patrolPoints.Count && loop || index == patrolPoints.Count && backtrack)
                index = 0;

            if (backtrack)
                patrolPoints.Reverse();
        }
        rb.MovePosition(Vector2.MoveTowards(rb.position, nextPatrolPoint.position, movementSpeed * Time.fixedDeltaTime));

        if (rotate)
        {
            Vector2 directionToPatrolPoint = nextPatrolPoint.position - transform.position;
            float angleToPatrolPoint = Mathf.Atan2(directionToPatrolPoint.y, directionToPatrolPoint.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angleToPatrolPoint, Vector3.forward), rotationSpeed * Time.fixedDeltaTime);
        }
    }



}
