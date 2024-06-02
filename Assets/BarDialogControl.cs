using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarDialogControl : MonoBehaviour
{

    public Slider slider; // Slider untuk mengontrol pengisian bar
    public float fillSpeed = 1f; // Kecepatan pengisian bar
    private bool conditionMet = false; // Kondisi yang memicu pengisian bar
    private bool isFilling = false; // Status pengisian bar

    public pathEnemy pe1;
    public pathEnemy pe2;

    public GameObject panelDialog;
    public ObjInisiasi objMuncul;
    public GameObject teksTips;

    void Update()
    {
        // Jika tombol 'K' ditekan, ubah kondisi menjadi true
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetCondition(true);
        }

        // Jika tombol 'K' dilepas, ubah kondisi menjadi false
        if (Input.GetKeyUp(KeyCode.K))
        {
            SetCondition(false);
        }

        if (conditionMet && !isFilling)
        {
            StartFilling(); // Mulai mengisi bar jika kondisi terpenuhi
        }
    }

    // Fungsi untuk memulai pengisian bar
    void StartFilling()
    {
        isFilling = true;
        slider.value = 0f; // Mulai dari awal
    }

    // Fungsi untuk menghentikan pengisian bar
    void StopFilling()
    {
        panelDialog.SetActive(false);
        teksTips.SetActive(false);
        
        objMuncul.SetTriggerTrue();
        pe1.bolehGantiBvior = true;
        pe2.bolehGantiBvior = true;
        isFilling = false;
    }

    void FixedUpdate()
    {
        if (isFilling)
        {
            Fill(); // Lanjutkan mengisi bar jika sedang dalam proses pengisian
        }
    }

    // Fungsi untuk mengisi bar
    void Fill()
    {
        if (slider.value < 1f)
        {
            slider.value += fillSpeed * Time.fixedDeltaTime; // Menambah nilai slider sesuai kecepatan
        }
        else
        {
            StopFilling(); // Berhenti mengisi jika sudah penuh
        }
    }

    // Fungsi untuk mengatur kondisi yang memicu pengisian bar
    public void SetCondition(bool condition)
    {
        conditionMet = condition;

        if (!condition)
        {
            StopFilling(); // Berhenti mengisi jika kondisi tidak terpenuhi
        }
    }
}


