using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tent : MonoBehaviour
{
    float hp;
    [SerializeField]
    GameObject mySpawnPoint;

    public void TakeDamage(float amount)
    {
        hp -= amount;

        if (hp <= 0)
        {
            mySpawnPoint.SetActive(false);
            gameObject.SetActive(false);
        }
    }

}
