using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolusiManager : MonoBehaviour
{
    void Start()
    {
        AdjustResolution();
    }

    void AdjustResolution()
    {
        // Dapatkan resolusi layar perangkat
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        // Cetak resolusi layar ke console untuk debugging
        Debug.Log("Screen Width: " + screenWidth + " Screen Height: " + screenHeight);

        // Sesuaikan resolusi game sesuai layar perangkat
        Screen.SetResolution(screenWidth, screenHeight, true);
    }
}
