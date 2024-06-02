using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Burst;
using UnityEngine;

public class pathEnemy : MonoBehaviour
{

   


    public LayerMask clickableLayer;

    public int idEnemy;

    public int starEnemyX;
    public int starEnemyY;
    public int endEnemyX;
    public int endEnemyY;
    public float speed = 5f;
    public List<GameObject> pointPatrol = new List<GameObject>();

    public List<GameObject> patrolEnemyPath = new List<GameObject>();

    public bool rotEnemy;

    public bool isMoving = false;

    private int currentEnemyTargetIndex = 0;

    public bool enemyJalan;
    public bool enemyJalan2;

    public bool takJalan;

    public int indexPoint = 0;



    public bool siagaPantau;
    public bool stopSiagaPantau;


    public int arahMuka = 1; // 1: atas, 2: kanan, 3: bawah, 4: kiri
    private Vector2 previousPosition; //

    public GameObject posKoinJatuh;
    public GridStart grid_KoinJatuh;

    public GameObject player;

    bool posDiam;

    public bool rotasiBiasa;

    public bool terkenaEfekKoin;
    public bool terkenaEfekSmoke;

    public float jarakEfekKoin = 4f;

    public bool jalanKeKoin;
    public bool jalanKeluarSmoke;

    public int pointBVior = 1;
    public int maxBVior = 3;
    public bool gantiBvior;

    public bool lagiJalanEfekKoin;
    public bool lagiJalanEfekSmoke;

    bool pindahPatrol;
    bool lagiPatrol;
    public bool menoleh;
    public bool sampaiLokasi;
    bool langsungJalan;

    public bool sudahCekKoin;

    public Animator animEmy;

    public bool pergiDariSmoke;
    public bool belumKeluarSmoke;

    public bool terkenaEfekTembak;
    public bool lagiEfekTembak;

    public bool sampai_Toleh;

    public int mukaAwalNoleh;

    bool nungguJalan;

    public GridBehavior gridBvior;

    GameObject posAmanSmoke;
    [Space]
    [Header("Behavior Enemy")]
    
    //public int[] indexPoint_0;
    //public bool[] sampaiLokasi_0;
    //public bool[] enemyJalan2_0;
    //public bool[] lagiPatrol_0;
    public bool[] sampai_Toleh_0;
    public int[] mukaAwalNoleh_0;
   // public bool[] langsungJalan_0;
    //public bool[] pindahPatrol_0;

    bool prosesBVior;

    public GridBehavior setWay;

    EnemySet1 enemySet1;

    List<GameObject> titikPatrol = new List<GameObject>();
    public List<List<GameObject>> titikPatrol_Jln2 = new List<List<GameObject>>();

    public string posEnm;

    GameObject[,] wayEnm1;
    public int indexJln2;

    bool sudahDeteksiWay2;

    public bool bolehGantiBvior = true;

    // Start is called before the first frame update

    void Start()
    {
        sudahDeteksiWay2 = false;
        posEnm = PosEnm();
        rotasiBiasa = true;
        stopSiagaPantau = true;

        if (posEnm == "Area1")
        {
            wayEnm1 = setWay.gridArray1;
        }
        else if(posEnm == "Area2")
        {
            wayEnm1 = setWay.gridArray2;
        }
        else if (posEnm == "Area3")
        {
            wayEnm1 = setWay.gridArray3;
        }
        else if (posEnm == "Area4")
        {
            wayEnm1 = setWay.gridArray4;
        }
        //takJalan = true;

        gantiBvior = true;
        previousPosition = transform.position;
        enemySet1 = transform.parent.GetComponent<EnemySet1>();
        
        indexPoint = -2;

        
    }

    // Update is called once per frame
    void Update()
    {

     

        
            if (!lagiJalanEfekKoin && !lagiJalanEfekSmoke && !lagiEfekTembak)
            {

                if (pointBVior == 0 && gantiBvior)
            {
                enemyJalan2 = false;
                BViorSet(maxBVior + 1);
                if (!sudahDeteksiWay2)
                {
                    enemySet1.PointSet(out titikPatrol, pointPatrol, out titikPatrol_Jln2, wayEnm1, posEnm);
                    sudahDeteksiWay2 = true;
                }
                indexJln2 = 3;
                enemySet1.SetPatrol(titikPatrol[6], out menoleh, out stopSiagaPantau, TilePosEnemy(), patrolEnemyPath, out takJalan, out enemyJalan, wayEnm1);
                gantiBvior = false;
            }


                if (pointBVior == 1 && gantiBvior)
                {
                    //indexPoint = 0;
                enemyJalan2 = false;
                BViorSet(pointBVior);
                langsungJalan = true;
                pindahPatrol = true;
                

                    gantiBvior = false;


                }
                if (pointBVior == 2 && gantiBvior)
                {
                //indexPoint = indexPoint_0[1];
                enemyJalan2 = false;
                BViorSet(pointBVior);
                langsungJalan = true;
                pindahPatrol = true;

                gantiBvior = false;

                }

                if (pointBVior == 3 && gantiBvior)
                {
                //indexPoint = indexPoint_0[2];
                enemyJalan2 = false;
                BViorSet(pointBVior);
                langsungJalan = true;
                pindahPatrol = true;

                gantiBvior = false;
                }
            }
        
     
        if (player.GetComponent<lempar>().efekKoin && posKoinJatuh != null)
        {
            //Debug.Log(Vector2.Distance(posKoinJatuh.transform.position, transform.position));
            if (Vector2.Distance(posKoinJatuh.transform.position, transform.position) <= jarakEfekKoin && !sudahCekKoin)
            {
                
                terkenaEfekKoin = true;
                // Debug.Log("mlaku");
                player.GetComponent<lempar>().cekKeKoin++;
                sudahCekKoin = true;
            }
            else if(!sudahCekKoin)
            {
                player.GetComponent<lempar>().cekKeKoin++;
                sudahCekKoin = true;
            }
            
        }

        if (terkenaEfekTembak)
        {
            if (!lagiJalanEfekKoin && !lagiJalanEfekSmoke)
            {
                enemyJalan2 = false;
                menoleh = true;
                lagiEfekTembak = true;
                
            }
            Debug.Log("jnjn");
            terkenaEfekTembak = false;
        }
       
        if (player.GetComponent<lempar>().efekSmoke)
        {

            
            if (!CekPosAmanSmoke() && !pergiDariSmoke && !lagiJalanEfekSmoke)
            {
                
                belumKeluarSmoke = true;
                pergiDariSmoke = true;
                jalanKeluarSmoke = false;
            }
        }

        if (pergiDariSmoke && !jalanKeluarSmoke)
        {
            stopSiagaPantau = true;
            enemyJalan = false;
            gantiBvior = false;
            isMoving = false;
            lagiJalanEfekSmoke = false;
            jalanKeluarSmoke = true;
            //takJalan = true;
            posAmanSmoke = GetComponent<WaySmokeManager>().CariAreaLuarSmoke(transform.position, wayEnm1);
            pergiDariSmoke = false;
        }


        if (terkenaEfekKoin && !lagiJalanEfekSmoke && CekPosAmanSmoke() && !terkenaEfekSmoke && !belumKeluarSmoke)
        {
            stopSiagaPantau = true;
            enemyJalan = false;
            gantiBvior = false;
            isMoving = false;
            jalanKeKoin = true;
            //takJalan = true;
            terkenaEfekKoin = false;

            
        }

        DeteksiMuka();
        //Debug.Log(arahMuka);
        if (menoleh && !terkenaEfekKoin && !lagiJalanEfekSmoke && !lagiJalanEfekKoin)
        {
            siagaPantau = true;
            enemyJalan = false;
            isMoving = false;
           // patrolEnemyPath.Clear();
            menoleh = false;
            
        }
        

        if (pindahPatrol)
        {

            stopSiagaPantau = true;
            isMoving = false;
            indexPoint = pointBVior * 2 - 2;
            
            enemyJalan = false;
            enemyJalan2 = false;
            //takJalan = true;
            /*if (langsungJalan)
            {
                enemyJalan2 = false;
                lagiPatrol = true;
                langsungJalan = false;
            }*/
            
                indexJln2 = indexPoint / 2;
            
            
            Patrol();
            
            pindahPatrol = false;
           
            
        }
        //if (lagiPatrol)
       // {
            
         //   lagiPatrol = false;
            
          
       // }

        

        if (jalanKeKoin && CekPosAmanSmoke() && !lagiJalanEfekSmoke)
        {
            if (player.GetComponent<lempar>().efekSmoke)
            {
                if (CekPosAmanSmoke() && !terkenaEfekSmoke && !belumKeluarSmoke && !isMoving)
                {
                    enemyJalan2 = false;
                    sampaiLokasi = false;
                    enemySet1.SetPatrol(posKoinJatuh, out menoleh, out stopSiagaPantau, TilePosEnemy(), patrolEnemyPath, out takJalan, out enemyJalan, wayEnm1);
                    lagiJalanEfekKoin = true;
                    jalanKeKoin = false;
                }
                else
                {
                    lagiJalanEfekKoin = false;
                }
            }
            else
            {
                sampaiLokasi = false;
                enemyJalan2 = false;
                enemySet1.SetPatrol(posKoinJatuh, out menoleh, out stopSiagaPantau, TilePosEnemy(), patrolEnemyPath, out takJalan, out enemyJalan, wayEnm1);
                lagiJalanEfekKoin = true;
                jalanKeKoin = false;
            }
            

        }
        if (lagiJalanEfekSmoke)
        {
            if (posAmanSmoke.GetComponent<GridStart>().terkenaGranat)
            {
                lagiJalanEfekSmoke = false;
            }
        }
        else if (lagiJalanEfekKoin && !isMoving)
        {
            EnemyPatrol();
        }
        if (jalanKeluarSmoke && !lagiJalanEfekSmoke && !lagiJalanEfekKoin)
        {

            sampaiLokasi = false;
            enemyJalan2 = false;
            enemySet1.SetPatrol(posAmanSmoke, out menoleh, out stopSiagaPantau, TilePosEnemy(), patrolEnemyPath, out takJalan, out enemyJalan, wayEnm1);
            lagiJalanEfekSmoke = true;
            jalanKeluarSmoke = false;
        }

        if (enemyJalan && !isMoving)
        {
            //Debug.Log("mantapp");
            EnemyPatrol();
            enemyJalan = false;
        }
      
       // Debug.Log(isMoving);
       if (patrolEnemyPath.Count == 1)
        {
            patrolEnemyPath.Add(patrolEnemyPath[0]);
        }

        if (isMoving && patrolEnemyPath.Count >= 2)
        {
            if (lagiJalanEfekKoin && !CekPosAmanSmoke())
            {
                isMoving = false;
                enemyJalan2 = false;
                gantiBvior = false;
                lagiJalanEfekKoin = false;
            }
            stopSiagaPantau = true;
            if (enemyJalan2 == false)
            {
                GerakJalan(patrolEnemyPath);
            }
            else
            {
                //Debug.Log(titikPatrol_Jln2.Count);
                GerakJalan(titikPatrol_Jln2[indexJln2]);
            }
            
            

        }else if (patrolEnemyPath.Count < 2)
        {
            isMoving = false;
            if (lagiJalanEfekSmoke)
            {
                lagiJalanEfekSmoke = false;
            }
            else if ( lagiJalanEfekKoin)
            {
                lagiJalanEfekKoin = false;
            }
        }
    }
    

    GameObject TilePosEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, clickableLayer);
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

    bool CekPosAmanSmoke()
    {
        GameObject posEnemy = TilePosEnemy();

        if (posEnemy != null && !posEnemy.GetComponent<GridStart>().terkenaGranat)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void EnemyPatrol()
    {
        currentEnemyTargetIndex = 0;
        isMoving = true;
       // MoveToNextTarget();
    }

    void MoveToNextTarget(List<GameObject> ruteJln)
    {
        // Jika telah mencapai titik terakhir dalam daftar, berhenti pergerakan

        if (currentEnemyTargetIndex >= ruteJln.Count - 1)
        {

            isMoving = false;
            if (!enemyJalan2 && !jalanKeKoin && !lagiJalanEfekKoin && !lagiJalanEfekSmoke)
            {
                enemyJalan2 = true;
                enemyJalan = true;
              //  Debug.Log("woii");
               // Patrol();
                

            }

            if (lagiJalanEfekKoin && !lagiJalanEfekSmoke)
            {
                enemyJalan2 = false;
                menoleh = true;
                gantiBvior = false;
                lagiJalanEfekKoin = false;
            }
            

            else if (lagiJalanEfekSmoke && !lagiJalanEfekKoin)
            {
                enemyJalan = false;
                enemyJalan2 = false;
                gantiBvior = false;
                if (CekPosAmanSmoke())
                {

                    arahMuka = ArahMukaBalik(arahMuka);
                    if (!jalanKeKoin)
                    {
                        menoleh = true;
                    }
                    
                    belumKeluarSmoke = false;
                    jalanKeluarSmoke = false;
                    lagiJalanEfekSmoke = false;
                }
                lagiJalanEfekSmoke = false;


            }
            else if (sampai_Toleh)
            {
                arahMuka = mukaAwalNoleh;
                menoleh = true;
                sampai_Toleh = false;
            }
            else if(enemyJalan2)
            {
                if (pointBVior > 0)
                {
                    enemyJalan = true;
                    patrolEnemyPath.Reverse();
                }
                else
                {
                    arahMuka = mukaAwalNoleh;
                }
                
            }
            //  Debug.Log("Pemain telah mencapai titik akhir.");
             // Berhenti pergerakan
             
            
          
            
            sampaiLokasi = true;
            //ActionManager.eksekusi = true;
            return;
        }

        // Pindahkan ke titik target berikutnya dalam daftar
        currentEnemyTargetIndex++;
    }

    int ArahMukaBalik(int arahMukaA)
    {
        if (arahMukaA >= 0)
        {
            arahMukaA = arahMukaA * -1;
        }

        switch (arahMukaA)
        {
            case -1:
                return -3;
            case -2:
                return -4;
            case -3:
                return -1;
            case -4:
                return -2;
            default:
                return -1;
        }
    }

   

    void DeteksiMuka()
    {// Hitung perbedaan posisi objek saat ini dengan posisi sebelumnya
        Vector2 movementDirection = (Vector2)transform.position - previousPosition;

        // Tentukan arah hadap muka berdasarkan pergerakan objek
        if (movementDirection.x <= 0f && movementDirection.x >= 0f && movementDirection.y <= 0f && movementDirection.y >= 0f)
        {
            // Jika pergerakan kurang dari threshold, set nilai arah muka menjadi negatif
            posDiam = true;
            
        }
        if (posDiam && arahMuka > 0)
        {
            arahMuka = arahMuka * - 1;
            posDiam = false;
        }

        // Simpan posisi sebelumnya untuk pembaruan berikutnya
        previousPosition = transform.position;
    }

    



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("SmokeEfek"))
        {
            terkenaEfekSmoke = true;

        }

    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("SmokeEfek"))
        {
            terkenaEfekSmoke = false;
        }
    }


    void BViorSet(int pointBViorEnm)
    {

        //sampaiLokasi = sampaiLokasi_0[pointBViorEnm - 1];
        //enemyJalan2 = enemyJalan2_0[pointBViorEnm - 1];
       // lagiPatrol = lagiPatrol_0[pointBViorEnm - 1];
        sampai_Toleh = sampai_Toleh_0[pointBViorEnm - 1];
        mukaAwalNoleh = mukaAwalNoleh_0[pointBViorEnm - 1];
        //langsungJalan = langsungJalan_0[pointBViorEnm - 1];
        //pindahPatrol = pindahPatrol_0[pointBViorEnm - 1];

    }

    /*public async Task ChangeVariableBVior(int pointBViorEnm)
    {
        await Task.Run(() =>
        {
            prosesBVior = true;
            //sampaiLokasi = sampaiLokasi_0[pointBViorEnm - 1];
            //enemyJalan2 = enemyJalan2_0[pointBViorEnm - 1];
            lagiPatrol = lagiPatrol_0[pointBViorEnm - 1];
            sampai_Toleh = sampai_Toleh_0[pointBViorEnm - 1];
            mukaAwalNoleh = mukaAwalNoleh_0[pointBViorEnm - 1];
            //langsungJalan = langsungJalan_0[pointBViorEnm - 1];
            //pindahPatrol = pindahPatrol_0[pointBViorEnm - 1];
        });
    }*/

    void Patrol()
    {
        stopSiagaPantau = true;
        isMoving = false;
        enemyJalan = false;
        if (!enemyJalan2 && !jalanKeKoin && !lagiEfekTembak)
        {
            Debug.Log(titikPatrol.Count);
            if (!sudahDeteksiWay2)
            {
                enemySet1.PointSet(out titikPatrol, pointPatrol, out titikPatrol_Jln2, wayEnm1, posEnm);
                sudahDeteksiWay2 = true;
            }
            enemySet1.SetPatrol(titikPatrol[indexPoint], out menoleh, out stopSiagaPantau, TilePosEnemy(), patrolEnemyPath, out takJalan, out enemyJalan, wayEnm1);

        }
        if (enemyJalan2 && !jalanKeKoin && !lagiEfekTembak)
        {
            
           // patrolEnemyPath = titikPatrol_Jln2[pointBVior - 1];
           // enemyJalan = true;
            //Debug.Log("sss");
            //enemySet1.SetPatrol(titikPatrol[indexPoint + 1], out menoleh, out stopSiagaPantau, titikPatrol[indexPoint], patrolEnemyPath, out takJalan, out enemyJalan);
            
        }
    }

    
    void GerakJalan(List<GameObject> rute)
    {
        if (rute != null)
        {
            Vector2 movementDirection = rute[currentEnemyTargetIndex].transform.position - transform.position;

            // Jika masih bergerak, tentukan arah muka berdasarkan arah pergerakan
            enemySet1.setMuka(movementDirection, out arahMuka);

            // Pindahkan pemain menuju titik target
            transform.position = Vector2.MoveTowards(transform.position, rute[currentEnemyTargetIndex].transform.position, speed * Time.deltaTime);



            // Jika pemain telah mencapai titik target saat ini, pindahkan ke titik berikutnya
            if (Vector2.Distance(transform.position, rute[currentEnemyTargetIndex].transform.position) <= 0f)
            {
                MoveToNextTarget(rute);
            }
        }
        
    }

    string PosEnm()
    {
        return TilePosEnemy().GetComponent<GridStart>().area;
    }
 



}



