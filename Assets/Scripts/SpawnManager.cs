using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    List<SpawnPoint> myZoneOneSpawnPoints;

    [SerializeField]
    List<SpawnPoint> myZoneTwoSpawnPoints;

    [SerializeField]
    List<SpawnPoint> myZoneThreeSpawnPoints;


    public void ActivateZoneOne()
    {
        foreach (SpawnPoint point in myZoneOneSpawnPoints)
        {
            point.ActivateSpawner();
        }
    }

    public void ActivateZoneTwo()
    {
        foreach (SpawnPoint point in myZoneTwoSpawnPoints)
        {
            point.ActivateSpawner();
        }
    }

    public void ActivateZoneThree()
    {
        foreach (SpawnPoint point in myZoneThreeSpawnPoints)
        {
            point.ActivateSpawner();
        }
    }
}
