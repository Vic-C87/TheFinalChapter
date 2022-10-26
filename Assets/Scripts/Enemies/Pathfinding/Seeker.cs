using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    public Transform myTarget;
    [SerializeField]
    float mySpeed = 5f;
    float myOriginalSpeed;
    float mySlowDownTime = 3f;
    float myLastHitTime;
    [SerializeField]
    Vector3[] myPath;
    [SerializeField]
    int myTargetIndex;
    bool myIsSlowedDown = false;
    [SerializeField]
    bool myIsLurker = false;

    void Start()
    {
        myOriginalSpeed = mySpeed;
        myTarget = GameManager.myInstance.GetPlayer().transform;
        if(!myIsLurker)
        Seek();
    }

    private void Update()
    {
        if (myIsSlowedDown)
        {
            if (Time.realtimeSinceStartup - myLastHitTime >= mySlowDownTime)
            {
                SpeedUp();
            }
        }
    }

    public void Seek()
    {
        PathManager.RequestPath(transform.position, myTarget.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] aNewPath, bool aSuccessMessage)
    {
        if (aSuccessMessage)
        {
            myPath = aNewPath;
            myTargetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");

        }

    }

    public void StopFollow()
    {
        myTargetIndex = myPath.Length - 1;
        //StopCoroutine("FollowPath");
        
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = myPath[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                myTargetIndex++;
                if (myTargetIndex >= myPath.Length)
                {
                    yield break;
                }
                currentWaypoint = myPath[myTargetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, mySpeed * Time.deltaTime);
            yield return null;
        }
    }

    bool PlayerIsFar()
    {
        if (Vector3.Distance(transform.position, myTarget.position) < 2)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SlowDown(float aSlowDownAmount)
    {
        if (!myIsSlowedDown)
        {
            float newSpeed = mySpeed - aSlowDownAmount;
            mySpeed = Mathf.Clamp(newSpeed, 0f, myOriginalSpeed);
            myIsSlowedDown = true;
            myLastHitTime = Time.realtimeSinceStartup;
        }
        else
        {
            myLastHitTime = Time.realtimeSinceStartup;
        }
    }

    public void SpeedUp()
    {
        mySpeed = myOriginalSpeed;
        myIsSlowedDown = false;
    }

    private void OnDrawGizmos()
    {
        if (myPath != null)
        {
            for (int i = myTargetIndex; i < myPath.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(myPath[i], Vector3.one);

                if (i == myTargetIndex)
                {
                    Gizmos.DrawLine(transform.position, myPath[i]);
                }
                else
                {
                    Gizmos.DrawLine(myPath[i - 1], myPath[i]);
                }
            }
        }
    }
}
