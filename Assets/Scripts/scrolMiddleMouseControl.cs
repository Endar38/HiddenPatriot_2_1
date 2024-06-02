using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class scrolMiddleMouseControl : MonoBehaviour
{

    public Scrollbar scrollbar;
    public float sensitivity = 0.1f; // Atur sensitivitas scroll di sini

    public void OnScroll()
    {
        // Mendapatkan input dari mouse scroll wheel
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        // Menghitung perubahan posisi scrollbar berdasarkan sensitivitas
        float newScrollbarValue = scrollbar.value + scrollDelta * sensitivity;

        // Menghindari nilai scrollbar keluar dari rentang [0, 1]
        newScrollbarValue = Mathf.Clamp01(newScrollbarValue);

        // Mengatur posisi scrollbar baru
        scrollbar.value = newScrollbarValue;
    }
}
