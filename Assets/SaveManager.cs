using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private void Start()
    {
       
    }

    // Fungsi untuk menyimpan data level ke PlayerPrefs
    public static void SaveLevelData(int level, int smokeSisa, int pistolSisa, int koinSisa)
    {
        string levelKey = "Level_" + level; // Membuat kunci unik untuk setiap level
        PlayerPrefs.SetInt(levelKey + "smokeSisa", smokeSisa);
        PlayerPrefs.SetInt(levelKey + "pistolSisa", pistolSisa);
        PlayerPrefs.SetInt(levelKey + "koinSisa", koinSisa);
        PlayerPrefs.Save();
    }
    public static void SaveData1(int level, int unlock)
    {
        PlayerPrefs.SetInt("level" + level, unlock);
        PlayerPrefs.Save();
    }
    public static void LoadData1(out int level, int levelUnlock)
    {
        level = PlayerPrefs.GetInt("level" + levelUnlock, 0);
    }

    // Fungsi untuk memuat data level dari PlayerPrefs
    public static void LoadLevelData(int level, out int smokeSisa, out int pistolSisa, out int koinSisa)
    {
        string levelKey = "Level_" + level;
        smokeSisa = PlayerPrefs.GetInt(levelKey + "smokeSisa", 0);
        pistolSisa = PlayerPrefs.GetInt(levelKey + "pistolSisa", 0);
        koinSisa = PlayerPrefs.GetInt(levelKey + "koinSisa", 0);

    }

    // Fungsi untuk menghapus data level dari PlayerPrefs
    public static void ResetLevelData(int level)
    {
        string levelKey = "Level_" + level;
        PlayerPrefs.DeleteKey(levelKey + "smokeSisa");
        PlayerPrefs.DeleteKey(levelKey + "pistolSisa");
        PlayerPrefs.DeleteKey(levelKey + "koinSisa");
        PlayerPrefs.Save();
    }

    public static void ResetDataAll()
    {
        PlayerPrefs.DeleteAll();
        SaveData1(1, 1);
        //PlayerPrefs.Save();
    }

    public static void SaveOptionSound(float valueVol, string audioName)
    {
        PlayerPrefs.SetFloat("volume" + audioName, valueVol);
        PlayerPrefs.Save();
    }

    public static void LoadOptionSound(out float valueVol, string audioName)
    {
        valueVol = PlayerPrefs.GetFloat("volume" + audioName, 0.5f);
    }
}
