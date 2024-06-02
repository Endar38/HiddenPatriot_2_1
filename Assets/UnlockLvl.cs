using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLvl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            SaveManager.SaveData1(2, 1);
            SaveManager.SaveData1(3, 1);
        }
    }
}
