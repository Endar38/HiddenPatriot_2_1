using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveControl : MonoBehaviour
{
    public GridBehavior scriptAM;
    public GameObject[] targetTile;
    public List<GameObject> targets = new List<GameObject>();
    public bool isAM;
    public LayerMask clickableLayer;
    public List<GameObject> waypointTilesAuto = new List<GameObject>();
    public bool waktuAutoMove;
    public GameObject Kamera;

    public GameObject Kanvas;

    public int maxTarget;

    public GridBehavior gridBv;
    public Jalan jalan1;
    public bool balikAM;

    public int idKunci1;
    public int idKunci2;
    public int idMaxAM;

    Collider2D colld;

    lempar setBviorEnm;
    public ActionManager am;


    // Start is called before the first frame update
    void Start()
    {
        targetTile = new GameObject[maxTarget];
       // StartCoroutine(ConvToArray());
       setBviorEnm = GetComponent<lempar>();
        colld = GetComponent<Collider2D>();
        waktuAutoMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(ActionManager.waktuPilih);
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("tileAutoMove"))
        {
            tileAutoMoveSet tileAMSet = coll.gameObject.GetComponent<tileAutoMoveSet>();
            if (tileAMSet.idTile <= idMaxAM)
            {
                StartCoroutine(AutoMovePlay(targetTile[(tileAMSet.idTile)]));
            }
            else if (tileAMSet.idTile == idKunci1 && ActionManager.kunci > 0)
            {
                StartCoroutine(AutoMovePlay(targetTile[(tileAMSet.idTile)]));
                Destroy(coll.gameObject);
            }
            else if (tileAMSet.idTile == idKunci2 && balikAM)
            {
                StartCoroutine(AutoMovePlay(targetTile[(tileAMSet.idTile)]));
            }
        }
    }

    IEnumerator AutoMovePlay(GameObject target)
    {
        if (isAM)
        {
            yield break;
        }
        isAM = true;

        colld.enabled = false;
        Kanvas.SetActive(false);
        yield return new WaitUntil(() => waktuAutoMove);
        
        
        yield return new WaitForSeconds(0.5f);

        waktuAutoMove = false;
        WayAuto(target, waypointTilesAuto);
        waypointTilesAuto.Reverse();
        GetComponent<playerGridMove>().isMoving = false;

        GetComponent<playerGridMove>().currentTargetIndex = 0;
        //GetComponent<playerGridMove>().IsWalking(waypointTilesAuto);
        yield return new WaitForSeconds(1);
        
        setBviorEnm.SwitchBVior(true);
        am.indexWaktuBVior = 0;
        Kamera.GetComponent<zoomKontrol>().ZoomIn();
        while (GetComponent<playerGridMove>().currentTargetIndex < waypointTilesAuto.Count)
        {
            GetComponent<playerGridMove>().IsWalking(waypointTilesAuto);
            yield return null;
        }

        Kamera.GetComponent<zoomKontrol>().ZoomOut();
        Kanvas.SetActive (true);
        yield return new WaitForSeconds(1f);
        colld.enabled = true;
        isAM = false;

    }
    public void WayAuto(GameObject pointPatrol, List<GameObject> wayMove)
    {

        string pos = GetComponent<Jalan>().PosPlayer();
        GameObject[,] wayArray;
        if (pos == "Area1")
        {
            wayArray = gridBv.gridArray1;
        }
        else if (pos == "Area2")
        {
            wayArray = gridBv.gridArray2;
        }
        else if (pos == "Area3")
        {
            wayArray = gridBv.gridArray3;
        }
        else
        {
            wayArray = gridBv.gridArray4;
        }

        GameObject hit = TilePosEnemy(gameObject);
        

            GridStart script = hit.GetComponent<GridStart>();
            GridStart script2 = pointPatrol.GetComponent<GridStart>();

        if (script != null)
        {

            scriptAM.SetDistance(script.x, script.y, wayArray);
            scriptAM.SetPath(script2.x, script2.y, wayMove, wayArray);
            // Lakukan sesuatu dengan nilai dari skrip yang diinginkan

            // GridBehavior.rePos = true;
            //Debug.Log("Nilai dari skrip di objek yang diklik: " + targetY);

        }



        
    }

    GameObject TilePosEnemy(GameObject player)
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.zero, Mathf.Infinity, clickableLayer);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("tile"))
        {
            GameObject PosEnemy = hit.collider.gameObject;
            return PosEnemy;
        }
        else
        {
            return null;
        }

    }

    IEnumerator ConvToArray()
    {
        yield return new WaitUntil(() => targets.Count >= maxTarget);

        targetTile = targets.ToArray();
    }
}
