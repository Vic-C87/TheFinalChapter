using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Enemy
{
    Seeker myPathAI;

    [SerializeField]
    bool myFoundLineOfSight = false;

    bool myIsAttacking = false;

    [SerializeField]
    LayerMask myEnemyLayerMask;
    [SerializeField]
    GameObject myProjectile;

    protected override void Start()
    {
        base.Start();
        myPathAI = GetComponent<Seeker>();
        myEnemyLayerMask = ~(1 << LayerMask.NameToLayer("Enemies"));
    }

    void Update()
    {
        if (myFoundLineOfSight)
        {
            myPathAI.StopFollow();
            myIsAttacking = true;
            Debug.Log("Stopped!");
            //Shoot player
        }
        else
        {
            myPathAI.Seek();
            myIsAttacking = false;

        }

        if (myIsAttacking)
        {
            Attack();
        }
        CheckLineOffSight();
    }

    void CheckLineOffSight()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (myPlayer.position - transform.position).normalized, 20f, myEnemyLayerMask);

        Debug.DrawRay(transform.position, (myPlayer.position - transform.position), Color.red);

        if (hit.collider.CompareTag("Player"))
        {
            
            myFoundLineOfSight = true;
        }
        else
        {
            myFoundLineOfSight = false;

        }

    }

    protected override void Attack()
    {
        if (CheckAttack())
        {
            GameObject projectile = Instantiate<GameObject>(myProjectile, transform.position, Quaternion.identity, GameManager.myInstance.transform);
            projectile.GetComponent<Projectile>().SetDirection((myPlayer.transform.position - transform.position).normalized);
        }
    }


}
