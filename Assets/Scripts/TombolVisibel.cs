using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TombolVisibel : MonoBehaviour
{

    public Button[] targetButtons; // Tombol target yang ingin ditampilkan/sembunyikan
    private bool isVisible = false; // Status visibilitas tombol target
     bool bolehDelete;
    public AutoMoveControl controlM;
    tombolMove TMovee;

    void Start()
    {
        TMovee = GetComponent<tombolMove>();
        bolehDelete = true;
        // Menonaktifkan visibilitas semua tombol target saat awal permainan
        foreach (Button targetButton in targetButtons)
        {
            targetButton.gameObject.SetActive(false);
        }
    }
    public void ToggleTargetVisibility(bool delt)
    {
        if (controlM.waktuAutoMove)
        {
            isVisible = !isVisible; // Ubah status visibilitas

            // Atur visibilitas tombol target berdasarkan status
            foreach (Button targetButton in targetButtons)
            {
                targetButton.gameObject.SetActive(isVisible);
            }

            // Tambahkan logika tambahan berdasarkan status visibilitas tombol target
            if (isVisible)
            {
                // Logika saat tombol target muncul

               // TMovee.PosAwalButton();
                Debug.Log("Tombol target muncul!");
            }
            else
            {
                if (delt)
                {
                    GetComponent<ActionManager>().commandSequence.Clear();
                    ActionManager.waktuPilih = true;
                }

                ActionManager.bolehPilih = false;
                /*
                GetComponent<ActionManager>().statusJalan = false;
                GetComponent<ActionManager>().statusJongkok = false;
                GetComponent<ActionManager>().statusLemparKoin = false;
                GetComponent<ActionManager>().statusLemparSmoke = false;
                GetComponent<ActionManager>().statusBerdiri = false;
                GetComponent<ActionManager>().statusTembak = false;
                */
                tombolMove.returnAllToOriginalPosition = true;
                // Logika saat tombol target hilang
                Debug.Log("Tombol target hilang!");
            }
        }
    }
        
}

