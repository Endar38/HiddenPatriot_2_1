using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropSet : MonoBehaviour
{
    
    public int jenisItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            if (jenisItem == 1)
            {
                ActionManager.smokeSisa++;
            }
            else if (jenisItem == 2)
            {
                ActionManager.pistolSisa++;
            }
            else if (jenisItem == 3)
            {
                ActionManager.koinSisa++;
            }else if (jenisItem == 4)
            {
                ActionManager.kunci++;
            }
            Destroy(gameObject);
        }
    }
}
