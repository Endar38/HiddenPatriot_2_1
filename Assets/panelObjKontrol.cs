using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelObjKontrol : MonoBehaviour
{
    // Start is called before the first frame update
    public KeretaKontrol keretaKontrol;
    public GameObject panelObj;
    public GameObject player;
    public AudioSource sourceEfek;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetFalsePntp()
    {
        gameObject.SetActive(false);
    }
    public void OnAnimationEnd()
    {
        keretaKontrol.waktuJalan = true;
        Debug.Log("Animation finished, condition set to true");
        gameObject.SetActive(false);
        panelObj.SetActive(false);
        player.SetActive(true);
    }
    public void StartJalan()
    {
        sourceEfek.Play();
    }
    public void StopJalan()
    {
        sourceEfek.Stop();
    }
}
