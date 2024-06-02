using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class klikEnemyManager : MonoBehaviour, IPointerClickHandler
{
    public LayerMask shootMask;
    public GameObject player;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (tembak.waktuPilihTembak || tembak.waktuPilihMelee)
        {
            RaycastHit2D hitEn = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, shootMask);
            if (hitEn.collider != null && hitEn.collider.gameObject.CompareTag("Enemy"))
            {
                if (tembak.waktuPilihTembak)
                {
                    player.GetComponent<tembak>().posEnemyTembak = hitEn.collider.GetComponent<Transform>();
                    tembak.waktuPilihTembak = false;
                }
                if (tembak.waktuPilihMelee)
                {
                    player.GetComponent<tembak>().posEnemyMelee = hitEn.collider.GetComponent<Transform>();
                    tembak.waktuPilihMelee = false;
                }
                PanelMapDrag.bolehDragMap = true;
                ActionManager.bolehPilih = true;


            }
            else
            {
                PanelMapDrag.bolehDragMap = true;
                ActionManager.bolehPilih = true;

                tembak.waktuPilihTembak = false;
            }
        }
    }
}

