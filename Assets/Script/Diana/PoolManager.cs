using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject BulletPrefab;
    public int PoolSize = 10;
    public Transform BulletsRoot;
    public Queue<GameObject> BulletQueue = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject go;

        for (int i = 0; i < PoolSize; i++)
        {
            go = Instantiate(BulletPrefab, Vector3.zero, Quaternion.identity, BulletsRoot);
            BulletQueue.Enqueue(go);
            go.SetActive(false);
        }
    }

    public void SpawnObj(Vector3 pos, Quaternion rot)
    {
        if (BulletQueue.Peek().activeSelf)  
            return;

        GameObject SpawnedObj = BulletQueue.Dequeue();
        SpawnedObj.transform.position = pos;
        SpawnedObj.transform.rotation = rot;
        SpawnedObj.SetActive(true);
        BulletQueue.Enqueue(SpawnedObj);
    }
}
