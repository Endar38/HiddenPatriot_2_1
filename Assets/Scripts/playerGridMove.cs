using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class playerGridMove : MonoBehaviour
{
    public LayerMask clickableLayer;

    public float speed = 5f; // Kecepatan pergerakan pemain
   // public List<Vector2> targetPoints; // Daftar titik-titik yang harus diikuti oleh pemain
    public int currentTargetIndex = 0; // Index dari titik target saat ini dalam daftar
    public bool isMoving = false; // Status pergerakan pemain



    public static bool jalan;
    public static bool jongkok;
    public static bool waktuBerdiri;
    public static bool waktuJongkok;



    public static int posX;
        public static int posY;


    public static bool batal;
    public static int posisiX;
    public static int posisiY;

    public GridBehavior gridBvior;


    public bool playerDiSmoke;

    //float scY = 1;
    //float scX = 0.5f;

    private void Start()
    {
        jongkok = false;
        transform.position = gridBvior.gridArray1[8, 0].transform.position;
        isMoving = false;
        batal = false;
        
    }
    void Update()
    {
        if (batal)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, clickableLayer);
            if (hit.collider != null)
            {
                if (hit.collider != null && hit.collider.CompareTag("tile"))
                {
                    // Mendapatkan komponen skrip dari objek yang dikenai ray
                    GridStart script = hit.collider.GetComponent<GridStart>();

                    if (script != null)
                    {
                        // Lakukan sesuatu dengan nilai dari skrip yang diinginkan
                        posisiX = script.x;
                        posisiY = script.y;
                        GridBehavior.rePos = true;
                        //Debug.Log("Nilai dari skrip di objek yang diklik: " + targetY);
                        batal = false;
                    }


                }
            }
        }






        if (Input.GetMouseButtonDown(0) )
        {

        }
       // transform.localScale = new Vector3(scX, scY, 1);
        if (waktuJongkok)
        {
            jongkok = true;
            speed = 0.85f;
            ActionManager.eksekusi = true;
            waktuJongkok = false;
        }

        if (waktuBerdiri)
        {
            jongkok = false;
            speed = 1.25f;
            ActionManager.eksekusi = true;
            waktuBerdiri = false;

        }

       // Debug.Log(speed);
        // Memulai pergerakan saat tombol Enter ditekan
        if (jalan && !isMoving)
        {
            
            currentTargetIndex = 0;
           
            isMoving = true;
            MoveToNextTarget(Jalan.waypointTiles);
            // GridBehavior.path.Reverse();
            //jongkok = false;
            jalan = false;
        }

        // Jika pemain dalam keadaan bergerak, perbarui pergerakan
        if (isMoving)
        {

            IsWalking(Jalan.waypointTiles);
        }
    }

    void MoveToNextTarget(List<GameObject> WayMove)
    {
        // Jika telah mencapai titik terakhir dalam daftar, berhenti pergerakan
        if (currentTargetIndex >= WayMove.Count - 1)
        {
          //  Debug.Log("Pemain telah mencapai titik akhir.");
            jalan = false;
            
            
            
            GridBehavior.path.Reverse();
            GridBehavior.path.Clear();

            if (isMoving)
            {
                isMoving = false; // Berhenti pergerakan
                ActionManager.eksekusi = true;
            }
            WayMove.Clear();
            
            currentTargetIndex = 0;
            return;
        }

        // Pindahkan ke titik target berikutnya dalam daftar
        currentTargetIndex++;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("SmokeEfek") && GetComponent<lempar>().efekSmoke)
        {
            playerDiSmoke = true;
        }
        if (coll.gameObject.CompareTag("Enemy"))
        {
            coll.gameObject.SetActive(false);
            PanelVideoManager.playVideoKalah = true;
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("SmokeEfek") )
        {
            playerDiSmoke = false;
        }
    }

    public void IsWalking(List<GameObject> WayMove)
    {
        Vector2 movementDirection = WayMove[currentTargetIndex].transform.position - transform.position;


        // Jika masih bergerak, tentukan arah muka berdasarkan arah pergerakan
        if (movementDirection.x > 0)
        {
            // Objek bergerak ke kanan, arah muka ke kanan
            GetComponent<Jalan>().arahMuka = 2;
        }
        else if (movementDirection.x < 0)
        {
            // Objek bergerak ke kiri, arah muka ke kiri
            GetComponent<Jalan>().arahMuka = 4;
        }
        else if (movementDirection.y > 0)
        {
            // Objek bergerak ke atas, arah muka ke atas
            GetComponent<Jalan>().arahMuka = 1;
        }
        else if (movementDirection.y < 0)
        {
            // Objek bergerak ke bawah, arah muka ke bawah
            GetComponent<Jalan>().arahMuka = 3;
        }
        else
        {
            GetComponent<Jalan>().posDiam = true;
        }
        // Pindahkan pemain menuju titik target
        transform.position = Vector2.MoveTowards(transform.position, WayMove[currentTargetIndex].transform.position, speed * Time.deltaTime);

        // Jika pemain telah mencapai titik target saat ini, pindahkan ke titik berikutnya
        if (Vector2.Distance(transform.position, WayMove[currentTargetIndex].transform.position) <= 0f)
        {
            MoveToNextTarget(WayMove);
        }
    }
}
