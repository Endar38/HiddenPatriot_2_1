using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomKontrol : MonoBehaviour

{
    public Camera mainCamera;
    public float zoomInSize = 5f; // Ukuran zoom in yang diinginkan
    public float zoomSpeed = 5f; // Kecepatan animasi zoom
    public float normalSize = 10f; // Ukuran normal kamera

    private bool isZoomed = false;

    void Update()
    {
        // Contoh kondisi untuk memulai animasi zoom in
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isZoomed)
                ZoomIn();
            else
                ZoomOut();
        }
    }

    public void ZoomIn()
    {
        //isZoomed = true;
        StartCoroutine(ZoomCamera(mainCamera.orthographicSize, zoomInSize));
    }

    public void ZoomOut()
    {
        //isZoomed = false;
        StartCoroutine(ZoomCamera(mainCamera.orthographicSize, normalSize));
    }

    IEnumerator ZoomCamera(float startSize, float targetSize)
    {
        if ( isZoomed)
        {
            yield break;
        }
        isZoomed = true;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * zoomSpeed;
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
            yield return null;
        }
        isZoomed = false;
    }
}

