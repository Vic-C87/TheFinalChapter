using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tent : MonoBehaviour
{
    float hp;
    [SerializeField]
    GameObject mySpawnPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
