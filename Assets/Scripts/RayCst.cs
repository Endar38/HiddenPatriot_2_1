using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCst : MonoBehaviour
{

    public float maxDistance = 5f; // Panjang maksimal raycast
    public LayerMask layerMask; // Layer mask untuk menentukan objek yang akan terkena raycast
    public LineRenderer lineRenderer; // Referensi ke Line Renderer untuk menampilkan raycast

    void Update()
    {
        // Hitung sudut untuk setiap titik dalam kerucut
        float coneAngle = 45f; // Sudut kerucut
        int rays = 50; // Jumlah titik dalam kerucut
        float stepAngle = coneAngle / rays;

        // Atur sudut awal berdasarkan setengah dari sudut kerucut
        float startAngle = transform.eulerAngles.y - coneAngle / 2;

        // Tentukan arah raycast awal ke kiri
        Vector2 direction = Vector2.left;

        // Ubah arah raycast agar mengikuti rotasi pemain setelahnya
        direction = Quaternion.Euler(0, 0, transform.eulerAngles.z) * direction;

        // Hapus semua titik dari Line Renderer
        lineRenderer.positionCount = 0;

        for (int i = 0; i < rays; i++)
        {
            // Hitung sudut raycast berdasarkan sudut kerucut dan iterasi saat ini
            float angle = startAngle + stepAngle * i;

            // Hitung arah raycast berdasarkan sudut
            Vector2 rayDirection = Quaternion.Euler(0, 0, angle) * direction;

            // Lakukan raycast
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, maxDistance, layerMask);

            // Tentukan panjang raycast sesuai dengan hasil raycast
            float length = maxDistance;
            if (hit.collider != null)
            {
                length = hit.distance;
            }

            // Tambahkan titik akhir raycast ke Line Renderer
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position + (Vector3)rayDirection * length);
        }
    }
}
