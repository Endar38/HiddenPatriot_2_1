using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoinKontrol : MonoBehaviour
{



    public delegate void PrefabInstantiated(GameObject instantiatedPrefab);
    public static event Action<GameObject> OnPrefabInstantiated;

    public static event Action<GameObject> OnPrefabDestroyed;
    public bool meledak;
    // Start is called before the first frame update
    void Start()
    {
        if (OnPrefabInstantiated != null)
        {
            OnPrefabInstantiated(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            meledak = true;
        }
    }

    void OnDestroy()
    {
        // Memanggil event saat prefab di-destroy
        if (OnPrefabDestroyed != null)
        {
            OnPrefabDestroyed(gameObject);
        }
    }
}
