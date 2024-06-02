using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderLayerManager : MonoBehaviour
{

    public List<GameObject> objectsToOrder = new List<GameObject>();
    public float maxPosY = 12f; // Nilai maksimal posisi y
    public int minOrderLayer = 1; // Nilai order layer minimal

    void Update()
    {
        foreach (GameObject obj in objectsToOrder)
        {
            if (obj != null)
            {
                SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    // Mengatur order layer berdasarkan posisi y
                    float yPos = obj.transform.position.y;
                    int orderLayer = Mathf.RoundToInt((maxPosY - yPos) * 10); // Perubahan orientasi, posisi 12 akan memiliki nilai order layer 1
                    orderLayer = Mathf.Max(minOrderLayer, orderLayer); // Pastikan order layer tidak kurang dari nilai minimal
                    spriteRenderer.sortingOrder = orderLayer;
                }
            }
        }
    }

    void OnEnable()
    {
        // Subscribe ke event saat prefab di-instantiate
        KoinKontrol.OnPrefabInstantiated += AddPrefabToList;
        // Subscribe ke event saat prefab di-destroy
        KoinKontrol.OnPrefabDestroyed += RemovePrefabFromList;
    }

    void OnDisable()
    {
        // Unsubscribe dari event saat objek dinonaktifkan
        KoinKontrol.OnPrefabInstantiated -= AddPrefabToList;
        KoinKontrol.OnPrefabDestroyed -= RemovePrefabFromList;
    }

    void AddPrefabToList(GameObject instantiatedPrefab)
    {
        // Menambahkan prefab yang di-instantiate ke dalam list
        objectsToOrder.Add(instantiatedPrefab);
    }

    void RemovePrefabFromList(GameObject destroyedPrefab)
    {
        // Menghapus prefab yang di-destroy dari list
        objectsToOrder.Remove(destroyedPrefab);
    }
}
