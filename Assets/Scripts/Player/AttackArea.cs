using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] float meleeDamage;
    PlayerMovment playerMovmentScript;

    // Start is called before the first frame update
    void Start()
    {
        playerMovmentScript = GameObject.Find("Player").GetComponent<PlayerMovment>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Health>() != null)
        {
            Health healthScript = collision.GetComponent<Health>();
            healthScript.health -= meleeDamage;
        }
    }
}
