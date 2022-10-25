using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
public class Tracker : Enemy
{
    protected Seeker myPathAI;
    [SerializeField]
    protected LayerMask myEnemyLayerMask;

    protected bool myIsAttacking = false;

    protected override void Start()
    {
        base.Start();
        myPathAI = GetComponent<Seeker>();
        myEnemyLayerMask = ~(1 << LayerMask.NameToLayer("Enemies"));
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected virtual void CheckFollow(ref bool aConditionToCheck)
    {
        if (aConditionToCheck)
        {
            myPathAI.StopFollow();
            GameManager.myInstance.DequeueMe(myPathAI);
            myIsAttacking = true;
        }
        else
        {
            //Debug.Log(myType);
            GameManager.myInstance.AddNewPathSeeker(myPathAI);
            myIsAttacking = false;
        }
    }
}
