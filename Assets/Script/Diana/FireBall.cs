using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float Speed;
    public float DestroyAfter;

    private void OnEnable()
    {
        Invoke("DestroyThis", DestroyAfter);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, Speed * Time.deltaTime);
    }

    void DestroyThis()
    {
        gameObject.SetActive(false);
    }
}
