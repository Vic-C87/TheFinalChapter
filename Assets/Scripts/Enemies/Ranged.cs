using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Enemy
{
    Seeker myPathAI;

    [SerializeField]
    bool myFoundLineOfSight = false;

    bool myIsAttacking = false;

    protected override void Start()
    {
        base.Start();
        myPathAI = GetComponent<Seeker>();
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
        CheckLineOffSight();
    }

    void CheckLineOffSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (myPlayer.position - transform.position).normalized);

        Debug.DrawRay(transform.position, (myPlayer.position - transform.position).normalized, Color.red);

        if (hit.collider.CompareTag("Player"))
        {
            
            myFoundLineOfSight = true;
        }
        else
        {
            myFoundLineOfSight = false;

        }

    }


}
