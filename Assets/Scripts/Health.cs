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
            health = 20;
        }
        else if (CompareTag("Enemy"))
        {
            health = 15;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
