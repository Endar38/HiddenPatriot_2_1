using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeretaKontrol : MonoBehaviour
{

    public bool waktuJalan;
    public AudioSource sourceKereta;
    Vector2 titikTarget;
    // Start is called before the first frame update
    void Start()
    {
        titikTarget = new Vector2(transform.position.x, transform.position.y - 30f);
    }

    // Update is called once per frame
    void Update()
    {
        if (waktuJalan)
        {
            transform.position = Vector2.MoveTowards(transform.position, titikTarget , 5 *Time.deltaTime);
            if (!sourceKereta.isPlaying )
            {
                sourceKereta.Play();
            }
            
            if (Vector2.Distance(transform.position, titikTarget) <= 0f)
            {
                sourceKereta.Stop();
                ActionManager.kondisiMenang = true;
                waktuJalan = false;
            }
        }
    }
}
