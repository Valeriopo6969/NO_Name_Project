using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locomotion_seek : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    [Header("seek variables")]

    public Transform target_transform;
    public float max_velocity = 10;
    public float max_force = 0.25f;
    public float rotation_rate;

    [Header("charge variables")]

    public float distance;
    public float wait_time = 2;
    public float acceleration = 3;
    public float deceleration_rate = 2;
    #endregion
    #region PRIVATE_VARIABLES

    private Quaternion charge_direction; 

    private Rigidbody _rb;
    /// <summary>
    /// Return the current target's position
    /// </summary>
    private Vector3 __target { get { return target_transform.position; } }
    private Vector3 __desidered_velocity { get {return (__target - this.transform.position).normalized; }}
    private Vector3 __forward_velocity { get { return this.transform.forward * max_velocity; } }
    
    #endregion

    private void Update()
    {
        //if(Vector3.Distance(this.transform.position, __target)>distance)Perform_seek();

        
        if(wait_time>0)
        {
            wait_time -= Time.deltaTime;
            charge_direction = Quaternion.LookRotation(__desidered_velocity, Vector3.up);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, charge_direction, Time.deltaTime * rotation_rate * rotation_rate);
        }
       
        if (wait_time<0)
        {
            this.transform.position += __forward_velocity * Time.deltaTime * (acceleration);
            if (acceleration > 0) acceleration -= Time.deltaTime *  deceleration_rate;
            else
            {
                wait_time = 2;
                acceleration = 3;
            }
        }


       
    }

    #region PRIVATE_METHODS
    private void Perform_seek()
    {
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(__desidered_velocity, Vector3.up), Time.deltaTime * rotation_rate);

        this.transform.position += __forward_velocity * Time.deltaTime;
    }


    
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(this.transform.position, __target);
        Gizmos.DrawWireSphere(__target, distance);
    }
    

    #endregion
}
