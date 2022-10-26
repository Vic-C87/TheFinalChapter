using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{
    PlayerMovment playerMovmentScript;
    BoxCollider2D attackArea;
    Transform myPlayer;

    public GameObject myProjectile;

    [SerializeField] float attackTime;
    [SerializeField] bool rangeAttacking;

    // Start is called before the first frame update
    void Start()
    {
        playerMovmentScript = GetComponent<PlayerMovment>();
        attackArea = GameObject.Find("AttackArea").GetComponent<BoxCollider2D>();
        myPlayer = GameManager.myInstance.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            attackArea.enabled = true;
            attackArea.offset = playerMovmentScript.Direction;
            StartCoroutine(attackCooldown());
        }

        if (Input.GetAxis("Fire2") == 1)
        {
            if (!rangeAttacking)
            {
                GameObject projectile = Instantiate<GameObject>(myProjectile, transform.position, Quaternion.identity, GameManager.myInstance.transform);
                projectile.GetComponent<ProjectileMovment>().SetDirection((playerMovmentScript.Direction).normalized);
                rangeAttacking = true;
                StartCoroutine(rangeAttack());
            }
        }

    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(attackTime);
        attackArea.enabled = false;
    }

    IEnumerator rangeAttack()
    {
        yield return new WaitForSeconds(attackTime);
        rangeAttacking = false;
    }
}
