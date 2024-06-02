using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missKontrol : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource sourceMiss;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MissPlay()
    {
        if (sourceMiss.isPlaying == false)
        {
            sourceMiss.time = 0.2f;
            sourceMiss.Play();
        }
    }

    public void MissStop()
    {
        if (sourceMiss.isPlaying)
        {
            sourceMiss.Stop();
        }
    }


}
