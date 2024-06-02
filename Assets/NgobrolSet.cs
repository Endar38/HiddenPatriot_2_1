using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class NgobrolSet : MonoBehaviour
{
    public LayerMask clickableLayer;
    public GameObject player;
    public GameObject[] enemyNgobrol;
    public GameObject[] areaNgobrol;
    public GameObject areaBahaya;
    public GameObject posPlayer;
    public GameObject teksTips;
    Jalan jlnPlayer;

    public int indexNgobrol;
    public bool mulaiNgobrol;
    public bool playerTerdeteksi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (indexNgobrol >= 2 )
        {
            areaBahaya.SetActive(true);
            areaNgobrol[0].SetActive(true);
            areaNgobrol[1].SetActive(true);

        }
        else
        {
            areaBahaya.SetActive(false);
            areaNgobrol[0].SetActive(false);
            areaNgobrol[1].SetActive(false);
        }
        if ( player.GetComponent<Jalan>().posPlayer == "Area2")
        {
            teksTips.SetActive(true);
        }
        
        if (playerTerdeteksi)
        {
            //Deteksiplayer();
            StartCoroutine(WayPlay());
            playerTerdeteksi = false;
        }
    }

    public void Deteksiplayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.zero, Mathf.Infinity, clickableLayer);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("tile"))
        {
            enemyNgobrol[0].GetComponent<pathEnemy>().posKoinJatuh = hit.collider.gameObject;
            enemyNgobrol[1].GetComponent<pathEnemy>().posKoinJatuh = hit.collider.gameObject;
            playerTerdeteksi=true;
        }
        
    }

    IEnumerator WayPlay()
    {
        yield return new WaitForSeconds(1f);
        enemyNgobrol[0].GetComponent<pathEnemy>().terkenaEfekKoin = true;
        enemyNgobrol[1].GetComponent<pathEnemy>().terkenaEfekKoin = true;
    }
}
