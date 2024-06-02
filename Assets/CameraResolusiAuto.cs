using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolusiAuto : MonoBehaviour
{
    void Start()
    {
        AdjustCamera();
    }

    void AdjustCamera()
    {
        Camera camera = Camera.main;

        if (camera != null)
        {
            float targetAspect = 1920f / 1080f; // Aspek rasio referensi dinamis
            float windowAspect = (float)Screen.width / (float)Screen.height;
            float scaleHeight = windowAspect / targetAspect;
            Debug.Log(targetAspect);
            Debug.Log(windowAspect);
            if (scaleHeight < 1.0f)
            {
                Rect rect = camera.rect;

                rect.width = 1.0f;
                rect.height = scaleHeight;
                rect.x = 0;
                rect.y = (1.0f - scaleHeight) / 2.0f;
                //camera.orthographicSize = camera.orthographicSize / scaleHeight;

                camera.rect = rect;
            }
            else
            {
                
                float scaleWidth = 1.0f / scaleHeight;

                Rect rect = camera.rect;

                rect.width = scaleWidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scaleWidth) / 2.0f;
                rect.y = 0;

                camera.rect = rect;
                
            }
        }
    }
}