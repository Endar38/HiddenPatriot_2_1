using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class lempar : MonoBehaviour
{
    public LayerMask clickableLayer;
    public GameObject koinPrefab;
    public GameObject smokePrefab;
    public float throwSpeed = 5f;

    private Vector2 targetPositionKoin;
    private Vector2 targetPositionSmoke;
    public static bool bisaPilihLemparKoin;
    public static bool bisaPilihLemparSmoke;


    public static bool waktuLemparKoin;
    public static bool waktuLemparSmoke;

    public bool efekKoin;
    public bool efekSmoke;

    public List <GameObject> enemys = new List<GameObject>();

    public int cekKeKoin;

    public GameObject bulletKoin;
    public GameObject bulletSmoke;

    Animator anim;
    Animator smokeAnim;

    int arahSmoke;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (bisaPilihLemparKoin || bisaPilihLemparSmoke))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, clickableLayer);
            if (hit.collider != null)
            {
                if (hit.collider != null && hit.collider.CompareTag("tile"))
                {

                    // Dapatkan posisi mouse dalam koordinat dunia
                    
                    // targetPosition.z = 0f;
                    ActionManager.bolehPilih = true;
                    PanelMapDrag.bolehDragMap = true;
                    DeteksiAnimasiLempar(hit.collider.gameObject);

                    if(bisaPilihLemparKoin)
                    {
                        foreach (GameObject enemy in enemys)
                        {
                            enemy.GetComponent<pathEnemy>().posKoinJatuh = hit.collider.gameObject;
                            enemy.GetComponent<pathEnemy>().sudahCekKoin = false;
                        }
                        
                        targetPositionKoin = hit.collider.GetComponent<Transform>().position;
                        bisaPilihLemparKoin = false;
                    }

                    if (bisaPilihLemparSmoke)
                    {
                        foreach (GameObject enemy in enemys)
                        {
                            enemy.GetComponentInChildren<FOVScript>().posSmokeJatuh = hit.collider.gameObject;
                            enemy.GetComponentInChildren<FOVScript>().player = transform.gameObject;

                        }
                        targetPositionSmoke = hit.collider.GetComponent<Transform>().position;
                        bisaPilihLemparSmoke = false;
                    }
                    

                }
            }
        
        

        }
        if (waktuLemparKoin)
        {
            anim.SetBool("Lempar", true);
            //GetComponentInChildren<SoundEfekPlayer>().playSoundLempar();

            // Instantiate peluru dari prefab
            bulletKoin = Instantiate(koinPrefab, transform.position, Quaternion.identity);
            cekKeKoin = 0;
            int i = 1;
            StartCoroutine(MoveProjectile(bulletKoin.transform, targetPositionKoin, throwSpeed, i));// Mulai animasi gerakan melengkung
            waktuLemparKoin = false;
  
        }

        if (waktuLemparSmoke)
        {
            anim.SetBool("Lempar", true);
            

            // Instantiate peluru dari prefab
            bulletSmoke = Instantiate(smokePrefab, transform.position, Quaternion.identity);

            smokeAnim = bulletSmoke.GetComponent<Animator>();
            if (arahSmoke == 1)
            {
                smokeAnim.SetFloat("Smoke", 0);
            }
            else if (arahSmoke == 2)
            {
                smokeAnim.SetFloat("Smoke", 0.35f);

            }
            else if (arahSmoke == 3)
            {
                smokeAnim.SetFloat("Smoke", 0.70f);

            }
            else if (arahSmoke == 4)
            {
                smokeAnim.SetFloat("Smoke", 1f);

            }
            int i = 2;
            StartCoroutine(MoveProjectile(bulletSmoke.transform, targetPositionSmoke, throwSpeed, i));// Mulai animasi gerakan melengkung
            waktuLemparSmoke = false;

        }
        if (cekKeKoin >= enemys.Count)
        {
            efekKoin = false;
        }

    }

    // Coroutine untuk menggerakkan peluru secara melengkung
    IEnumerator MoveProjectile(Transform projectile, Vector2 target, float speed, int indexLemparKS)
    {
        GetComponentInChildren<SoundEfekPlayer>().playSoundLempar();
        float duration = Vector2.Distance(projectile.position, target) / speed;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            projectile.position = CalculateParabolicArc(transform.position, target, t);
            elapsed += Time.deltaTime;

            if (projectile.GetComponent<KoinKontrol>().meledak)
            {
                projectile.GetComponent<KoinKontrol>().meledak = false;
                break;
            }

            yield return null;
        }

        // Pastikan peluru berada di posisi target secara akurat pada akhir animasi
        //projectile.position = target;

        // Hancurkan peluru saat mencapai target
       // Destroy(projectile.gameObject);
        // Debug.Log("josee");
        // Debug.Log(indexLemparKS);
        anim.SetBool("Lempar", false);

        if (indexLemparKS == 1)
        {
          //  Debug.Log("jos");
            efekKoin = true;
            GetComponentInChildren<SoundEfekPlayer>().playSoundKoin();
            ActionManager.koinSisa--;
        }
        if (indexLemparKS == 2)
        {
            smokeAnim.SetTrigger("SmokeMeledak");
            GetComponentInChildren<SoundEfekPlayer>().playSoundSmoke();
            projectile.GetComponent<SmokeKontrol>().efekLedakanSmoke = true;
            efekSmoke = true;
            ActionManager.smokeSisa--;
        }
        ActionManager.eksekusi = true;
    }

    // Fungsi untuk menghitung jalur parabola
    Vector2 CalculateParabolicArc(Vector2 start, Vector2 target, float t)
    {
        float x = Mathf.Lerp(start.x, target.x, t);
        float y = Mathf.Lerp(start.y, target.y, t) + Mathf.Sin(t * Mathf.PI) * 2f; // Sesuaikan dengan melengkung yang diinginkan

        return new Vector2(x, y);
    }

    void DeteksiAnimasiLempar(GameObject sasaran)
    {
        float angleThreshold = 45f;
        // Hitung vektor dari Player ke objek A (dalam 2D, tidak perlu menggunakan Vector3.up)
        Vector2 direction = sasaran.transform.position - transform.position;

        // Hitung sudut antara vektor dari Player ke objek A dan vektor hadap depan Player (dalam 2D, tidak perlu menggunakan Vector3.forward)
        float angle = Vector2.SignedAngle(Vector2.right, direction);

        // Ubah sudut menjadi nilai positif (tidak perlu dilakukan perubahan karena SignedAngle sudah memberikan sudut yang positif)
        if (angle < 0)
        {
            angle += 360f;
        }
        //Debug.Log(angle);
        // Tentukan nilai x berdasarkan sudut
        if ((angle <= angleThreshold && angle >= 0f) || (angle > 360f - angleThreshold && angle <= 360f))
        {
            arahSmoke = 2;
            anim.SetFloat("ArahLempar", 0.35f);
        }
        else if (angle > angleThreshold && angle <= 180f - angleThreshold)
        {
            arahSmoke = 1;
            anim.SetFloat("ArahLempar", 0);
        }
        else if (angle > 180f + angleThreshold && angle <= 360f - angleThreshold)
        {
            arahSmoke = 3;
            anim.SetFloat("ArahLempar", 0.70f);
        }
        else if (angle > 180f - angleThreshold && angle <= 180f + angleThreshold)
        {
            arahSmoke = 4;
            anim.SetFloat("ArahLempar", 1f);
        }

    }

    public void SwitchBVior(bool reset)
    {
        foreach (GameObject enemy in enemys)
        {
            pathEnemy PE = enemy.GetComponent<pathEnemy>();
            
            if (PE.pointBVior < PE.maxBVior && !reset && PE.bolehGantiBvior)
            {
                PE.pointBVior = PE.pointBVior + 1;
                PE.gantiBvior = true;
            }
            else if (!reset && PE.bolehGantiBvior && PE.pointBVior >= PE.maxBVior)
            {
                PE.pointBVior = 1;
                PE.gantiBvior = true;
            }
            else if (PE.bolehGantiBvior && reset && PE.bolehGantiBvior)
            {
                PE.pointBVior = 1;
                PE.gantiBvior = true;
            }
            

        }

    }
}


