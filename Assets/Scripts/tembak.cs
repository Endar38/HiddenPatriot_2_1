using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tembak : MonoBehaviour
{

    public LayerMask shootMask;
    public LayerMask enemyMask;
   
    public float maxShoot = 5;
    public float maxMelee = 1;
    public static bool waktuTembak;
    public static bool waktuMelee;
    public static Vector2 arahTembak;
    public static Vector2 arahMelee;
    public static bool waktuPilihTembak;
    public static bool waktuPilihMelee;
    public Transform posEnemyTembak;
    public Transform posEnemyMelee;

    public float jarakEfekTembak = 7;


    List<GameObject> enemys = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        enemys = GetComponent<lempar>().enemys;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && (waktuPilihTembak || waktuPilihMelee))
        {
            RaycastHit2D hitEn = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, enemyMask);
            if(hitEn.collider != null && hitEn.collider.gameObject.CompareTag("enemyFull"))
            {
                if (waktuPilihTembak)
                {
                    posEnemyTembak = hitEn.collider.gameObject.GetComponent<Transform>().parent;
                    waktuPilihTembak = false;
                }
                if (waktuPilihMelee)
                {
                    posEnemyMelee = hitEn.collider.GetComponent<Transform>();
                    waktuPilihMelee = false;
                }
                PanelMapDrag.bolehDragMap = true;
                ActionManager.bolehPilih = true;

                
            }
            else
            {
                PanelMapDrag.bolehDragMap = true;
                ActionManager.bolehPilih = true;

                waktuPilihTembak = false;
            }
        }
        
        if (waktuTembak)
        {
            try
            {
                int indexLayerObs1 = LayerMask.NameToLayer("Obstacle1");

                if (playerGridMove.jongkok)
                {
                    shootMask = shootMask | (1 << indexLayerObs1);
                }
                else
                {
                    shootMask = shootMask & ~(1 << indexLayerObs1);
                }
                arahTembak = new Vector2(posEnemyTembak.position.x, posEnemyTembak.position.y);
                Vector2 direct = (arahTembak - (Vector2)transform.position).normalized;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, direct, maxShoot, shootMask);
                if (hit.collider != null && hit.collider.gameObject.CompareTag("enemyFull"))
                {
                    PanelVideoManager.playVideoTembak = true;
                    hit.collider.transform.parent.GetComponent<skripItemDrop>().isDead = true;
                    
                    
                }
                else
                {
                    PanelVideoManager.miss = true;
                }
                EfekTembak();
                ActionManager.pistolSisa--;
                ActionManager.eksekusi = true;
                posEnemyTembak = null;
                waktuTembak = false;
            }
            catch
            {
                PanelVideoManager.miss = true;
                EfekTembak();
                ActionManager.pistolSisa--;
                ActionManager.eksekusi = true;
                waktuTembak = false;
            }
        }

        if (waktuMelee && arahMelee != null)
        {
            try
            {
                arahMelee = new Vector2(posEnemyMelee.position.x, posEnemyMelee.position.y);
                Vector2 direct = (arahMelee - (Vector2)transform.position).normalized;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, direct, maxMelee, shootMask);
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
                {
                    hit.collider.gameObject.SetActive(false);
                }
                ActionManager.eksekusi = true;
                posEnemyMelee = null;
                waktuMelee = false;
            }
            catch
            {

            }
        }
        else if (waktuMelee)
        {
            ActionManager.eksekusi = true;
            waktuMelee = false;
        }

    }

    void EfekTembak()
    {
        foreach (GameObject enemy in enemys)
        {
            if (Vector2.Distance (transform.position, enemy.transform.position) <= jarakEfekTembak && !enemy.GetComponent<pathEnemy>().terkenaEfekTembak)
            {
                enemy.GetComponent<pathEnemy>().terkenaEfekTembak = true;
            }
        }
    }
}
