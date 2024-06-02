using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    public float moveSpeed = 5f;
    public LayerMask clickableLayer;

    private Vector2 currentTarget;
    private List<Vector2> currentPath;
    private int currentWaypointIndex;
    private bool isMoving = false;

    void Update()
    {
        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, clickableLayer);
            if (hit.collider != null)
            {
                Vector2 target = hit.collider.transform.position;
                target = new Vector2(Mathf.Round(target.x), Mathf.Round(target.y)); // Membulatkan posisi target ke integer
                MoveTo(target);
            }
        }

        if (isMoving)
        {
            MoveToTarget();
        }
    }

    void MoveTo(Vector2 target)
    {
        currentTarget = target;
        currentPath = FindPath(transform.position, target);
        currentWaypointIndex = 0;
        isMoving = true;
    }

    void MoveToTarget()
    {
        if (currentPath == null || currentPath.Count == 0)
        {
            isMoving = false;
            return;
        }

        Vector2 targetPosition = currentPath[currentWaypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if ((Vector2)transform.position == targetPosition)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= currentPath.Count)
            {
                isMoving = false;
            }
        }
    }

    List<Vector2> FindPath(Vector2 start, Vector2 target)
    {
        List<Vector2> path = new List<Vector2>();

        // Jarak horizontal dan vertikal antara start dan target
        float dx = Mathf.Abs(target.x - start.x);
        float dy = Mathf.Abs(target.y - start.y);
        float posx = target.x - start.x;
        float posy = target.y - start.y;
        // Jika target bergerak secara horizontal, tambahkan waypoint horizontal ke target
        if (dx > dy)
        {
            Debug.Log(posx);
            for (int x = Mathf.RoundToInt(start.x); x != Mathf.RoundToInt(target.x); x += (int)Mathf.Sign(target.x - start.x))
            {
                if(posx > 0)
                {
                    path.Add(new Vector2(x + 1, start.y));
                }
                else
                {
                    path.Add(new Vector2(x -1 , start.y));
                }
                
            }
        }
        // Jika target bergerak secara vertikal, tambahkan waypoint vertikal ke target
        else
        {
            for (int y = Mathf.RoundToInt(start.y); y != Mathf.RoundToInt(target.y); y += (int)Mathf.Sign(target.y - start.y))
            {
                if(posy > 0)
                {
                    path.Add(new Vector2(start.x, y + 1));
                }
                else
                {
                    path.Add(new Vector2(start.x, y - 1));
                }
                
            }
        }

        // Tambahkan posisi target sebagai waypoint terakhir
        path.Add(target);

        return path;
    }
}

