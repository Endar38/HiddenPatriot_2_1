using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemySet1 : MonoBehaviour
{

    public LayerMask clickableLayer;
    public GridBehavior gridBehavior;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPatrol(GameObject pointPatrol, out bool menoleh, out bool stopSiagaPantau, GameObject tilePosEnm, List<GameObject> path, out bool takJalan, out bool enemyJalan, GameObject[,] gridArray)
    {
        menoleh = false;
        stopSiagaPantau = true;
        //GameObject hit = tilePosEnm;

        // Lakukan sesuatu dengan nilai dari skrip yang diinginkan
        int starEnemyX = tilePosEnm.GetComponent<GridStart>().x;
        int starEnemyY = tilePosEnm.GetComponent<GridStart>().y;


        int endEnemyX = pointPatrol.GetComponent<GridStart>().x;
        int endEnemyY = pointPatrol.GetComponent<GridStart>().y;
        // GridBehavior.rePos = true;
        //Debug.Log("Nilai dari skrip di objek yang diklik: " + targetY);

        gridBehavior.SetDistance(starEnemyX, starEnemyY, gridArray);
        gridBehavior.SetPath(endEnemyX, endEnemyY, path, gridArray);
        path.Reverse();
        takJalan = false;
        enemyJalan = true;



    }

    public void setMuka(Vector2 movementDir, out int arahMuka)
    {
        if (movementDir.x > 0)
        {
            // Objek bergerak ke kanan, arah muka ke kanan
            arahMuka = 2;
        }
        else if (movementDir.x < 0)
        {
            // Objek bergerak ke kiri, arah muka ke kiri
            arahMuka = 4;
        }
        else if (movementDir.y > 0)
        {
            // Objek bergerak ke atas, arah muka ke atas
            arahMuka = 1;
        }
        else if (movementDir.y < 0)
        {
            // Objek bergerak ke bawah, arah muka ke bawah
            arahMuka = 3;
        }
        else
        {
            //posDiam = true;
            arahMuka = -1;
        }
    }

    public void PointSet(out List<GameObject> titikP, List<GameObject> pointP, out List<List<GameObject>> pointJln2, GameObject[,] gridArray, string Area)
    {

        List<GameObject> list = new List<GameObject>();
        foreach(GameObject point in pointP)
        {
            RaycastHit2D hitPoint = Physics2D.Raycast(point.GetComponent<Transform>().position, Vector2.zero, Mathf.Infinity, clickableLayer);
            if (hitPoint.collider.gameObject.CompareTag("tile"))
            {
                if (hitPoint.collider.gameObject.GetComponent<GridStart>().area == Area)
                {
                    list.Add(hitPoint.collider.gameObject);
                }
                else
                {
                    hitPoint.collider.gameObject.SetActive(false);
                    RaycastHit2D hit = Physics2D.Raycast(point.GetComponent<Transform>().position, Vector2.zero, Mathf.Infinity, clickableLayer);
                    if (hit.collider.gameObject.GetComponent<GridStart>().area == Area)
                    {
                        list.Add(hitPoint.collider.gameObject);
                    }
                }
                hitPoint.collider.gameObject.SetActive(true);
                
            }
        }
        List<GameObject> point1 = new List<GameObject>();
        List<List<GameObject>> pointAll = new List<List<GameObject>>();

        for (int i = 0; i < list.Count - 1; i+= 2)
        {
            point1 = GeneratePoint2(list[i], list[i + 1], gridArray);
            pointAll.Add(point1);
        }
        titikP = list;
        pointJln2 = pointAll;
    }

    public List<GameObject> GeneratePoint2(GameObject pointPos, GameObject pointPos2, GameObject[,] gridArray)
    {
        int starEnemyX = pointPos.GetComponent<GridStart>().x;
        int starEnemyY = pointPos.GetComponent<GridStart>().y;


        int endEnemyX = pointPos2.GetComponent<GridStart>().x;
        int endEnemyY = pointPos2.GetComponent<GridStart>().y;
        // GridBehavior.rePos = true;
        //Debug.Log("Nilai dari skrip di objek yang diklik: " + targetY);
        List<GameObject> path = new List<GameObject>();
        gridBehavior.SetDistance(starEnemyX, starEnemyY, gridArray);
        gridBehavior.SetPath(endEnemyX, endEnemyY, path, gridArray);
        return path;
    }
}
