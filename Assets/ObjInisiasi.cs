using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjInisiasi : MonoBehaviour
{
    public RectTransform target;
    public GameObject buttonPrefab; // Prefab tombol yang akan diinisiasi
    public Canvas canvas; // Referensi canvas tempat tombol akan diinisiasi
    public RectTransform targetPosition; // RectTransform dari objek UI target untuk inisiasi tombol
    public RectTransform moveDestination; // RectTransform dari objek UI tujuan untuk pergerakan tombol
    public float moveSpeed = 1f; // Kecepatan pergerakan tombol

    private bool trigger = false; // Bool yang akan memicu inisiasi tombol

    public Camera uiCam;

    void Update()
    {
        if (trigger)
        {
            // Inisiasi tombol prefab
            GameObject newButton = Instantiate(buttonPrefab, canvas.transform);
            ObjDragDrop objDD = newButton.GetComponent<ObjDragDrop>();
            objDD.targetObject = target;
            objDD.GetComponent<ObjDragDrop>().oriPos = moveDestination;
            objDD.GetComponent<ObjDragDrop>().UICame = uiCam;
            // Ubah skala dari 0 menjadi 1
            newButton.transform.localScale = Vector3.zero;

            // Atur posisi tombol relatif terhadap canvas menggunakan RectTransform dari objek UI target
            RectTransform newButtonRectTransform = newButton.GetComponent<RectTransform>();
            newButtonRectTransform.SetSiblingIndex(10);
            Vector3 targetPos = targetPosition.position;
            newButtonRectTransform.position = targetPos;

            // Animasi perubahan skala
            newButton.transform.localScale = Vector3.one; // Set skala menjadi 1 secara langsung

            // Pindahkan tombol ke posisi yang ditentukan dengan animasi pergerakan menggunakan RectTransform dari objek UI tujuan
            StartCoroutine(MoveButton(newButtonRectTransform, moveDestination.position, moveSpeed));

            trigger = false; // Setel trigger kembali ke false setelah inisiasi tombol
        }
    }

    // Metode untuk mengubah nilai trigger menjadi true dari luar
    public void SetTriggerTrue()
    {
        trigger = true;
    }

    // Coroutine untuk animasi pergerakan tombol
    IEnumerator MoveButton(RectTransform buttonTransform, Vector3 destination, float speed)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = buttonTransform.position;

        // Lakukan pergerakan selama waktu tertentu
        while (elapsedTime < speed)
        {
            buttonTransform.position = Vector3.Lerp(startPosition, destination, (elapsedTime / speed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Pastikan posisi akhir adalah posisi tujuan
        buttonTransform.position = destination;

        Debug.Log("Button movement completed!"); // Pesan debug ketika pergerakan selesai
    }
}

