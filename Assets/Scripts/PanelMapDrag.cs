using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelMapDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    private Vector3 dragOrigin;
    private bool isDragging = false;

    public KameraKontrol cameraFollowPlayer; // Referensi ke skrip CameraFollowPlayer
    public static bool bolehDragMap;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsPointerOverPanel() && bolehDragMap)
        {
            isDragging = true;
            dragOrigin = Input.mousePosition;

            // Set kamera untuk tidak mengikuti pemain saat digeser
            cameraFollowPlayer.SetFollowPlayer(false);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        // Set kamera untuk mengikuti pemain setelah digeser selesai
        cameraFollowPlayer.SetFollowPlayer(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector3 currentPosition = Input.mousePosition;
            Vector3 difference = Camera.main.ScreenToWorldPoint(dragOrigin) - Camera.main.ScreenToWorldPoint(currentPosition);
            Camera.main.transform.position += difference;
            dragOrigin = currentPosition;
        }
    }

    private bool IsPointerOverPanel()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        foreach (var result in results)
        {
            if (result.gameObject.CompareTag("PanelKam"))
            {
                return true;
            }
        }
        return false;
    }
}
