using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public RectTransform targetObject; // Objek UI target yang akan ditempelkan gambar
    public float snapDistance = 50f; // Jarak untuk menempelkan gambar ke objek target
    public float targetScaleMultiplier = 1.2f; // Faktor perbesaran skala objek target saat objek drag-and-drop berada dalam jarak tertentu
    public bool snapCompleted = false; // Variabel bool yang menjadi true saat gambar menempel ke objek target
    public bool isDraggable = true; // Apakah tombol dapat di-drag dan drop
    public RectTransform oriPos;
    public static bool pwBenar;

    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector3 originalTargetScale;
    private bool isHovering = false; // Apakah tombol berada dalam jangkauan target
    public Camera UICame;

    void Start()
    {
        pwBenar = false;
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        //oriPos = GetComponent<RectTransform>();

        originalTargetScale = targetObject.localScale;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isDraggable) return;

        rectTransform.SetAsLastSibling(); // Menempatkan tombol di atas semua tombol lain
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable) return;

        Vector3 screenPos = Input.mousePosition;

        // Mengonversi posisi layar ke posisi dunia
        Vector2 worldPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, screenPos, UICame, out worldPos);

        // Set posisi objek UI sesuai dengan posisi dunia yang telah dihitung
        rectTransform.localPosition = worldPos;

        // Cek apakah tombol berada dalam jarak snap dengan objek target
        isHovering = Vector2.Distance(rectTransform.position, targetObject.position) <= snapDistance;
        if (isHovering)
        {
            // Memperbesar skala objek target
            targetObject.localScale = originalTargetScale * targetScaleMultiplier;
        }
        else
        {
            // Mengembalikan skala objek target ke ukuran aslinya
            targetObject.localScale = originalTargetScale;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable) return;

        // Cek apakah tombol berada dalam jarak snap dengan objek target
        if (isHovering)
        {
            rectTransform.position = targetObject.position; // Tempelkan tombol ke objek target
            pwBenar = true;
            snapCompleted = true; // Set variabel bool menjadi true
            gameObject.SetActive(false);
        }
        else
        {
            rectTransform.position = oriPos.position;
            snapCompleted = false;
        }

        // Mengembalikan skala objek target ke ukuran aslinya
        targetObject.localScale = originalTargetScale;
    }
}


