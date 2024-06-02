using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tombolMove : MonoBehaviour
{

    public RectTransform[] equipmentPositions; // Array untuk menyimpan posisi tombol peralatan
    public RectTransform[] posAwal;
    public Button[] equipmentButtons; // Array untuk menyimpan tombol peralatan

    private List<Button> selectedEquipment = new List<Button>(); // List untuk menyimpan tombol peralatan yang dipilih
    private Dictionary<Button, Vector3> originalPositions = new Dictionary<Button, Vector3>(); // Dictionary untuk menyimpan posisi asli setiap tombol

    // Variabel static untuk menentukan apakah tombol dapat berpindah dari posisi asalnya
    public static bool allowMove = false;

    // Variabel static untuk menentukan apakah seluruh tombol yang berpindah tempat harus kembali ke posisi awal
    public static bool returnAllToOriginalPosition = false;

    void Start()
    {
        // Simpan posisi asli setiap tombol
        for (int i = 0; i < equipmentButtons.Length; i++)
        {
            
            originalPositions.Add(equipmentButtons[i], posAwal[i].position);
        }

        // Menetapkan fungsi untuk setiap tombol peralatan
        for (int i = 0; i < equipmentButtons.Length; i++)
        {
            int index = i; // Simpan nilai i agar dapat diakses di dalam lambda expression
            equipmentButtons[i].onClick.AddListener(() => ToggleEquipmentSelection(index));
        }
        //StartCoroutine(PosAwalButton());
    }

    void ToggleEquipmentSelection(int index)
    {
        // Periksa apakah indeks yang diklik berada dalam rentang yang valid
        if (index < 0 || index >= equipmentButtons.Length)
        {
            // Indeks di luar rentang, jadi tidak melakukan apa-apa
            return;
        }

        Button clickedButton = equipmentButtons[index]; // Tombol yang diklik

        // Jika tombol tidak dapat berpindah dari posisi asalnya, keluar dari method
        if (!allowMove)
        {
            return;
        }

        // Jika tombol sudah dipilih, batalkan pemilihan
        if (selectedEquipment.Contains(clickedButton))
        {
            int i = Array.IndexOf(equipmentButtons, clickedButton);
            selectedEquipment.Remove(clickedButton);
            clickedButton.transform.Find("Image").gameObject.SetActive(true);
            clickedButton.GetComponent<RectTransform>().position = posAwal[i].position; // Kembalikan tombol ke posisi aslinya
            UpdateEquipmentPositions();
        }
        // Jika tombol belum dipilih dan masih ada posisi kosong
        else if (selectedEquipment.Count < equipmentPositions.Length)
        {
            selectedEquipment.Add(clickedButton);
            clickedButton.transform.Find("Image").gameObject.SetActive(false);
            clickedButton.GetComponent<RectTransform>().position = equipmentPositions[selectedEquipment.Count - 1].position; // Pindahkan tombol ke posisi terakhir dalam list yang dipilih
        }
    }

    void UpdateEquipmentPositions()
    {
        // Atur posisi setiap tombol peralatan berdasarkan urutan peralatan yang dipilih
        for (int i = 0; i < selectedEquipment.Count; i++)
        {
            selectedEquipment[i].GetComponent<RectTransform>().position = equipmentPositions[i].position;
        }
    }

    void Update()
    {
        // Jika returnAllToOriginalPosition bernilai true, kembalikan semua tombol ke posisi asal
        if (returnAllToOriginalPosition)
        {
            foreach (Button button in selectedEquipment)
            {
                int i = Array.IndexOf(equipmentButtons, button);
                button.GetComponent<RectTransform>().Find("Image").gameObject.SetActive(true);
                button.GetComponent<RectTransform>().position = posAwal[i].position;
            }
            selectedEquipment.Clear(); // Bersihkan list selectedEquipment
            returnAllToOriginalPosition = false; // Set returnAllToOriginalPosition kembali ke false setelah semua tombol dikembalikan
        }
    }
    /*
    IEnumerator PosAwalButton()
    {
        yield return new WaitForSeconds(1f);
        foreach (Button button in equipmentButtons)
        {
            originalPositions[button] = button.GetComponent<RectTransform>().position;
        }
    }*/
}
