using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobilKontrol : MonoBehaviour
{

    public GameObject player;
    public GameObject panelObj;
    public Animator anim;
    public GameObject target;
    public GameObject dialogBar;
    public GameObject pw;
    public AudioSource sourceMobil;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ObjDragDrop.pwBenar)
        {
            target.SetActive(false);
            dialogBar.SetActive(false); 
            anim.SetBool("Jalan", true);
        }
    }

    public void MobilSampe()
    {
        ActionManager.kondisiMenang = true;
        //panelObj.SetActive(false);
        player.SetActive(true);

    }

    public void MobilStart()
    {
        if (sourceMobil.isPlaying == false)
        {
            sourceMobil.Play();
        }
    }

    public void MobilStop()
    {
        if (sourceMobil.isPlaying)
        {
            sourceMobil.Stop();
        }
    }
}
