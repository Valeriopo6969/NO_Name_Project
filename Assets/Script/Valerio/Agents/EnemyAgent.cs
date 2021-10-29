using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class EnemyAgent : Agent
{
    [Header("Agents attributes")]
    [Tooltip("Force to apply when moving")]
    public float MovementForce = 2f;

    [Tooltip("Turning Speed")]
    [Range(0f,1f)]
    public float TurningSpeed = .5f;

    public Transform AreaCenter;

    /// <summary>
    /// Rigidbody of the agent
    /// </summary>
    new private Rigidbody rb;

    private float smoothTurn = 0f;

    //Attack distance
    private const float attackDistance = .2f;

    //Wheter the Agent is 'attacking'
    private bool attacking = false;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        MoveToSafeRandomPosition();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actions.ContinuousActions[0];
        controlSignal.z = actions.ContinuousActions[1];
        rb.AddForce(controlSignal * MovementForce);

        //will it works?
        transform.LookAt(transform.localPosition+rb.velocity.normalized, Vector3.up);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition); //3 obs

       
        sensor.AddObservation(rb.velocity.x); //3 obs
        sensor.AddObservation(rb.velocity.z); //3 obs

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
    private void MoveToSafeRandomPosition()
    {
        bool safePositionFound = false;
        int attemptsRemaining = 100;
        Vector3 potentialPosition = Vector3.zero;
        Quaternion potentialRotation = Quaternion.identity;

        while (!safePositionFound && attemptsRemaining > 100)
        {
            attemptsRemaining--;
            potentialPosition = AreaCenter.position + new Vector3(UnityEngine.Random.insideUnitCircle.x*10f,1f, UnityEngine.Random.insideUnitCircle.y*10f);
            potentialRotation = Quaternion.Euler(0f, UnityEngine.Random.Range(-180f, 180f), 0f);

            Collider[] colliders = Physics.OverlapSphere(potentialPosition, .5f);

            safePositionFound = colliders.Length == 0;

            Debug.Assert(safePositionFound, "Could not found a safe position to spawn");

            transform.position = potentialPosition;
            transform.rotation = potentialRotation;
        }
       
    }
}
