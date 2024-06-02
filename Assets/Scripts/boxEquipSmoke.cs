using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boxEquipSmoke : MonoBehaviour
{

    public Button[] boxesSmoke; // Array untuk menyimpan button-button
    public Button[] boxesPistol;
    public Button[] boxesKoin;

    public Text textTotalBox;

    public int boxesSmokeCount = 0; // Jumlah kotak yang aktif
    public int boxesPistolCount = 0;
    public int boxesKoinCount = 0;
    public int totalBox;
    public int MaxBox = 10;

    public int maxBokLv2;
    public int maxBokLv3;
    ActionManager actionManager;



    private void Start()
    {
        textTotalBox.text = ("0 / 10");
        actionManager = GetComponent<ActionManager>();
        if (actionManager.level == 1)
        {
            MaxBox = 10;
        }
        else if (actionManager.level == 2)
        {
            MaxBox = maxBokLv2;
        }
        else if (actionManager.level == 3)
        {
            MaxBox = maxBokLv3;
        }
    }
    private void Update()
    {
        textTotalBox.text = (totalBox + " / " + MaxBox);
    }

    // Method yang dipanggil saat kotak di-klik
    public void OnBoxSmokeClick(int index)
    {
        boxesSmokeCount = 0;
        // Matikan semua kotak yang sebelumnya menyala
        for (int i = 0; i < boxesSmoke.Length; i++)
        {
            if (i < index)
            {
                // Kotak-kotak sebelum kotak yang diklik harus menyala
                boxesSmoke[i].GetComponent<Image>().color = Color.yellow;
                boxesSmokeCount++;
                totalBox = boxesKoinCount + boxesSmokeCount + boxesPistolCount;
                if (totalBox > MaxBox)
                {
                    boxesSmokeCount--;
                    boxesSmoke[i].GetComponent<Image>().color = Color.white;
                }
            }
            else
            {
                // Kotak-kotak setelah atau sama dengan kotak yang diklik harus dimatikan
                boxesSmoke[i].GetComponent<Image>().color = Color.white;
            }
        }

        if (totalBox > MaxBox)
        {
            totalBox = MaxBox;
        }

        textTotalBox.text = (totalBox + " / 10");
        // Tampilkan jumlah kotak yang aktif di konsol
        Debug.Log("Active Boxes Count: " + boxesSmokeCount);
    }

    public void OnBoxPistolClick(int index)
    {
        boxesPistolCount = 0;
        // Matikan semua kotak yang sebelumnya menyala
        for (int i = 0; i < boxesPistol.Length; i++)
        {
            if (i < index)
            {
                // Kotak-kotak sebelum kotak yang diklik harus menyala
                boxesPistol[i].GetComponent<Image>().color = Color.yellow;
                boxesPistolCount++;
                totalBox = boxesKoinCount + boxesSmokeCount + boxesPistolCount;
                if (totalBox > MaxBox)
                {
                    boxesPistolCount--;
                    boxesPistol[i].GetComponent<Image>().color = Color.white;
                }
            }
            else
            {
                // Kotak-kotak setelah atau sama dengan kotak yang diklik harus dimatikan
                boxesPistol[i].GetComponent<Image>().color = Color.white;
            }
        }

        if (totalBox > MaxBox)
        {
            totalBox = MaxBox;
        }
        textTotalBox.text = (totalBox + " / 10");
        // Tampilkan jumlah kotak yang aktif di konsol
        Debug.Log("Active Boxes Count: " + boxesPistolCount);
    }

    public void OnBoxKoinClick(int index)
    {
        boxesKoinCount = 0;
        // Matikan semua kotak yang sebelumnya menyala
        for (int i = 0; i < boxesKoin.Length; i++)
        {
            if (i < index)
            {
                // Kotak-kotak sebelum kotak yang diklik harus menyala
                boxesKoin[i].GetComponent<Image>().color = Color.yellow;
                boxesKoinCount++;
                totalBox = boxesKoinCount + boxesSmokeCount + boxesPistolCount;
                if (totalBox > MaxBox)
                {
                    boxesKoinCount--;
                    boxesKoin[i].GetComponent<Image>().color = Color.white;
                }
            }
            else
            {
                // Kotak-kotak setelah atau sama dengan kotak yang diklik harus dimatikan
                boxesKoin[i].GetComponent<Image>().color = Color.white;
            }
        }

        // Tampilkan jumlah kotak yang aktif di konsol
        if (totalBox > MaxBox)
        {
            totalBox = MaxBox;
        }
        textTotalBox.text = (totalBox + " / 10");
        Debug.Log("Active Boxes Count: " + boxesKoinCount);
    }


}

