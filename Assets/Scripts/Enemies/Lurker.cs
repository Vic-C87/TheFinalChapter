using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
public class Lurker : Enemy
{
    protected Seeker myPathAI;
    [SerializeField]
    protected LayerMask myEnemyLayerMask;

    protected bool myIsAttacking = false;
    [SerializeField]
    protected bool myShouldFollow = false;

    protected bool myPlayerIsClose = false;

    protected override void Start()
    {
        base.Start();
        myPathAI = GetComponent<Seeker>();
        myEnemyLayerMask = ~(1 << LayerMask.NameToLayer("Enemies"));
        
    }

    protected override void Update()
    {
        base.Update();
        if (myShouldFollow && !myPlayerIsClose)
        {
            //GameManager.myInstance.AddNewPathSeeker(myPathAI);
            CheckFollow(ref myPlayerIsClose);
        }

        if (myIsAttacking)
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        base.Attack();
        if(CheckAttack())
        {
            if (myCircleCollider.IsTouching(myPlayer.GetComponent<CircleCollider2D>()))
            {
                //Hurt Player
            }
        }
    }

    protected virtual void CheckFollow(ref bool aConditionToCheck)
    {
        if (aConditionToCheck)
        {
            //myPathAI.StopFollow();
            myIsAttacking = true;
        }
        else
        {
            GameManager.myInstance.AddNewPathSeeker(myPathAI);
            myIsAttacking = false;
        }
    }

    public virtual void SetFollow(bool aToogle)
    {
        myShouldFollow = aToogle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            myPathAI.StopFollow();
            GameManager.myInstance.DequeueMe(myPathAI);
            myPlayerIsClose = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            myPlayerIsClose = false;
        }
    }
}
