using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAktifKontrol : MonoBehaviour
{
    // Start is called before the first frame update

    public bool aktifImage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(aktifImage);
    }
}
