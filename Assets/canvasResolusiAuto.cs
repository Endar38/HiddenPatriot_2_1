using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasResolusiAuto : MonoBehaviour
{
    void Start()
    {
        CanvasScaler canvasScaler = GetComponent<CanvasScaler>();

        if (canvasScaler != null)
        {
            // Ambil resolusi layar perangkat
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;

            // Tentukan resolusi referensi
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(screenWidth, screenHeight);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f; // Sesuaikan nilai ini berdasarkan kebutuhan desain Anda
        }
    }
}

