using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelKontrol : MonoBehaviour
{
    public int currentLevel;
    public Sprite[] spriteImgLvl;
    public Sprite[] spriteStoryLvl;
    public Sprite[] spriteTextLvl;
    public GameObject keyLock;
    public Image imgLvl;
    public Image storyLvl;
    public Image textLvl;
    public int lvlCount;
    // Start is called before the first frame update
    void Start()
    {
        SaveManager.SaveData1(currentLevel, 1);
        currentLevel = 1;
        imgLvl.sprite = spriteImgLvl[0];
        storyLvl.sprite = spriteStoryLvl[0];
        textLvl.sprite = spriteTextLvl[0];
        keyLock.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLvlClicked()
    {
        if (currentLevel < lvlCount)
        {
            currentLevel++;
        }
        else
        {
            currentLevel = 1;
        }
        imgLvl.sprite = spriteImgLvl[currentLevel - 1];
        storyLvl.sprite = spriteStoryLvl[currentLevel - 1];
        textLvl.sprite = spriteTextLvl[currentLevel - 1];
        LockLevelCheck();
        
    }

    public void PrevLvlClicked()
    {
        if (currentLevel > 1)
        {
            currentLevel--;
        }
        else
        {
            currentLevel = lvlCount;
        }
        imgLvl.sprite = spriteImgLvl[currentLevel - 1];
        storyLvl.sprite = spriteStoryLvl[currentLevel - 1];
        textLvl.sprite = spriteTextLvl[currentLevel - 1];
        LockLevelCheck();

    }

    public void LockLevelCheck()
    {
        int lvl = currentLevel;
        int testLvl;
        SaveManager.LoadData1(out testLvl, lvl);
        if (testLvl == 0)
        {
            keyLock.SetActive(true);
        }
        else
        {
            keyLock.SetActive(false);
        }
    }
}
