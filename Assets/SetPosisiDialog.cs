using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosisiDialog : MonoBehaviour
{
    public Transform targetObject; // Objek yang akan diikuti posisinya
    public RectTransform uiObject; // Objek UI yang akan disesuaikan posisinya
    public Canvas canvas; // Canvas yang digunakan
    public Camera uiCamera; // Kamera yang digunakan untuk canvas

    public float offsetX = 5f; // Offset pada sumbu X
    public float offsetY = 3f; // Offset pada sumbu Y

    void LateUpdate()
    {
        // Mengonversi posisi objek dunia menjadi posisi layar
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetObject.position);

        // Menambah offset pada sumbu X dan Y
        screenPos.x += offsetX;
        screenPos.y += offsetY;

        // Mengonversi posisi layar menjadi posisi lokal dalam canvas
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, screenPos, uiCamera, out localPoint);

        // Set posisi objek UI sesuai dengan posisi lokal yang telah dihitung
        uiObject.localPosition = localPoint;
    }
}

