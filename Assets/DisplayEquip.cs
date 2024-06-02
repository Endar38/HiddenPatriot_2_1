using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayEquip : MonoBehaviour
{
    public PauseManajer pauseManager;
    AutoMoveControl AMC;
    public Animator anim;
    public GameObject penutup;
    
    // Start is called before the first frame update
    void Start()
    {
        AMC = GetComponent<AutoMoveControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("pointAddEquipment"))
        {
            StartCoroutine(ProsesSentuhEquip());
            Destroy(coll.gameObject);
        }
    }

    IEnumerator ProsesSentuhEquip()
    {
        yield return new WaitUntil(() => AMC.waktuAutoMove);


        yield return new WaitForSeconds(0.5f);
        AMC.waktuAutoMove = false;
        GetComponent<playerGridMove>().isMoving = false;

        GetComponent<playerGridMove>().currentTargetIndex = 0;
        //Time.timeScale = 0f;
        pauseManager.colPlayer.enabled = false;
        penutup.SetActive(true);
        anim.SetFloat("status", 0);
        pauseManager.nungguTutup = true;
        pauseManager.tampilEquipment = true;
        AMC.balikAM = true;
    }
}
