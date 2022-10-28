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
    [SerializeField]
    float myActionTime;
    Vector2 myWanderDirection;

    [SerializeField]
    List<GameObject> myProjectiles;

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
            myActionChoice = GameManager.myInstance.GetRandomNumber(1, 3);
            myHaveAction = true;
            myActionTimeStamp = Time.realtimeSinceStartup;
        }

        switch (myActionChoice)
        {
            case 1:
                Wander();
                break;
            case 2:
                Wander();
                break;
            case 3:
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
            else if (hit.collider.CompareTag("Player"))
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
                GameObject projectile = Instantiate<GameObject>(myProjectiles[GetRandomProjectileIndex()], transform.position, Quaternion.identity, GameManager.myInstance.transform);
                projectile.GetComponent<Projectile>().SetDirection((myPlayer.transform.position - transform.position).normalized, myDamage);
            }
        }
        else
        {
            myHaveAction = false;
        }
    }

    int GetRandomProjectileIndex()
    {

        return GameManager.myInstance.GetRandomNumber(0, myProjectiles.Count - 1);
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
