using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaySmokeManager : MonoBehaviour
{

    //public GameObject[,] tilesArray; // Array game objek tile
    //public GameObject granat; // GameObject granat

    // Fungsi untuk mencari tile terdekat yang tidak terkena granat
    public GameObject CariAreaLuarSmoke(Vector2 enemyPosition, GameObject[,] tilesArray)
    {
        float shortestDistance = Mathf.Infinity;
        GameObject closestTile = null;

        // Loop untuk mencari tile tujuan
        for (int i = 0; i < tilesArray.GetLength(0); i++)
        {
            for (int j = 0; j < tilesArray.GetLength(1); j++)
            {
                GameObject targetTile = tilesArray[i, j];

                if (targetTile != null && !targetTile.GetComponent<GridStart>().terkenaGranat)
                {
                    // Periksa apakah jarak ke tile ini lebih pendek dari yang sebelumnya
                    float distanceToEnemy = Vector2.Distance(enemyPosition, targetTile.transform.position);
                    if (distanceToEnemy < shortestDistance)
                    {
                        shortestDistance = distanceToEnemy;
                        closestTile = targetTile;
                    }
                }
            }
        }

        return closestTile;
    }
}
