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
    private Rigidbody rb;
    
    

    

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
       

        float horInput = actions.ContinuousActions[0] * Time.fixedDeltaTime * 100;
        float vertInput = actions.ContinuousActions[1] * Time.deltaTime * 10;
        rb.MoveRotation(Quaternion.Euler(0, horInput, 0) * rb.rotation);
        if (vertInput != 0) rb.MovePosition(transform.position + transform.forward.normalized * vertInput);

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition); //3 obs

       
        sensor.AddObservation(rb.velocity.x); //1 obs
        sensor.AddObservation(rb.velocity.z); //1 obs

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
