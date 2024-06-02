using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Jalan : MonoBehaviour
{

    public LayerMask clickableLayer;
    public LayerMask playerLayer;
    public static int maxWaypoints = 10; // Jumlah maksimal waypoint

    public static List<GameObject> waypointTiles = new List<GameObject>(); // Untuk menyimpan petak-petak yang menjadi waypoint
    public Color selectedColor = Color.green; // Warna untuk petak yang terkena drag, diatur langsung di skrip

    public static int targetX;
    public static int targetY;

    public static bool bisaDeteksiJalur;
    public static bool mulaiPilihJalan;
    public static GameObject lastWaypoint;



    public static bool waktuPilihJalan;
    private Color defaultColor; // Warna default tile

    public float transparencyValue = 1;

    public GameObject tile1;


    public int arahMuka = 1; // 1: atas, 2: kanan, 3: bawah, 4: kiri
    private Vector2 previousPosition; //

    public bool posDiam;

    public int arahHadap; // Defaultnya menghadap ke atas (0 derajat)

    public Animator anim;

    //public static bool jongkok;
    bool startWalk;
    bool endWalk;

    public string posPlayer;

    public GridBehavior gridBvior;

    public GameObject footprintPrefab; // Prefab untuk telapak kaki
    private List<GameObject> footprints = new List<GameObject>();

    void Start()
    {
        defaultColor = tile1.GetComponent<TileHighlight>().originalColor;
        // selectedColor.a = 1;
        playerGridMove.jongkok = false;

       
        previousPosition = transform.position;
    }
    void Update()
    {


        AnimasiRotasiPlayer();


        DeteksiMuka();

        if (Input.GetMouseButtonDown(0) && waktuPilihJalan) // Ketika mouse ditekan
        {
           
           // ClearWaypoints(); // Hapus semua waypoint sebelumnya
            RaycastHit2D hitTile = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, clickableLayer);
            RaycastHit2D hitStart = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, clickableLayer);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, playerLayer);
            if (hitTile.collider != null && hitStart.collider != null)
            {
                /*SpriteRenderer firstTileRenderer = hitTile.collider.gameObject.GetComponent<SpriteRenderer>();
                if (firstTileRenderer != null)
                {
                    
                }*/
                if (hitTile.collider.gameObject == hitStart.collider.gameObject)
                {
                    mulaiPilihJalan = true;
                }
            }else if(hit.collider != null)
            {
                mulaiPilihJalan = true;
            }

        }

        if (Input.GetMouseButton(0) && waypointTiles.Count < maxWaypoints && mulaiPilihJalan) // Ketika mouse digeser dan jumlah waypoint masih kurang dari maksimal
        {
            
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, clickableLayer);
            if (hit.collider != null)
            {
                GameObject tile = hit.collider.gameObject;
                if (tile.GetComponent<GridStart>().area == posPlayer)
                {
                    AddWaypointTile(tile);
                }
                
            }
        }


        if (Input.GetMouseButtonUp(0) && mulaiPilihJalan && waypointTiles.Count > 1)
        {
  
            ActionManager.bolehPilih = true;
            ClearWaypoints();
            PanelMapDrag.bolehDragMap = true;
            mulaiPilihJalan = false;
            waktuPilihJalan = false;
        }

        if (bisaDeteksiJalur)
        {
            setWyAuto();

        }
        
     //   Debug.Log(playerGridMove.jalan);
    }

    void ClearWaypoints()
    {

        foreach (GameObject tile in waypointTiles)
        {
            tile.GetComponent<TileHighlight>().balikWarna = true;
            SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.color = defaultColor; // Kembalikan warna tile ke warna aslinya
            }
        }
        foreach (GameObject footprint in footprints)
        {
            Destroy(footprint); // Hapus telapak kaki
        }
        footprints.Clear();
        //waypointTiles.Clear();
        // waypointTiles.Clear(); // Hapus semua waypoint dari list
    }

    void AddWaypointTile(GameObject tile)
    {
        if (!waypointTiles.Contains(tile))
        {
            // Cek apakah petak sebelumnya ada
            if (waypointTiles.Count > 0)
            {
                GameObject lastTile = waypointTiles[waypointTiles.Count - 1];
                // Cek apakah tile dan lastTile berada di posisi yang valid
                if (Mathf.Abs(tile.transform.position.x - lastTile.transform.position.x) < 1.01f && Mathf.Abs(tile.transform.position.y - lastTile.transform.position.y) < 1.01f)
                {
                    // Cek apakah petak-petak berdekatan secara horizontal atau vertikal
                    if (Mathf.Abs(tile.transform.position.x - lastTile.transform.position.x) < 0.01f && Mathf.Abs(tile.transform.position.y - lastTile.transform.position.y) < 1.01f)
                    {

                        /*
                        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();

                        tile.GetComponent<TileHighlight>().balikWarna = false;
                        if (renderer != null)
                        {
                            renderer.color = selectedColor; // Ubah warna petak menjadi hijau
                            Color newColor = renderer.color;
                            newColor.a = transparencyValue;
                            renderer.color = newColor;
                        }*/
                        waypointTiles.Add(tile); // Tambahkan tile ke dalam daftar waypoint

                        if (waypointTiles.Count > 1)
                        {
                            InisiasiJejak(tile, waypointTiles[waypointTiles.Count - 2]);
                        }
                    }
                    else if (Mathf.Abs(tile.transform.position.y - lastTile.transform.position.y) < 0.01f && Mathf.Abs(tile.transform.position.x - lastTile.transform.position.x) < 1.01f)
                    {
                        /*
                        tile.GetComponent<TileHighlight>().balikWarna = false;
                        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
                        if (renderer != null)
                        {
                            renderer.color = selectedColor; // Ubah warna petak menjadi hijau
                            Color newColor = renderer.color;
                            newColor.a = transparencyValue;
                            renderer.color = newColor;
                        }*/
                        waypointTiles.Add(tile); // Tambahkan tile ke dalam daftar waypoint

                        if (waypointTiles.Count > 1)
                        {
                            InisiasiJejak(tile, waypointTiles[waypointTiles.Count - 2]);
                        }
                    }
                    else // Jika petak yang dipilih tidak valid (bersebrangan atau diagonal), panggil metode jalur waypoint otomatis
                    {
                        lastWaypoint = waypointTiles[waypointTiles.Count - 1];

                        // Panggil metode untuk mendapatkan daftar waypoint dari method yang sudah ada
                        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, clickableLayer);
                        if (hit.collider != null)
                        {
                            if (hit.collider != null && hit.collider.CompareTag("tile"))
                            {
                                // Mendapatkan komponen skrip dari objek yang dikenai ray
                                GridStart script = hit.collider.GetComponent<GridStart>();

                                if (script != null)
                                {
                                    // Lakukan sesuatu dengan nilai dari skrip yang diinginkan
                                    targetX = script.x;
                                    targetY = script.y;
                                   
                                    bisaDeteksiJalur = true;
                                }


                            }
                        }


                        // Ubah warna petak menjadi hijau

                    }
                }
                else // Jika petak yang dipilih tidak valid (berseberangan), panggil metode jalur waypoint otomatis
                {
                    lastWaypoint = waypointTiles[waypointTiles.Count - 1];

                    // Panggil metode untuk mendapatkan daftar waypoint dari method yang sudah ada
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, clickableLayer);
                    if (hit.collider != null)
                    {
                        if (hit.collider != null && hit.collider.CompareTag("tile"))
                        {
                            // Mendapatkan komponen skrip dari objek yang dikenai ray
                            GridStart script = hit.collider.GetComponent<GridStart>();

                            if (script != null)
                            {
                                // Lakukan sesuatu dengan nilai dari skrip yang diinginkan
                                targetX = script.x;
                                targetY = script.y;
                                //Debug.Log("Nilai dari skrip di objek yang diklik: " + targetY);

                                bisaDeteksiJalur = true;
                            }


                        }
                    }
                }
            }
            else
            {
               /* tile.GetComponent<TileHighlight>().balikWarna = false;
                SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    renderer.color = selectedColor; // Ubah warna petak menjadi hijau
                    Color newColor = renderer.color;
                    newColor.a = transparencyValue;
                    renderer.color = newColor;
                }*/
                waypointTiles.Add(tile); // Tambahkan tile ke dalam daftar waypoint

                if (waypointTiles.Count > 1)
                {
                    InisiasiJejak(tile, waypointTiles[waypointTiles.Count - 2]);
                }
            }
        }
        //ColorSet();
    }

    void DeteksiMuka()
    {// Hitung perbedaan posisi objek saat ini dengan posisi sebelumnya
        Vector2 movementDirection = (Vector2)transform.position - previousPosition;

        // Tentukan arah hadap muka berdasarkan pergerakan objek
        if (movementDirection.x <= 0.01f && movementDirection.x >= -0.01f && movementDirection.y <= 0.01f && movementDirection.y >= -0.01f)
        {
            // Jika pergerakan kurang dari threshold, set nilai arah muka menjadi negatif
            posDiam = true;

        }
        if (posDiam && arahMuka > 0)
        {
            arahMuka = arahMuka * -1;
            posDiam = false;
        }

        // Simpan posisi sebelumnya untuk pembaruan berikutnya
        previousPosition = transform.position;
    }

    void AnimasiRotasiPlayer()
    {


        // float targetRotation = 0f; // Defaultnya tidak berputar (0 derajat)

        // Mengonversi nilai arah negatif menjadi nilai positif yang sesuai
        //  if (arahHadap < 0)
        //    arahHadap = (arahHadap % 4 + 4) % 4;
        if (playerGridMove.jongkok)
        {
            anim.SetBool("Squat", true);
        }
        else
        {
            anim.SetBool("Squat", false);
        }

        

        if (!playerGridMove.jongkok && arahMuka > 0)
        {
            startWalk = true;
            if (!SoundSkripKontrol.lagiPause)
            {
                GetComponentInChildren<SoundEfekPlayer>().playSoundJalan(true);
            }
            

        }
        else if (arahMuka < 0)
        {
            endWalk = true;
            startWalk = false;
            GetComponentInChildren<SoundEfekPlayer>().playSoundJalan(false);

        }

        if (startWalk && endWalk)
        {
            
            endWalk = false;
            
        }

        {

        }

        if (arahMuka < 0)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Walk", false);
        }
        else
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Walk", true);
        }


        switch (arahMuka)
        {
            case -1:
                anim.SetFloat("isIdle", 0);
                break;
            case -2:
                anim.SetFloat("isIdle", 0.35f);
                break;
            case -3:
                anim.SetFloat("isIdle", 0.70f);
                break;
            case -4:
                anim.SetFloat("isIdle", 1f);
                break;
            case 1: // Menghadap ke atas (0 derajat)
                anim.SetFloat("isWalk", 0);
                break;
            case 2:
                anim.SetFloat("isWalk", 0.35f);
                break;
            case 3: // Menghadap ke bawah (180 derajat)
                anim.SetFloat("isWalk", 0.70f);
                break;
            case 4: // Menghadap ke kiri (270 derajat)
                anim.SetFloat("isWalk", 1f);
                break;
            default:
                break;
        }

        // Menghitung perubahan sudut rotasi yang dibutuhkan
        //float rotationAmount = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetRotation, rotasiSpeed * Time.deltaTime) - transform.eulerAngles.z;

        // Melakukan rotasi objek
       // transform.Rotate(Vector3.forward, rotationAmount);
    }

    public string PosPlayer()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, clickableLayer);
        GameObject Pos0;
        if (hit.collider != null && hit.collider.gameObject.CompareTag("tile"))
        {
            Pos0 = hit.collider.gameObject;
            posPlayer = Pos0.GetComponent<GridStart>().area;
           
        }
        return posPlayer;
    }

    void setWyAuto()
    {
        //Debug.Log("Nilai dari skrip di objek yang diklik: " + targetY);
        int startX = lastWaypoint.GetComponent<GridStart>().x;
        int startY = lastWaypoint.GetComponent<GridStart>().y;

        string pos = posPlayer;
        GameObject[,] wayArray;
        if (pos == "Area1")
        {
            wayArray = gridBvior.gridArray1;
        }
        else if (pos == "Area2")
        {
            wayArray = gridBvior.gridArray2;
        }
        else if (pos == "Area3")
        {
            wayArray = gridBvior.gridArray3;
        }
        else 
        {
            wayArray = gridBvior.gridArray4;
        }
        gridBvior.SetDistance(startX, startY, wayArray);
        gridBvior.SetPath(targetX, targetY, GridBehavior.path, wayArray);

        GridBehavior.path.Reverse();
        List<GameObject> automaticWaypoints = GridBehavior.path;

        // Tambahkan daftar waypoint yang didapatkan ke dalam daftar waypoint pada skrip
        automaticWaypoints.RemoveAt(0);

        
        waypointTiles.AddRange(automaticWaypoints);
        for(int i = waypointTiles.Count - automaticWaypoints.Count; i < waypointTiles.Count; i++)
        {
            InisiasiJejak(waypointTiles[i], waypointTiles[i - 1]);
        }
        //Debug.Log("Uhuuy");
        if (waypointTiles.Count > maxWaypoints)
        {
            waypointTiles.RemoveRange(maxWaypoints, waypointTiles.Count - maxWaypoints);
            for (int i = maxWaypoints - 1; i < footprints.Count; i++)
            {
                Destroy(footprints[i]);
            }
        }

        //ColorSet();
        
        GridBehavior.path.Clear();
        //siapJalan = true;

        // Jalan.waktuPilihJalan = false;
        bisaDeteksiJalur = false;
        //findDistance = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AREA"))
        {
            posPlayer = collision.gameObject.name;
        }
    }

    void ColorSet()
    {
        foreach (GameObject waypointTile in waypointTiles)
        {
            SpriteRenderer waypointRenderer = waypointTile.GetComponent<SpriteRenderer>();
            waypointTile.GetComponent<TileHighlight>().balikWarna = false;
            if (waypointRenderer != null)
            {
                waypointRenderer.color = selectedColor;
                Color newColor = waypointRenderer.color;
                newColor.a = transparencyValue;
                waypointRenderer.color = newColor;
            }
        }
    }

    void InisiasiJejak(GameObject tile, GameObject lastTile)
    {
        
            //GameObject lastTile = waypointTiles[waypointTiles.Count - 2];
            Vector2 direction = tile.transform.position - lastTile.transform.position;
            float angle = GetRotationAngleFromDirection(direction);
            InitializeFootprint(tile, angle);
        
    }

    float GetRotationAngleFromDirection(Vector2 direction)
    {
        float angle = 0f;

        if (direction == Vector2.up)
            angle = 0f;
        else if (direction == Vector2.right)
            angle = -90f;
        else if (direction == Vector2.down)
            angle = 180f;
        else if (direction == Vector2.left)
            angle = 90f;

        return angle;
    }

    void InitializeFootprint(GameObject tile, float rotationAngle)
    {
        // Buat dan atur posisi telapak kaki
        GameObject footprint = Instantiate(footprintPrefab, tile.transform.position, Quaternion.identity);
        footprints.Add(footprint); // Simpan referensi telapak kaki

        // Atur rotasi sprite telapak kaki
        footprint.transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }


    void InisiasiJejakAuto(List<GameObject> tiles)
    {
        if (tiles.Count < 2)
            return; // Tidak cukup waypoint untuk membuat jalur

        // Iterasi melalui waypoint baru, mulai dari yang kedua
        for (int i = 1; i < tiles.Count; i++)
        {
            GameObject previousWaypoint = tiles[i - 1];
            GameObject currentWaypoint = tiles[i];

            // Hitung jalur antara dua waypoint
            List<Vector2> path = CalculatePath(previousWaypoint.transform.position, currentWaypoint.transform.position);

            // Inisiasi telapak kaki di sepanjang jalur
            InitializeFootprints(path);
        }
    }

    List<Vector2> CalculatePath(Vector2 startPosition, Vector2 endPosition)
    {
        List<Vector2> path = new List<Vector2>();

        // Hitung jalur antara dua posisi
        // Misalnya, Anda dapat menggunakan algoritma seperti Bresenham's Line Algorithm atau Algoritma A*.

        return path;
    }

    void InitializeFootprints(List<Vector2> path)
    {
        float stepDistance = 0.5f; // Jarak antara setiap telapak kaki

        // Iterasi melalui jalur dan inisiasi telapak kaki di setiap titik
        for (int i = 0; i < path.Count - 1; i++)
        {
            Vector2 currentPosition = path[i];
            Vector2 nextPosition = path[i + 1];
            Vector2 direction = (nextPosition - currentPosition).normalized;

            float totalDistance = Vector2.Distance(currentPosition, nextPosition);
            int footprintCount = Mathf.FloorToInt(totalDistance / stepDistance);

            // Iterasi melalui setiap titik dalam bagian jalur dan inisiasi telapak kaki
            for (int j = 0; j < footprintCount; j++)
            {
                Vector2 footprintPosition = Vector2.Lerp(currentPosition, nextPosition, (float)j / footprintCount);
                GameObject footprint = Instantiate(footprintPrefab, footprintPosition, Quaternion.identity);
                footprints.Add(footprint); // Simpan referensi telapak kaki
            }
        }
    }
}
