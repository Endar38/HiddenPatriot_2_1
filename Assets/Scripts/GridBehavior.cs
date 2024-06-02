using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehavior : MonoBehaviour
{
    public bool findDistance = false;
    public int rows1 = 17;
    public int columns1 = 20;
    public int rows2 = 17;
    public int columns2 = 20;
    public int rows3 = 17;
    public int columns3 = 20;
    public int rows4 = 17;
    public int columns4 = 20;
    public int scale = 1;

    public GameObject gridPrefabs;
    public Vector2 leftButtomLocation1 = new Vector2 (-10, -10);
    public Vector2 leftButtomLocation2 = new Vector2 (-10, -10);
    public Vector2 leftButtomLocation3 = new Vector2 (-10, -10);
    public Vector2 leftButtomLocation4 = new Vector2 (-10, -10);

    public GameObject[,] gridArray1;
    public GameObject[,] gridArray2;
    public GameObject[,] gridArray3;
    public GameObject[,] gridArray4;

    public int startX = -5;
    public int startY = -5;
    public int endX = 2;
    public int endY = 2;

    public static List<GameObject> path = new List<GameObject>();

    //public static bool siapJalan;

    // Start is called before the first frame update\

    public static bool rePos;

    public Color selectedColor = Color.green;
    public float transparencyValue = 1;

    public static bool deteksiOtomatis;

    public Jalan playerJln;

    public List <GameObject> emn ;

    void Awake()
    {
        

        if (gridPrefabs)
        {
            GenerateGrid(out gridArray1, leftButtomLocation1, "Area1", columns1, rows1, -1);
            GenerateGrid(out gridArray2, leftButtomLocation2, "Area2", columns2, rows2, 0);
            GenerateGrid(out gridArray3, leftButtomLocation3, "Area3", columns3, rows3, 1);
            GenerateGrid(out gridArray4, leftButtomLocation4, "Area4", columns4, rows4, 2);
        }
            
        else
            print("mising grid");

    }
    void Start()
    {
        rePos = false;
    }
    // Update is called once per frame
    void Update()
    {
       
        if (rePos)
        {
            startX = playerGridMove.posisiX;
            startY = playerGridMove.posisiY;
            rePos = false;
        }
        if (deteksiOtomatis)
        {

            
        }
        if (Jalan.bisaDeteksiJalur == true)
        {
           
        }
        
        
    }

    void GenerateGrid(out GameObject[,] gridArray, Vector2 lefBottLoc, string Area, int columns, int rows, int layer)
    {
        gridArray = new GameObject[columns, rows];
        for (int i = 0; i < columns; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                GameObject obj = Instantiate(gridPrefabs, new Vector2 (lefBottLoc.x + scale*i
                    ,lefBottLoc.y + scale *j), Quaternion.identity);

                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<GridStart>().x = i;
                obj.GetComponent<GridStart>().y = j;
                obj.GetComponent<GridStart>().area = Area;
                obj.GetComponent<SpriteRenderer>().sortingOrder = layer;

                gridArray[i,j] = obj;
            }
        }
    }
    public void SetDistance(int x, int y, GameObject[,] gridArray)
    {
        InitialSetUp(x, y, gridArray);
        int[] testArray = new int[gridArray.GetLength(1)*gridArray.GetLength(0)];

        for(int step = 1; step < testArray.Length; step++)
        {
            foreach (GameObject obj in gridArray)
            {
                if (obj && obj.GetComponent<GridStart>().visited == step - 1)
                    TestFourDirections(obj.GetComponent<GridStart>().x, obj.GetComponent<GridStart>().y, step, gridArray);
            }
        }

    }
    void InitialSetUp(int x, int y, GameObject[,] gridArray)
    {
        {
            foreach (GameObject obj in gridArray)
            {
                try
                {
                    obj.GetComponent<GridStart>().visited = -1;

                }
                catch { }
            }

            gridArray[x, y].GetComponent<GridStart>().visited = 0;
        }
    }


    public void SetPath(int x, int y, List<GameObject>pathPlayer, GameObject[,] gridArray)
    {
        int step;
        List<GameObject> tempList = new List<GameObject>();
        pathPlayer.Clear();
        if (gridArray[x, y] || gridArray[x, y].GetComponent<GridStart>().visited > 0)
        {
            pathPlayer.Add(gridArray[x,y]);
            step = gridArray[x, y].GetComponent<GridStart>().visited - 1;
        }
        else
        {
            print(" can't reach");
            return;
        }
        for (int i = step; step > -1; step--)
        {
            if (TestDirection(x, y, step, 1, gridArray))
                tempList.Add(gridArray[x, y + 1]);
            if (TestDirection(x, y, step, 2, gridArray))
                tempList.Add(gridArray[x+1, y]);
            if (TestDirection(x, y, step, 3, gridArray))
                tempList.Add(gridArray[x, y - 1]);
            if (TestDirection(x, y, step, 4, gridArray))
                tempList.Add(gridArray[x-1, y]);

            GameObject tempObj = FindClosest(gridArray[x, y].transform, tempList, gridArray);
            pathPlayer.Add(tempObj);
            x = tempObj.GetComponent<GridStart>().x;
            y = tempObj.GetComponent<GridStart>().y;
            tempList.Clear();


        }
        

    }

    bool TestDirection(int x, int y, int step, int direction, GameObject[,] gridArray)
    {
        //1up, 2right, 3down, 4left
        switch(direction)
        {
            case 4:
                if (x - 1 > -1 && gridArray[x - 1, y] && gridArray[x - 1, y].GetComponent<GridStart>().visited == step)
                    return true;
                else
                    return false;

            case 3:
                if (y - 1 > -1 && gridArray[x, y - 1] && gridArray[x, y - 1].GetComponent<GridStart>().visited == step)
                    return true;
                else
                    return false;
            case 2:
                if (x + 1 < gridArray.GetLength(0) && gridArray[x + 1, y] && gridArray[x + 1, y].GetComponent<GridStart>().visited == step)
                    return true;
                else
                    return false;
            case 1:
                if (y + 1 < gridArray.GetLength(1) && gridArray[x, y + 1] && gridArray[x, y + 1].GetComponent<GridStart>().visited == step)
                    return true;
                else
                    return false;
        }
        return false;
    }

    /*void TestFourDirections(int x, int y, int step)
    {
        if (TestDirection(x, y, -1, 1))
            SetVisited(x,y + 1, step);
        if (TestDirection(x, y, -1, 2))
            SetVisited(x + 1, y, step);
        if (TestDirection(x, y, -1, 3))
            SetVisited(x, y - 1, step);
        if (TestDirection(x,y, -1, 4))
            SetVisited(x - 1, y, step);
    }*/
    void TestFourDirections(int x, int y, int step, GameObject[,] gridArray)
    {
        if (TestDirection(x, y, -1, 1, gridArray) && !IsWaypoint(gridArray[x, y + 1]))
            SetVisited(x, y + 1, step, gridArray);

        if (TestDirection(x, y, -1, 2, gridArray) && !IsWaypoint(gridArray[x + 1, y]))
            SetVisited(x + 1, y, step, gridArray);

        if (TestDirection(x, y, -1, 3, gridArray) && !IsWaypoint(gridArray[x, y - 1]))
            SetVisited(x, y - 1, step, gridArray);

        if (TestDirection(x, y, -1, 4, gridArray) && !IsWaypoint(gridArray[x - 1, y]))
            SetVisited(x - 1, y, step, gridArray);
    }

    bool IsWaypoint(GameObject tile)
    {
        // Check if the tile is in the other list
        foreach (GameObject obj in Jalan.waypointTiles)
        {
            if (obj == tile)
                return true;
        }
        return false;
    }

    void SetVisited(int x, int y, int step, GameObject[,] gridArray)
    {
        if (gridArray[x, y])
            gridArray[x, y].GetComponent<GridStart>().visited = step;
    }
    GameObject FindClosest(Transform targetLocation, List<GameObject> list, GameObject[,] gridArray)
    {
        float currentDistance = scale * gridArray.GetLength(1) * gridArray.GetLength(0);
        int indexNumber = 0;
        for(int i = 0; i<list.Count; i++)
        {
            if (Vector3.Distance(targetLocation.position, list[i].transform.position) < currentDistance)
            {
                currentDistance = Vector3.Distance(targetLocation.position, list[i].transform.position);
                indexNumber = i;
            }
        }
        return list[indexNumber];
    }
}
