using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class skripItemDrop : MonoBehaviour
{
    public GameObject[] prefabitemDrop;
    public int[] itemCount;
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead )
        {
            if (itemCount != null)
            {
                for (int i = 0; i < prefabitemDrop.Length; i++)
                {
                    for (int j = 0; j < itemCount[i]; j++)
                    {
                        DropItem(prefabitemDrop[i]);
                        Debug.Log("fff");
                    }

                }
            }
            isDead = false;
            gameObject.SetActive(false);
        }
    }

    public void DropItem(GameObject item)
    {
        float[] rentangRotasi = {  90f, 180f, 270f };
        Quaternion rotasi = Quaternion.Euler(0f, 0f, rentangRotasi[Random.Range(0, rentangRotasi.Length - 1)]);
        GameObject dropItem = Instantiate(item, transform.position, rotasi);
        //dropItem.transform.rotation = rotasi;


    }
}
