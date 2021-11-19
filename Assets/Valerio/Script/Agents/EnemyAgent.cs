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
    public float moveForce = 2f;

    [Tooltip("Rotation Speed")]
    public float rotationSpeed = 100f;

    public Transform noseTip;
    public Transform currentTarget;
    public Transform AreaCenter;
    public Transform body;
    
    /// <summary>
    /// Rigidbody of the agent
    /// </summary>
    private Rigidbody rb;
    float smoothRotationChange;
    

    

    public override void Initialize()
    {
        rb = GetComponentInChildren<Rigidbody>();
        
    }

    public override void OnEpisodeBegin()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (UnityEngine.Random.Range(0, 11) % 2 == 0)
        {
            transform.position = AreaCenter.position;
            body.position = new Vector3(AreaCenter.position.x, AreaCenter.position.y + 0.8f, AreaCenter.position.z);
        }
        else
        {
            
            body.position = new Vector3(currentTarget.position.x, 0.8f, currentTarget.position.z);
        }
        Debug.Log(body.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float[] continuosActions = actions.ContinuousActions.Array;
        // Actions, size = 2

        Vector3 move = new Vector3(continuosActions[0], 0, continuosActions[1]);

        //Add force in the direction of move vector
        rb.AddForce(move * moveForce);

        Vector3 currentRotation = body.rotation.eulerAngles;
        float rotationChange = continuosActions[2];

        smoothRotationChange = Mathf.MoveTowards(smoothRotationChange, rotationChange, 2f * Time.fixedDeltaTime);
        float yRotation = currentRotation.y + smoothRotationChange * Time.fixedDeltaTime * rotationSpeed;

        body.rotation = Quaternion.Euler(0, yRotation, 0f);
        
        if(Vector3.Distance(body.position,currentTarget.position)<5.5f)
        {
           //float bonus = .02f * Mathf.Clamp01(Vector3.Dot((transform.forward.normalized), -currentTarget.up.normalized));
           AddReward(1f);
           
           Debug.DrawLine(body.position, currentTarget.position, Color.red);
        }
        else
        {
            Debug.DrawLine(body.position, currentTarget.position, Color.green);
        }

        

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(body.position.normalized); //4 obs

        Vector3 toTarget = currentTarget.position - noseTip.position;
        sensor.AddObservation(toTarget.normalized); //3 obs

        //Observe a dot product that indicates whether the beak tip is in front of the flower(1 observation)
        //(+1 means directly in front, -1 directly behind)
        sensor.AddObservation(Vector3.Dot(toTarget.normalized, currentTarget.up.normalized));

        //Observe a dot product that indicates whether the beak is pointing toward the flower(1 observation)
        //(+1 means that is pointing directly at the flower, -1 means directly behind)
        sensor.AddObservation(Vector3.Dot(noseTip.forward.normalized, -currentTarget.up.normalized));

        //Observe the relative distance from the beak tip to the flower (1 observation)
        sensor.AddObservation(toTarget.magnitude / 20);

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        float[] continuosActionsOut = actionsOut.ContinuousActions.Array;

        //Create placeholders for all movement/turning
        Vector3 forward = Vector3.zero;
        Vector3 left = Vector3.zero;
        
        float yaw = 0f;

        //Convert keyboard inputs to movement and turning
        //All values should be between -1 and +1

        //Forward/backward
        if (Input.GetKey(KeyCode.W)) forward = body.forward;
        else if (Input.GetKey(KeyCode.S)) forward = -body.forward;
        //Left/right
        if (Input.GetKey(KeyCode.A)) left = -body.right;
        else if (Input.GetKey(KeyCode.D)) left = body.right;







        //Turn left /right
        if (Input.GetKey(KeyCode.LeftArrow)) yaw = -1f;
        else if (Input.GetKey(KeyCode.RightArrow)) yaw = 1f;

        //COmbine movement vectors and normalize
        Vector3 combined = (forward + left).normalized;

        //Add the 3 movement values, pitch, and yaw to the actionsOut array
        continuosActionsOut[0] = combined.x;
        continuosActionsOut[1] = combined.z;
        continuosActionsOut[2] = yaw;
        
        
    }
    
}
