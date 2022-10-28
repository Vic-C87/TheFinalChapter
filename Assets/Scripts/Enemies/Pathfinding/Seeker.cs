using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    public Transform myTarget;
    [SerializeField]
    float mySpeed = 5f;
    [SerializeField]
    Vector3[] myPath;
    [SerializeField]
    int myTargetIndex;
    [SerializeField]
    bool myIsLurker = false;

    void Start()
    {
        myTarget = GameManager.myInstance.GetPlayer().transform;
        OnSpawn();
    }

    public void OnSpawn()
    {
        if (!myIsLurker)
            Seek();
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
            if (this.gameObject.activeSelf)
            {
                StartCoroutine("FollowPath");
            }
        }
    }

    public void StopFollow()
    {
        myTargetIndex = myPath.Length - 1;
    }

    IEnumerator FollowPath()
    {
        if (myPath.Length == 0)
        {
            yield break;
        }
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
