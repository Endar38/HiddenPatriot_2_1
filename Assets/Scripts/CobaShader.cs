using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobaShader : MonoBehaviour
{

    // Referensi ke renderer sprite
    private SpriteRenderer spriteRenderer;

    // Shader properties
    private int outlineEnabledPropertyID;

    // Material instance
    private Material materialInstance;

    void Start()
    {
        // Mendapatkan referensi ke SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Mendapatkan ID properti shader untuk _OutlineEnabled
        outlineEnabledPropertyID = Shader.PropertyToID("_OutlineEnabled");

        // Membuat instance baru dari material sprite renderer
        materialInstance = Instantiate(spriteRenderer.material);
        spriteRenderer.material = materialInstance;

        // Default outline dinonaktifkan
        SetOutlineEnabled(false);
    }

    void Update()
    {
        // Periksa apakah mouse menyentuh sprite
        if (MouseIsTouchingSprite() && (tembak.waktuPilihMelee || tembak.waktuPilihTembak))
        {
            // Aktifkan outline jika mouse menyentuh sprite
            SetOutlineEnabled(true);
        }
        else
        {
            // Nonaktifkan outline jika mouse tidak menyentuh sprite
            SetOutlineEnabled(false);
        }
    }

    // Fungsi untuk menentukan apakah mouse menyentuh sprite
    bool MouseIsTouchingSprite()
    {
        // Ambil posisi mouse dalam koordinat dunia
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Periksa apakah posisi mouse berada di dalam collider sprite
        return transform.Find("colFull").gameObject.GetComponent<Collider2D>().OverlapPoint(mousePosition);
    }

    // Fungsi untuk mengaktifkan atau menonaktifkan outline
    void SetOutlineEnabled(bool enabled)
    {
        materialInstance.SetFloat(outlineEnabledPropertyID, enabled ? 1 : 0);
    }
}

