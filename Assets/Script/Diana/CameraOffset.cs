using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffset : MonoBehaviour
{
    public Transform parentObj;
    public Vector3 offset;    

    // Update is called once per frame
    void Update()
    {
        if (parentObj)
            transform.position = parentObj.position + offset;
    }
}
