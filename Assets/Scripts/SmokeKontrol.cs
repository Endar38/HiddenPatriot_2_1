using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeKontrol : MonoBehaviour
{
    public ParticleSystem[] efekSmoke;
    public bool efekLedakanSmoke;
    private Quaternion localRotasi;
    private Transform rotasiInduk;

    private ParticleSystemRenderer[] rendSmoke;
    private SpriteRenderer RendSmokeSprite;

    

    // Start is called before the first frame update
    void Start()
    {
        localRotasi = efekSmoke[0].transform.localRotation;
        rotasiInduk = transform;


        rendSmoke = new ParticleSystemRenderer[efekSmoke.Length];
        for (int i = 0; i < efekSmoke.Length; i++)
        {
            rendSmoke[i] = efekSmoke[i].GetComponent<ParticleSystemRenderer>();
        }
        RendSmokeSprite = transform.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {

        foreach (ParticleSystemRenderer renderer in rendSmoke)
        {
            if (renderer.sortingOrder != RendSmokeSprite.sortingOrder)
            {
                renderer.sortingOrder = RendSmokeSprite.sortingOrder;
            }
        }

        if (efekLedakanSmoke)
        {
            foreach (ParticleSystem psSmoke in efekSmoke)
            {
                if (!psSmoke.isPlaying)
                {
                    psSmoke.transform.localRotation = localRotasi;
                    psSmoke.Play();
                    StartCoroutine(StopParticleSystemWhenFinished(2));
                }
            }
            efekLedakanSmoke = false;
        }
        
    }
    private void LateUpdate()
    {
        Vector3 indukRot = rotasiInduk.eulerAngles;

        Vector3 invertedRot = new Vector3 (-indukRot.x, -indukRot.y, -indukRot.z);




        efekSmoke[0].transform.localEulerAngles = invertedRot;
        
    }

    IEnumerator StopParticleSystemWhenFinished(int i)
    {
        // Tunggu sampai partikel selesai diputar
        while (efekSmoke[i].isPlaying)
        {
            yield return null;
        }

        // Ketika selesai, hentikan partikel
       // efekSmoke[i].GetComponent<PS_Smoke>().stopEfek = true;
        efekSmoke[i].Stop();
    }
}
