using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteSkala : MonoBehaviour
{
    void Start()
    {
        AdjustBackground();
    }

    public void AdjustBackground()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        // Mendapatkan ukuran sprite dalam unit dunia
        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        // Mendapatkan ukuran layar dalam unit dunia
        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        // Menghitung skala yang dibutuhkan
        Vector3 scale = transform.localScale;
        scale.x = worldScreenWidth / width;
        scale.y = worldScreenHeight / height;
        transform.localScale = scale;
    }
}
