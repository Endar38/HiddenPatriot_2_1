using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjektifControl : MonoBehaviour
{

    public LayerMask clickableLayer;

    //private GameObject tileObj;
    public GameObject player;
    public GameObject panelObj;
    public int boxObj;
    public bool playerTidakIlang;
    bool cekObj;
    AutoMoveControl AMCon;
    // Start is called before the first frame update
    void Start()
    {
        panelObj.SetActive(false);
        AMCon = player.GetComponent<AutoMoveControl>();
        cekObj = false;
        //deteksiObj();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector2.Distance(transform.position, player.transform.position) <= 1 && !cekObj)
        {
            Debug.Log("Menang");

            StartCoroutine(ProsesObj());
            cekObj = true;
            

           
        }
    }

    /*void deteksiObj()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, clickableLayer);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("tile"))
        {
            tileObj = hit.collider.gameObject;
        }
    }*/

    IEnumerator ProsesObj()
    {
        yield return new WaitUntil(() => AMCon.waktuAutoMove);


        yield return new WaitForSeconds(0.5f);


        if (playerTidakIlang == false)
        {
            player.transform.position = new Vector2(99, 99);

        }
        player.SetActive(false);
        //PanelVideoManager.playVideoMenang = true;
        panelObj.SetActive(true);
        gameObject.SetActive(false);


    }
}
