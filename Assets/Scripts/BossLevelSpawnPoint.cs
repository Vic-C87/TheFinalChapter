using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelSpawnPoint : MonoBehaviour
{
    [SerializeField]
    List<GameObject> myEnemyPrefabs;

    [SerializeField]
    float mySpawnDelay;

    float mySpawnTimeStamp;

    int mySpawnCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Spawn();
    }

    void Spawn()
    {
        if (mySpawnDelay < Time.realtimeSinceStartup - mySpawnTimeStamp)
        {
            mySpawnTimeStamp = Time.realtimeSinceStartup;
            Instantiate<GameObject>(myEnemyPrefabs[mySpawnCounter], transform.position, Quaternion.identity, this.transform);
            mySpawnCounter++;
            if (mySpawnCounter >= myEnemyPrefabs.Count)
            {
                mySpawnCounter = 0;
            }
        }
    }
}
