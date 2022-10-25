using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Tracker
{
    [SerializeField]
    bool myFoundLineOfSight = false;

    [SerializeField]
    GameObject myProjectile;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        CheckFollow(ref myFoundLineOfSight);

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

        if (hit.collider.CompareTag("Obstacles"))
        {
            myFoundLineOfSight = false;
        }
        else
        {
            myFoundLineOfSight = true;
        }
    }

    protected override void Attack()
    {
        if (CheckAttack())
        {
            GameObject projectile = Instantiate<GameObject>(myProjectile, transform.position, Quaternion.identity, GameManager.myInstance.transform);
            projectile.GetComponent<Projectile>().SetDirection((myPlayer.transform.position - transform.position).normalized, myDamage);
        }
    }
}
