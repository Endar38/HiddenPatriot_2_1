using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathFinding : MonoBehaviour
{

    public LayerMask obstacleLayer;

    private List<Vector2> currentPath;
    private GridNode[,] grid;

    private void Start()
    {
        // Inisialisasi grid saat memulai permainan
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        // Mendapatkan ukuran grid dari collider objek yang memiliki obstacleLayer
        Collider2D[] obstacles = Physics2D.OverlapAreaAll(Vector2.one * -1000f, Vector2.one * 1000f, obstacleLayer);

        // Tentukan ukuran grid berdasarkan collider yang ditemukan
        float gridSize = 1f;
        Vector2Int gridSizeInt = new Vector2Int(Mathf.RoundToInt(2000f / gridSize), Mathf.RoundToInt(2000f / gridSize));

        // Inisialisasi grid
        grid = new GridNode[gridSizeInt.x, gridSizeInt.y];

        // Isi grid dengan informasi rintangan
        for (int x = 0; x < gridSizeInt.x; x++)
        {
            for (int y = 0; y < gridSizeInt.y; y++)
            {
                Vector2 worldPoint = new Vector2(x * gridSize + gridSize / 2f, y * gridSize + gridSize / 2f);
                bool isObstacle = false;

                // Periksa apakah ada rintangan di posisi ini
                foreach (Collider2D obstacle in obstacles)
                {
                    if (obstacle.bounds.Contains(worldPoint))
                    {
                        isObstacle = true;
                        break;
                    }
                }

                grid[x, y] = new GridNode(worldPoint, isObstacle);
            }
        }
    }

    public List<Vector2> FindPath(Vector2 startPosition, Vector2 targetPosition)
    {
        GridNode startNode = NodeFromWorldPoint(startPosition);
        GridNode targetNode = NodeFromWorldPoint(targetPosition);

        List<GridNode> openSet = new List<GridNode>();
        HashSet<GridNode> closedSet = new HashSet<GridNode>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            GridNode currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentNode.FCost || (openSet[i].FCost == currentNode.FCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (GridNode neighbor in GetNeighbors(currentNode))
            {
                if (closedSet.Contains(neighbor) || neighbor.isObstacle)
                {
                    continue;
                }

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }

    private List<Vector2> RetracePath(GridNode startNode, GridNode endNode)
    {
        List<Vector2> path = new List<Vector2>();
        GridNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.worldPosition);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        return path;
    }

    private List<GridNode> GetNeighbors(GridNode node)
    {
        List<GridNode> neighbors = new List<GridNode>();

        int[] xOffsets = { -1, 0, 1, 0 };
        int[] yOffsets = { 0, 1, 0, -1 };

        for (int i = 0; i < 4; i++)
        {
            int x = node.gridX + xOffsets[i];
            int y = node.gridY + yOffsets[i];

            if (x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1))
            {
                neighbors.Add(grid[x, y]);
            }
        }

        return neighbors;
    }

    private int GetDistance(GridNode nodeA, GridNode nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return distX + distY;
    }

    private GridNode NodeFromWorldPoint(Vector2 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x / 1f);
        int y = Mathf.RoundToInt(worldPosition.y / 1f);
        return grid[x, y];
    }
}

public class GridNode
{
    public Vector2 worldPosition;
    public bool isObstacle;
    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;
    public GridNode parent;

    public int FCost { get { return gCost + hCost; } }

    public GridNode(Vector2 _worldPosition, bool _isObstacle)
    {
        worldPosition = _worldPosition;
        isObstacle = _isObstacle;
    }
}
