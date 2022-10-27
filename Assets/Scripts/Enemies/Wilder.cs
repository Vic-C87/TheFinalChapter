using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wilder : Enemy
{
    bool myFoundLineOfSight = false;
    bool myHaveAction = false;
    bool myHaveWanderDirection = false;
    int myActionChoice;
    float myActionTimeStamp;
    float myActionTime;
    Vector2 myWanderDirection;

    [SerializeField]
    GameObject myProjectile;

    LayerMask myEnemyLayerMask;

    protected override void Start()
    {
        base.Start();
        myEnemyLayerMask = ~(1 << LayerMask.NameToLayer("Enemies"));
    }

    void FixedUpdate()
    {
        Action();
    }

    void Action()
    {
        if (!myHaveAction)
        {
            myActionChoice = GameManager.myInstance.GetRandomNumber(1, 2);
            myHaveAction = true;
            myActionTimeStamp = Time.realtimeSinceStartup;
        }

        switch (myActionChoice)
        {
            case 1:
                Wander();
                break;
            case 2:
                Attack();
                break;
            default:
                break;
        }
    }

    void CheckLineOffSight()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, (myPlayer.transform.position - transform.position).normalized, 20f, myEnemyLayerMask);

        Debug.DrawRay(transform.position, (myPlayer.transform.position - transform.position), Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Obstacles"))
            {
                myFoundLineOfSight = false;
            }
            else
            {
                myFoundLineOfSight = true;
            }
        }
    }

    protected override void Attack()
    {
        CheckLineOffSight();
        if (myFoundLineOfSight)
        {
            if (CheckAttack())
            {
                GameObject projectile = Instantiate<GameObject>(myProjectile, transform.position, Quaternion.identity, GameManager.myInstance.transform);
                projectile.GetComponent<Projectile>().SetDirection((myPlayer.transform.position - transform.position).normalized, myDamage);
            }
        }
        else
        {
            myHaveAction = false;
        }
    }

    void Wander()
    {
        if (!myHaveWanderDirection)
        {
            myWanderDirection = GameManager.myInstance.GetWanderDirection();
            myHaveWanderDirection = true;
        }
        if (myActionTime > Time.realtimeSinceStartup - myActionTimeStamp)
        {
            transform.Translate(myWanderDirection * Time.deltaTime * mySpeed);
        }
        else
        {
            myHaveWanderDirection = false;
            myHaveAction = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            myWanderDirection *= -1;
        }
    }
}
