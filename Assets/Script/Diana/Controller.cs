using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Animator Animator;    
    public float VSpeed;    
    public float rotationBoost = 1;
    public PoolManager PoolMng;
    
    GameObject SpawnPoint;
    float modVal = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPoint = GameObject.Find("SpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            PoolMng.SpawnObj(SpawnPoint.transform.position, SpawnPoint.transform.rotation);
    }

    void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
            modVal = 2f;

        Animator.SetFloat("Speed", z * modVal * VSpeed);
        transform.Rotate(0, x * rotationBoost, 0);        
    }
}
