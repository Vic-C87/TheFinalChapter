using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    BoxCollider2D attackArea;
    public GameObject projectile;
    public float attack;


    // Start is called before the first frame update
    void Start()
    {
        attackArea = GameObject.Find("Attack").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            attackArea.enabled = true;
            StartCoroutine(attackCooldown());
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Instantiate(projectile, transform.position, projectile.transform.rotation);
        }

    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(attack);
        attackArea.enabled = false;
    }
}
