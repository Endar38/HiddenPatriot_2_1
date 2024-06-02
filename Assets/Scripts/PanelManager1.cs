using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager1 : MonoBehaviour
{
    public GameObject[] panelPanel;
    public GameObject panelLevel;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject panel1 in panelPanel)
        {
            panel1.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MunculPanel(string Name)
    {
        GameObject panelAktif1 = GameObject.Find(Name);
        foreach (GameObject panel1 in panelPanel)
        {
            if (panel1.name != Name)
            {
                panel1.SetActive(false);
            }
            else
            {
                panel1.SetActive(true);
            }
        }
 
    }

    public void Keluar()
    {
        Application.Quit();
    }

    public void PlayMain()
    {
        int lvl = panelLevel.GetComponent<LevelKontrol>().currentLevel;
        int testLvl;
        SaveManager.LoadData1(out testLvl, lvl);
        if (testLvl == 1)
        {
            SceneManager.LoadScene("GameplayLvl" + lvl);
        }
        
    }

    public void ResetDataAllKlik()
    {
        SaveManager.ResetDataAll();
        panelLevel.GetComponent<LevelKontrol>().LockLevelCheck();
    }


}
