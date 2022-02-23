using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //public Animator Animator;
    public float moveSpeed = 2;
    public float jumpForce = 6;
    public float rotationBoost = 1;
    //public PoolManager PoolMng;
    
    GameObject SpawnPoint;
    float modVal = 1f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnPoint = GameObject.Find("SpawnPoint");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        //if (Input.GetKeyDown(KeyCode.Space))
        //    PoolMng.SpawnObj(SpawnPoint.transform.position, SpawnPoint.transform.rotation);
    }

    //void FixedUpdate()
    //{
    //    var x = Input.GetAxis("Horizontal");
    //    var z = Input.GetAxis("Vertical");

    //    if (Input.GetKey(KeyCode.LeftShift))
    //        modVal = 2f;

    //    Animator.SetFloat("Speed", z * modVal * VSpeed);
    //    transform.Rotate(0, x * rotationBoost, 0);        
    //}

    void Move()
    {
        var x = InputManager.HORIZONTALMOVE;
        var z = InputManager.VERTICALMOVE;

        Vector3 moveDirection = new Vector3(x, 0, z);
        //transform.rotation = Quaternion.LookRotation(moveDirection);

        rb.velocity = moveDirection.normalized * moveSpeed;
        //Animator.SetFloat("Speed", rb.velocity.magnitude); 

        if(InputManager.JUMPBUTTON)
        {
            rb.velocity = Vector3.up * jumpForce;
        }

        Debug.Log(rb.velocity);
    }
}
