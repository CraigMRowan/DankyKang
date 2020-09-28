using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    public GameObject barrel;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;
    public float barrelForce;

    void Start()
    {
        InvokeRepeating("SpawnBarrel", spawnTime, spawnDelay);
    }

    void SpawnBarrel()
    {
        GameObject cloneBarrel = (GameObject)Instantiate(barrel, transform.position, transform.rotation);
        Rigidbody2D rigidbody2D = cloneBarrel.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = Vector3.right * barrelForce;

        if (stopSpawning)
            CancelInvoke("SpawnBarrel");
    }
}
