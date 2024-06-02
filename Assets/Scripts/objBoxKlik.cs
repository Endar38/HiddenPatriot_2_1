using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objBoxKlik : MonoBehaviour
{

    public Sprite[] spriteDefault;
    public Sprite[] spriteCliked;
    public Button[] buttonObj;
    private bool bolehKlikObj;

    public int indexKlikBox = 0;



    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttonObj.Length; i++)
        {
            int index = i;
            buttonObj[i].image.sprite = spriteDefault[index];
            buttonObj[i].onClick.AddListener(() => ClickedBox(index));
            buttonObj[i].interactable = true;
            
        }
        //gameObject.SetActive(false);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (indexKlikBox <= 0)
        {
            ActionManager.kondisiMenang = true;
            //gameObject.SetActive(false);
        }
    }
    public void ClickedBox(int indexBox)
    {
        Debug.Log(indexBox);
        buttonObj[indexBox].image.sprite = spriteCliked[indexBox];
        buttonObj[indexBox].interactable = false;
        indexKlikBox--;
    }
}
