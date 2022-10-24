using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinding : MonoBehaviour
{
    PathManager myPathManager;

    NodeGrid myGrid;

    private void Awake()
    {
        myGrid = GetComponent<NodeGrid>();
        myPathManager = GetComponent<PathManager>();
    }

    public void StartFindPath(Vector3 aStartingPosition, Vector3 aTargetPosition)
    {
        StartCoroutine(FindPath(aStartingPosition, aTargetPosition));
    }

    IEnumerator FindPath(Vector3 aStartingPosition, Vector3 aTargetPosition)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = myGrid.NodeFromWorldPoint(aStartingPosition);
        Node targetNode = myGrid.NodeFromWorldPoint(aTargetPosition);


        if (startNode.myWalkable && targetNode.myWalkable)
        {
            Heap<Node> openSet = new Heap<Node>(myGrid.myMaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    pathSuccess = true;

                    break;
                }

                foreach (Node neighbour in myGrid.GetNeighbours(currentNode))
                {
                    if (!neighbour.myWalkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.myGCost + GetDistance(currentNode, neighbour) + neighbour.myMovementPenalty;

                    if (newMovementCostToNeighbour < neighbour.myGCost || !openSet.Contains(neighbour))
                    {
                        neighbour.myGCost = newMovementCostToNeighbour;
                        neighbour.myHCost = GetDistance(neighbour, targetNode);
                        neighbour.myParent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                        else
                        {
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }

            }
        }
        yield return null;
        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
        }
        myPathManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    Vector3[] RetracePath(Node aStartNode, Node aTargetNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = aTargetNode;

        while (currentNode != aStartNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.myParent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);

        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> aPath)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < aPath.Count; i++)
        {
            Vector2 directionNew = new Vector2(aPath[i - 1].myGridXCoordinate - aPath[i].myGridXCoordinate, aPath[i - 1].myGridYCoordinate - aPath[i].myGridYCoordinate);
            if (directionNew != directionOld)
            {
                waypoints.Add(aPath[i].myWorldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    int GetDistance(Node aNodeA, Node aNodeB)
    {
        int distanceX = Mathf.Abs(aNodeA.myGridXCoordinate - aNodeB.myGridXCoordinate);
        int distanceY = Mathf.Abs(aNodeA.myGridYCoordinate - aNodeB.myGridYCoordinate);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }

        return 14 * distanceX + 10 * (distanceY - distanceX);

    }

}
