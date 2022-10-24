using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    public bool myDisplayGridGizmos;
    public float myNodeRadius;
    public TerrainType[] myWalkableRegions;
    public Vector2 myGridWorldSize;
    public LayerMask myUnwalkableMask;
    LayerMask myWalkableMask;
    Dictionary<int, int> myRegions = new Dictionary<int, int>();

    float myNodeDiameter;
    int myGridSizeX, myGridSizeY;

    Node[,] myGrid;

    public int myObstacleProximityPenalty = 10;
    int myPenaltyMin = int.MaxValue;
    int myPenaltyMax = int.MinValue;

    void Awake()
    {
        myNodeDiameter = myNodeRadius * 2;
        myGridSizeX = Mathf.RoundToInt(myGridWorldSize.x / myNodeDiameter);
        myGridSizeY = Mathf.RoundToInt(myGridWorldSize.y / myNodeDiameter);

        foreach (TerrainType region in myWalkableRegions)
        {
            myWalkableMask.value |= region.myTerrainMask.value;
            myRegions.Add((int)Mathf.Log(region.myTerrainMask.value, 2), region.myTerrainPenalty);
        }

        CreateGrid();
    }

    public int myMaxSize
    {
        get
        {
            return myGridSizeX * myGridSizeY;
        }
    }

    void CreateGrid()
    {
        myGrid = new Node[myGridSizeX, myGridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * myGridWorldSize.x / 2 - Vector3.up * myGridWorldSize.y / 2;

        for (int x = 0; x < myGridSizeX; x++)
        {
            for (int y = 0; y < myGridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + (Vector3.right * (x * myNodeDiameter + myNodeRadius) + Vector3.up * (y * myNodeDiameter + myNodeRadius));
                bool walkable = !Physics2D.CircleCast(worldPoint, myNodeRadius, worldPoint, 0f, myUnwalkableMask);

                int movementPenalty = 0;

                RaycastHit2D[] hits;

                hits = Physics2D.CircleCastAll(worldPoint, myNodeRadius, worldPoint, 1f, myWalkableMask);
                if (hits.Length == 1)
                {
                    movementPenalty = myRegions[hits[0].collider.gameObject.layer];
                }
                else if (hits.Length > 1)
                {
                    for (int i = 0; i < hits.Length; i++)
                    {
                        movementPenalty += myRegions[hits[i].collider.gameObject.layer];
                    }

                }
                myGrid[x, y] = new Node(walkable, worldPoint, x, y, movementPenalty);
            }
        }
    }

    public List<Node> GetNeighbours(Node aNode)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = aNode.myGridXCoordinate + x;
                int checkY = aNode.myGridYCoordinate + y;

                if (checkX >= 0 && checkX < myGridSizeX && checkY >= 0 && checkY < myGridSizeY)
                {
                    neighbours.Add(myGrid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 aWorldPosition)
    {
        float percentX = (aWorldPosition.x + myGridWorldSize.x / 2) / myGridWorldSize.x;
        float percentY = (aWorldPosition.y + myGridWorldSize.y / 2) / myGridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((myGridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((myGridSizeY - 1) * percentY);

        return myGrid[x, y];
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(myGridWorldSize.x, myGridWorldSize.y, .1f));
        if (myGrid != null && myDisplayGridGizmos)
        {
            foreach (Node n in myGrid)
            {
                Gizmos.color = Color.Lerp(Color.white, Color.black, Mathf.InverseLerp(myPenaltyMin, myPenaltyMax, n.myMovementPenalty));
                Gizmos.color = (n.myWalkable) ? Gizmos.color : Color.red;
                Gizmos.DrawCube(n.myWorldPosition, Vector3.one * (myNodeDiameter - .1f));
            }
        }
    }

    [System.Serializable]
    public class TerrainType
    {
        public LayerMask myTerrainMask;
        public int myTerrainPenalty;
    }
}
