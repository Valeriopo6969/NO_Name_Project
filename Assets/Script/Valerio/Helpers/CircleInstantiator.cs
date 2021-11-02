using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CircleInstantiator : MonoBehaviour
{
    [Header("Instantiator Parameter")]

    [Tooltip("Instantiate prefabs under this Transform if !null")]
    public Transform Parent;

    [Tooltip("# of prefab u want to instantiate")]
    public int N;

    //public bool RandomRotation, RandomScale;

    [Tooltip("If RandomScale is selected this would be the minimum random value for scaling")]
    public float RandomMinScale;

    [Tooltip("If RandomScale is selected this would be the maximum random value for scaling")]
    public float RandomMaxScale;

    [Tooltip("Ray of the Circle")]
    public float R;

    [Tooltip("Prefab u want to instantiate")]
    public GameObject Prefab;

    [Header("Controls")]

    public bool Start = true;
    public bool Clear = false;

    private List<GameObject> activePrefabs = new List<GameObject>();



    private void OnEnable()
    {
        if (!Parent) Parent = this.transform;

        if(Start)CircleInstantiate();
    }

    private void Update()
    {
        if(Start)
        {
            
            CircleInstantiate();
        }
        if (Clear)
        {
            Clear = false;
            Reset();
        }
    }
    private void CircleInstantiate()
    {
        Start = false;

        
        for (int i = 0; i < N; i++)
        {
            GameObject _go = Instantiate(Prefab, GetCirclePosition(), GetRandomRotation(false, true, false), Parent);
            _go.transform.localScale = GetRandomScale();
            activePrefabs.Add(_go);
        }
    }

    private Vector3 GetCirclePosition()
    {
        //Refactor
        return new Vector3(UnityEngine.Random.insideUnitCircle.x * R, transform.position.y, UnityEngine.Random.insideUnitCircle.y * R);
    }

    private Quaternion GetRandomRotation(bool x, bool y, bool z, float minRotation = 0f, float maxRotation = 360f)
    {
        return Quaternion.Euler(
            Convert.ToInt16(x) * UnityEngine.Random.Range(minRotation, maxRotation),
            Convert.ToInt16(y) * UnityEngine.Random.Range(minRotation, maxRotation),
            Convert.ToInt16(z) * UnityEngine.Random.Range(minRotation, maxRotation)
            );
    }

    private Vector3 GetRandomScale()
    {
        return new Vector3(
            UnityEngine.Random.Range(RandomMinScale, RandomMaxScale),
            UnityEngine.Random.Range(RandomMinScale, RandomMaxScale),
            UnityEngine.Random.Range(RandomMinScale, RandomMaxScale)
            );
    }

    private void Reset()
    {

        foreach (GameObject go in activePrefabs)
        {
            DestroyImmediate(go);
        }

    }
    




}
