using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathManager : MonoBehaviour
{
    Queue<PathRequest> myPathRequestQueue = new Queue<PathRequest>();
    PathRequest myCurrentPathRequest;

    public static PathManager myInstance;

    Pathfinding myPathfinding;
    bool myIsProcessingPath;

    void Awake()
    {
        myInstance = this;
        myPathfinding = GetComponent<Pathfinding>();
    }

    public static void RequestPath(Vector3 aPathStart, Vector3 aPathEnd, Action<Vector3[], bool> aCallback)
    {
        PathRequest newRequest = new PathRequest(aPathStart, aPathEnd, aCallback);
        myInstance.myPathRequestQueue.Enqueue(newRequest);
        myInstance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if (!myIsProcessingPath && myPathRequestQueue.Count > 0)
        {
            GameManager.myInstance.myIsFindingPath = false;
            myCurrentPathRequest = myPathRequestQueue.Dequeue();
            myIsProcessingPath = true;
            myPathfinding.StartFindPath(myCurrentPathRequest.myPathStart, myCurrentPathRequest.myPathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] aPath, bool aSuccessMessage)
    {
        myCurrentPathRequest.myCallback(aPath, aSuccessMessage);
        myIsProcessingPath = false;
        GameManager.myInstance.myIsFindingPath = false;
        TryProcessNext();
    }

    struct PathRequest
    {
        public Vector3 myPathStart;
        public Vector3 myPathEnd;
        public Action<Vector3[], bool> myCallback;

        public PathRequest(Vector3 aStart, Vector3 anEnd, Action<Vector3[], bool> aCallback)
        {
            myPathStart = aStart;
            myPathEnd = anEnd;
            myCallback = aCallback;
        }
    }
}
