using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVScript : MonoBehaviour
{
    public float fovAngle = 90f;
    public Transform fovPoint;
    public float range = 8;
    public LayerMask playerLayer;  // Assign the player layer in the Unity Editor
    public LayerMask obstacleLayer; // Assign the obstacle layer in the Unity Editor
    public Transform target;

    // Add this variable to control the rotation speed
    //public float rotationSpeed = 5f;

    public GameObject indukEnemy;

    // Kecepatan rotasi objek
    public float rotasiSpeed = 90f; // Sudut rotasi per detik

    // Variabel untuk menentukan arah menggunakan int
    public int arahHadap; // Defaultnya menghadap ke atas (0 derajat)

    //private bool rotateRight = false; // Menandakan arah rotasi
    //private Quaternion targetRotation; // Rotasi target


    public float rotateAngle = 90f; // Sudut rotasi

    public bool startClockwise = true; // Menentukan apakah rotasi awal searah jarum jam



    private int nilaiAwal = 1;
    private float waktuPerubahan = 1.5f;

    bool rotasiMulai;

    public float jarakEfekSmoke;
   

    public GameObject player;
    public GameObject posSmokeJatuh;

    public GameObject smokeLokasi;

    public bool efekSmokeBaru;

    bool sedangRotasi;

    void Start()
    {

    }
    void Update()
    {
        if (player.GetComponent<lempar>().efekSmoke && efekSmokeBaru == false)
        {
            smokeLokasi = posSmokeJatuh;
            efekSmokeBaru = true;
        }


        /*
        if (player.GetComponent<lempar>().efekSmoke)
        {
            if ((Vector2.Distance (smokeLokasi.transform.position, transform.position)) <= jarakEfekSmoke || (Vector2.Distance (smokeLokasi.transform.position, player.transform.position)) <= jarakEfekSmoke)
            {
                terkenaEfekSmoke = true;
            }
            else
            {
                terkenaEfekSmoke = false;
            }
        }
        else
        {
            terkenaEfekSmoke = false;
        }
        */
      
            Rotasi();
        
        
        

        if (target != null)
        {
            Vector2 dir = target.position - transform.position;
            float angle = Vector2.Angle(dir, fovPoint.up);

            // Use layer masks to ignore specific layers (e.g., obstacleLayer)
            LayerMask raycastLayerMask = playerLayer | obstacleLayer;

            int indexLayerObs1 = LayerMask.NameToLayer("Obstacle1");

            if (playerGridMove.jongkok)
            {
                raycastLayerMask = raycastLayerMask | (1 << indexLayerObs1);
            }
            else
            {
                raycastLayerMask = raycastLayerMask & ~(1 << indexLayerObs1);
            }



            RaycastHit2D hit = Physics2D.Raycast(fovPoint.position, dir, range, raycastLayerMask);




            if (angle < fovAngle / 2)
            {
                if (hit.collider != null && hit.collider.CompareTag("Player") && !GetComponentInParent<pathEnemy>().terkenaEfekSmoke && !player.GetComponent<playerGridMove>().playerDiSmoke)
                {
                    // WE SPOTTED THE PLAYER!

                    hit.collider.gameObject.SetActive(false);
                    PanelVideoManager.playVideoKalah = true;
                   // print("SEEN!");
                    Debug.DrawRay(fovPoint.position, dir.normalized * range, Color.red);

             
                }
                else
                {
                    
                    //print("we don't seen");
                    Debug.DrawRay(fovPoint.position, dir.normalized * range, Color.green);
                }
            }

        }
    }



    void Rotasi()
    {
        if (indukEnemy.GetComponent<pathEnemy>().rotasiBiasa == true)
        {
            arahHadap = indukEnemy.GetComponent<pathEnemy>().arahMuka;
           /* if (arahHadap > 0)
            {
                arahHadap = -arahHadap;
            }*/
        }
        

        if (indukEnemy.GetComponent<pathEnemy>().siagaPantau == true)
        {
            indukEnemy.GetComponent<pathEnemy>().rotasiBiasa = false;
            indukEnemy.GetComponent<pathEnemy>().stopSiagaPantau = false;
            rotasiSpeed = 300f;

            indukEnemy.GetComponent <pathEnemy>().isMoving = false;
            if (arahHadap > 0)
            {
                arahHadap = arahHadap *- 1;
            }

           
            nilaiAwal = 1;
            int i;
            if (arahHadap != -4 && arahHadap != 1)
            {
                rotasiMulai = true;
                i = 1;
                StartCoroutine(Rotate(i));
            }
            else if (arahHadap == -4 || arahHadap == 1)
            {
                rotasiMulai = true;
                i = 2;
                StartCoroutine(Rotate(i));
            }
            
            indukEnemy.GetComponent<pathEnemy>().siagaPantau = false;
        }
        else if (indukEnemy.GetComponent<pathEnemy>().stopSiagaPantau == true)
        {
            indukEnemy.GetComponent<pathEnemy>().rotasiBiasa = true;
            rotasiSpeed = 720f;
            rotasiMulai = false;
            nilaiAwal = 1;
            indukEnemy.GetComponent<pathEnemy>().stopSiagaPantau = false;


        }
        // Menghitung sudut rotasi berdasarkan nilai arah
        float targetRotation = 0f; // Defaultnya tidak berputar (0 derajat)

        // Mengonversi nilai arah negatif menjadi nilai positif yang sesuai
      //  if (arahHadap < 0)
        //    arahHadap = (arahHadap % 4 + 4) % 4;

        switch (arahHadap)
        {
            case -1: // Menghadap ke atas (0 derajat)
                targetRotation = 0f;
                break;
            case -2: // Menghadap ke kanan (90 derajat)
                targetRotation = -90f;
                break;
            case -3: // Menghadap ke bawah (180 derajat)
                targetRotation = -180f;
                break;
            case -4: // Menghadap ke kiri (270 derajat)
                targetRotation = -270f;
                break;
            case 1: // Menghadap ke atas (0 derajat)
                targetRotation = 0f;
                break;
            case 2: // Menghadap ke kanan (90 derajat)
                targetRotation = -90f;
                break;
            case 3: // Menghadap ke bawah (180 derajat)
                targetRotation = -180f;
                break;
            case 4: // Menghadap ke kiri (270 derajat)
                targetRotation = -270f;
                break;
            default:
                targetRotation = 0f; ;
                break;
        }

        // Menghitung perubahan sudut rotasi yang dibutuhkan
        float rotationAmount = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetRotation, rotasiSpeed * Time.deltaTime) - transform.eulerAngles.z;

        // Melakukan rotasi objek
        transform.Rotate(Vector3.forward, rotationAmount);
    }

    IEnumerator Rotate(int index)
    {

        if (sedangRotasi)
        {
            yield break;
        }

        indukEnemy.GetComponent<pathEnemy>().isMoving = false;
        indukEnemy.GetComponent<pathEnemy>().enemyJalan = false;
        sedangRotasi = true;
        while (rotasiMulai)
        {
            
            // Tunggu selama waktuPerubahan
            //Debug.Log(arahHadap);
            // Ubah nilai sesuai kebutuhan (misalnya naik turun secara bergantian)
            
            if (index == 1)
            {
                if (nilaiAwal == 1)
                {

                    arahHadap = arahHadap - 1;


                    nilaiAwal = 2;
                }
                else
                {
                    arahHadap = arahHadap + 1;
                    nilaiAwal = 1;
                }
            }
            else if (index == 2)
            {
                if (nilaiAwal == 1)
                {

                    arahHadap = arahHadap + 3;


                    nilaiAwal = 2;
                }
                else
                {
                    arahHadap = arahHadap - 3;
                    nilaiAwal = 1;
                }
            }
            indukEnemy.GetComponent<pathEnemy>().arahMuka = arahHadap;
            // Lakukan sesuatu dengan nilai yang telah diubah (contoh: cetak ke konsol)
            yield return new WaitForSeconds(waktuPerubahan);
            //Debug.Log(index + "Nilai telah diubah menjadi: " + nilaiAwal);
        }

        sedangRotasi = false;
    }
}
