using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    List<GameObject> myEnemyPrefabs;

    [SerializeField]
    float mySpawnDelay;

    float mySpawnTimeStamp;

    bool myIsActivated = false;

    void Start()
    {
         GameManager.myInstance.myLevelManager.AddEnemyToList(myEnemyPrefabs.Count);
    }

    void FixedUpdate()
    {
        if (myIsActivated)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        if (mySpawnDelay < Time.realtimeSinceStartup - mySpawnTimeStamp && myEnemyPrefabs.Count > 0)
        {
            mySpawnTimeStamp = Time.realtimeSinceStartup;
            Instantiate<GameObject>(myEnemyPrefabs[0], transform.position, Quaternion.identity, this.transform);
            myEnemyPrefabs.RemoveAt(0);
        }
    }

    public void ActivateSpawner()
    {
        myIsActivated = true;
    }
}
