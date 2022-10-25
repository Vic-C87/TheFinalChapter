using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wilder : Enemy
{
    [SerializeField]
    bool myFoundLineOfSight = false;
    [SerializeField]
    bool myHaveAction = false;
    [SerializeField]
    bool myHaveWanderDirection = false;
    [SerializeField]
    bool myHaveRunDirection = false;
    [SerializeField]
    int myActionChoice;
    [SerializeField]
    float myActionTimeStamp;

    [SerializeField]
    float myActionTime;
    [SerializeField]
    Vector2 myWanderDirection;
    [SerializeField]
    Vector2 myRunDirection;

    [SerializeField]
    GameObject myProjectile;

    protected LayerMask myEnemyLayerMask;

    [SerializeField]
    int myScreenXMinimum = -16;
    [SerializeField]
    int myScreenXMaximum = 16;
    [SerializeField]
    int myScreenYMinimum = -8;
    [SerializeField]
    int myScreenYMaximum = 8;

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
                //RunAway();
                myHaveAction = false;
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
            CheckBounds(ref myWanderDirection);
            transform.Translate(myWanderDirection * Time.deltaTime * mySpeed);
        }
        else
        {
            myHaveWanderDirection = false;
            myHaveAction = false;
        }

    }

    void CheckBounds(ref Vector2 aDirection)
    {
        if (transform.position.x > myScreenXMaximum)
        {
            aDirection *= -1;
        }
        if (transform.position.y > myScreenYMaximum)
        {
            aDirection *= -1;
        }
        if (transform.position.x < myScreenXMinimum)
        {
            aDirection *= -1;
        }
        if (transform.position.y < myScreenYMinimum)
        {
            aDirection *= -1;
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
