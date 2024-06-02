using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHighlight : MonoBehaviour
{

    public float transparencyValue = 1f;
    private SpriteRenderer spriteRenderer;
    public Color originalColor;

    public Color lightColor1 = Color.green;
    public Color smokeColor = Color.green;

    public bool balikWarna2;
    public bool balikWarna;
    Color newColSmoke;

    void Start()
    {
        balikWarna = true;
        balikWarna2 = true;
        // Dapatkan komponen Sprite Renderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Simpan warna asli
        originalColor = spriteRenderer.color;
        newColSmoke = smokeColor;
    }
    void Update()
    {
        if (balikWarna2 && balikWarna && !GetComponent<GridStart>().terkenaGranat)
        {
            spriteRenderer.color = originalColor;
            newColSmoke.a = 0;
        }


        if (GetComponent<GridStart>().terkenaGranat && spriteRenderer.color.a <= smokeColor.a)
        {
           newColSmoke.a = newColSmoke.a + Time.deltaTime / 500;
            
            spriteRenderer.color = newColSmoke;
        }
    }

    // Dipanggil saat mouse masuk ke objek
    void OnMouseEnter()
    {
        if (balikWarna)
        {
            balikWarna2 = false;
            // Mengatur warna saat mouse masuk
            spriteRenderer.color = lightColor1;
            Color newColor = spriteRenderer.color;
            newColor.a = transparencyValue;
            spriteRenderer.color = newColor;
        }
        
    }

    // Dipanggil saat mouse keluar dari objek
    void OnMouseExit()
    {
        balikWarna2 = true;
   
        
    }
}


