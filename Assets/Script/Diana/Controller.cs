using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Animator Animator;    
    public float VSpeed;
    public KeyCode ModifierKey;
    public float rotationBoost = 1;

    float modVal = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        if (Input.GetKey(ModifierKey))
            modVal = 2f;

        Animator.SetFloat("Speed", z * modVal * VSpeed);
        transform.Rotate(0, x * rotationBoost, 0);
    }
}
