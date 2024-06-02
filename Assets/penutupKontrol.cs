using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class penutupKontrol : MonoBehaviour
{

    public PauseManajer pauseMnj;
    public AudioSource sourceEfek;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EndAnimated()
    {
        
        gameObject.SetActive(false);
        if (pauseMnj != null )
        {
            pauseMnj.bolehTutup = true;
        }
        
    }
    public void StartAnim()
    {
        sourceEfek.Play();
    }

    public void StopJalan()
    {
        sourceEfek.Stop();
    }
}
