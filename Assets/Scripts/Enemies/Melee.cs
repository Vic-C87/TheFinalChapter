using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Tracker
{
    protected bool myPlayerIsClose = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        CheckFollow(ref myPlayerIsClose);

        if (myIsAttacking)
        {
            Attack();
        }
        //CheckPlayerDistance();
    }

    protected override void Attack()
    {
        base.Attack();
        if (CheckAttack())
        {
            if (myCapsulecollider.IsTouching((Collider2D)myPlayer.GetPlayerCollider()))
            {
                Debug.Log("Player hit");
                myPlayer.TakeDamage(myDamage);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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
