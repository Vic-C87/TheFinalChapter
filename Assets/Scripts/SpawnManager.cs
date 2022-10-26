using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject myRangedEnemy;
    [SerializeField]
    int myRangedAmountToSpawn;
    [SerializeField]
    GameObject myMeleeEnemy;
    [SerializeField]
    int myMeleeAmountToSpawn;
    [SerializeField]
    GameObject myLurker;
    [SerializeField]
    int myLurkerAmountToSpawn;
    [SerializeField]
    GameObject myWilder;
    [SerializeField]
    int myWilderAmountToSpawn;

    [SerializeField]
    Transform[] mySpawnPoints;

    void Start()
    {
        mySpawnPoints = GetComponentsInChildren<Transform>();
        SpawnAll();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnAll()
    {
        Spawn(myRangedEnemy, mySpawnPoints[1].position, ref myRangedAmountToSpawn);
        
        Spawn(myMeleeEnemy, mySpawnPoints[2].position, ref myMeleeAmountToSpawn);

        Spawn(myLurker, mySpawnPoints[3].position, ref myLurkerAmountToSpawn);

        Spawn(myWilder, mySpawnPoints[4].position, ref myWilderAmountToSpawn);
    }

    void Spawn(GameObject anEnemyPrefabToSpawn, Vector3 aPositionToSpawnAt, ref int anAmountLeft)
    {
        if (anAmountLeft > 0)
        {
            Instantiate<GameObject>(anEnemyPrefabToSpawn, aPositionToSpawnAt, Quaternion.identity, this.transform);
            anAmountLeft--;
        }
    }
}
