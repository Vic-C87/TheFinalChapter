using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        if (CompareTag("Player"))
        {
            health = 10;
        }
        else
        {
            health = 20;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
