
using System;
using UnityEngine;


public class NPC : MonoBehaviour
{
    public float speed;
    public float waitTime;
    public float startWaitTime;
    public float Radius;
    public Transform target;
    public Transform AreaCenter;
   
    private void Start()
    {
        
        waitTime = startWaitTime;
       
        RandomizeSpeed();
        AssignNewTargetPosition(AreaCenter,Radius);
    }

    private void Update()
    {
        
        

        
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position,target.position)<.2f)
        {
            if(waitTime<=0)
            {
                waitTime = startWaitTime;
                AssignNewTargetPosition(AreaCenter,Radius);
                RandomizeSpeed();
            } else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void AssignNewTargetPosition(Transform center,float radius)
    {
        target.position = center.position + new Vector3(UnityEngine.Random.insideUnitCircle.x, 0.0f, UnityEngine.Random.insideUnitCircle.y) * Radius;
        
    }

    void RandomizeSpeed()
    {
        speed = UnityEngine.Random.Range(3f, 5f);
    }

}
    

    





