using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : IHeapItem<Node>
{
    public bool myWalkable;
    public Vector3 myWorldPosition;
    public int myGridXCoordinate;
    public int myGridYCoordinate;
    public int myMovementPenalty;

    public int myGCost;
    public int myHCost;

    public Node myParent;



    public Node(bool aWalkable, Vector3 aWorldPosition, int anXCoordinate, int aYCoordinate, int aPenalty)
    {
        myWalkable = aWalkable;
        myWorldPosition = aWorldPosition;
        myGridXCoordinate = anXCoordinate;
        myGridYCoordinate = aYCoordinate;
        myMovementPenalty = aPenalty;
    }
    public int myFCost
    {
        get
        {
            return myGCost + myHCost;
        }
    }

    public int myHeapIndex { get; set; }

    public int CompareTo(Node aNodeToCompare)
    {
        int compare = myFCost.CompareTo(aNodeToCompare.myFCost);
        if (compare == 0)
        {
            compare = myHCost.CompareTo(aNodeToCompare.myHCost);
        }
        return -compare;
    }
}
