using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManajer : MonoBehaviour
{
    public GameObject pausePanel; // Referensi ke panel pause
    public GameObject optionPanel;
    public GameObject blurPanel;
    public GameObject blurPanelEquip;
    public GameObject equipPanel;
    public bool tampilEquipment;
    public GameObject penutup;
    public bool nungguTutup;
    public bool bolehTutup;
    public Animator anim;
    public Collider2D colPlayer;

    ActionManager actionManager;


    //private bool isPaused = false; // Status game sedang di-pause atau tidak

    // Method yang akan dipanggil saat tombol pause ditekan

    private void Start()
    {
        pausePanel.SetActive(false);
        optionPanel.SetActive(false);
        blurPanel.SetActive(false);
        /*
        blurPanelEquip.SetActive(true);
        equipPanel.SetActive(true);
        Time.timeScale = 0f;
        */
        actionManager = GetComponent<ActionManager>();
    }
    private void Update()
    {
        if (tampilEquipment)
        {
            blurPanelEquip.SetActive(true);
            equipPanel.SetActive(true);
            //Time.timeScale = 0f;
            tampilEquipment = false;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("GameplayLvl2");
        }
    }
    public void PauseGame()
    {
        //isPaused = true;
        Time.timeScale = 0f; // Berhenti waktu game
        pausePanel.SetActive(true); // Tampilkan panel pause
        blurPanel.SetActive(true);
    }

    // Method yang akan dipanggil saat tombol lanjutkan ditekan
    public void ResumeGame()
    {
        //isPaused = false;
        Time.timeScale = 1f; // Lanjutkan waktu game
        pausePanel.SetActive(false); // Sembunyikan panel pause
        blurPanel.SetActive(false );
    }

    public void KeMenu()
    {
       SceneManager.LoadScene("MainMenu");
       Time.timeScale = 1f;
    }

    public void KeOption()
    {
        pausePanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void KembaliPause()
    {
        optionPanel.SetActive(false);
        pausePanel.SetActive(true );
    }

    public void PlayLevelMulai(int level)
    {
        if (nungguTutup == false)
        {
            TutupEquip();

        }
        else if (nungguTutup == true)
        {
            StartCoroutine(prosesEquipmentTutup());
        }


    }

    IEnumerator prosesEquipmentTutup()
    {
        bolehTutup = false;
        
        penutup.SetActive(true) ;
        anim.SetFloat("status", 1);
        yield return new WaitUntil(() => bolehTutup);
        TutupEquip() ;
        colPlayer.enabled = true;

    }

    void TutupEquip()
    {
        blurPanelEquip.SetActive(false);
        equipPanel.SetActive(false);
        Time.timeScale = 1f;

        ActionManager.smokeSisa = ActionManager.smokeSisa + GetComponent<boxEquipSmoke>().boxesSmokeCount;
        ActionManager.pistolSisa = ActionManager.pistolSisa + GetComponent<boxEquipSmoke>().boxesPistolCount;
        ActionManager.koinSisa = ActionManager.koinSisa + GetComponent<boxEquipSmoke>().boxesKoinCount;
    }

    public void UlangLvl()
    {
        SceneManager.LoadScene("GameplayLvl" + actionManager.level);
        Time.timeScale = 1f;
    }

}
