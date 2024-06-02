using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    public static bool waktuPilih;
    public static bool bolehPilih;
    /*
    public static bool bolehPilihJalan;
    public static bool bolehPilihLemparKoin;
    public static bool bolehPilihLemparSmoke;
    public static bool bolehPilihJongkok;
    public static bool bolehPilihBerdiri;
    public static bool bolehPilihTembak;
    public static bool bolehPilihMelee;
    */
    public static bool eksekusi = false;

    bool tombolPilihAksi;
    bool tombolJalan;
    bool tombolBerdiri;
    bool tombolTembak;
    bool tombolMelee;
    bool tombolLemparKoin;
    bool tombolLemparSmoke;
    bool tombolJongkok;
    bool tombolEksekusi;
    public List<System.Action> commandSequence = new List<System.Action>();
    /*
    public bool statusJalan;
    public bool statusJongkok;
    public bool statusLemparKoin;
    public bool statusLemparSmoke;
    public bool statusBerdiri;
    public bool statusTembak;
    public bool statusMelee;
    */
    int indexJalan;
    int indexJongkok;
    int indexLemparKoin;
    int indexLemparSmoke;
    int indexBerdiri;
    int indexTembak;
    int indexMelee;

    int indexAksi;
    bool indexDel;
    int indexDelAksi;

    public GameObject player;
    public List <GameObject> prefabEnemys = new List<GameObject>();

    public static int koinSisa;
    public static int pistolSisa;
    public static int smokeSisa;
    public static int kunci;

    public Text koinTeksSisa;
    public Text pistolTeksSisa;
    public Text smokeTeksSisa;
    public Text teksJongkok;

    public Image ikonBerdiri;
    public Sprite[] ikonBerdiri2;

    public static bool kondisiKalah;
    public static bool kondisiMenang;

    public GameObject panelKalah;
    public GameObject panelMenang;
    public GameObject panelUtama;
    public GameObject panelTombolPause;
    public GameObject panelDrag;

    public int indexWaktuBVior;
    //public bool alatDari0;
   // public bool alatDariLanjut;
   public int level;

    public int smokeAwal;
    public int pistolAwal;
    public int koinAwal;

    


    // Start is called before the first frame update
    void Start()
    {
        bolehPilih = false;
        indexAksi = 0;
        waktuPilih = true;

        /*
        statusJalan = false;
        statusJongkok = false;
        statusLemparKoin = false;
        statusLemparSmoke = false;
        statusBerdiri = false;
        statusTembak = false;
        statusMelee = false;
        */

        indexDel = false;
        kondisiKalah = false;
        kondisiMenang = false;

        PanelMapDrag.bolehDragMap = true;

        panelKalah.SetActive(false);
        panelMenang.SetActive(false);
        panelUtama.SetActive(true);
        panelTombolPause.SetActive(true);

        kunci = 1;
        SaveManager.LoadLevelData(level, out smokeSisa, out pistolSisa, out koinSisa);
        indexWaktuBVior = 0;
        //pistolSisa = 11;

        Time.timeScale = 1;
        
        

    }

    // Update is called once per frame
    void Update()
    {

        koinTeksSisa.text = (koinSisa.ToString());
        pistolTeksSisa.text = (pistolSisa.ToString());
        smokeTeksSisa.text = (smokeSisa.ToString());
        
        
        
        
        if( indexDel )
        {
            if (indexJalan > indexDelAksi)
            {
                indexJalan = indexJalan - 1;
            }
            if (indexBerdiri > indexDelAksi)
            {
                indexBerdiri = indexBerdiri - 1;
            }
            if (indexJongkok > indexDelAksi)
            {
                indexJongkok= indexJongkok - 1;
            }
            if (indexLemparKoin > indexDelAksi)
            {
                indexLemparKoin = indexLemparKoin - 1;
            }
            if (indexLemparSmoke > indexDelAksi)
            {
                indexLemparSmoke = indexLemparSmoke - 1;
            }
            if ( indexTembak > indexDelAksi)
            {
                indexTembak = indexTembak - 1;
            }
            if (indexMelee > indexDelAksi)
            {
                indexMelee = indexMelee - 1;
            }
            indexDel = false;
        }

       // Debug.Log(eksekusi);

        if (kondisiKalah)
        {
            Time.timeScale = 0;
            panelKalah.SetActive(true);
            panelUtama.SetActive(false);
            panelTombolPause.SetActive(false);
            GetComponent<SoundSkripKontrol>().playLose = true;
            kondisiKalah = false;
        }
        if (kondisiMenang)
        {
            
            SaveManager.SaveLevelData(level + 1, smokeSisa, pistolSisa, koinSisa);
            SaveManager.SaveData1(level + 1, 1);
            
            Time.timeScale = 0;
            panelMenang.SetActive(true);
            panelUtama.SetActive(false);
            panelTombolPause.SetActive(false);
            GetComponent<SoundSkripKontrol>().playWin = true;
            kondisiMenang = false;
        }

        if(indexWaktuBVior >= 2)
        {
            player.GetComponent<lempar>().SwitchBVior(false);
            indexWaktuBVior = 0;
        }


        if (!PanelMapDrag.bolehDragMap)
        {
            panelUtama.SetActive (false);
        }
        else
        {
            panelUtama.SetActive (true);
        }



    }
    public void AddCommand(System.Action command)
    {
        commandSequence.Add(command); 
    }
    void WaktuJalan()
    {
        playerGridMove.jalan = true;
    }
    void WaktuJongkok()
    {
        playerGridMove.waktuJongkok = true;
        teksJongkok.text = "Stand";
        ikonBerdiri.sprite = ikonBerdiri2[0];
    }
    void WaktuBerdiri()
    {
        playerGridMove.waktuBerdiri = true;
        teksJongkok.text = "Crouch";
        ikonBerdiri.sprite = ikonBerdiri2[1];
    }
    void WaktuLemparKoin()
    {
        lempar.waktuLemparKoin = true;
    }
    void WaktuLemparSmoke()
    {
        lempar.waktuLemparSmoke = true;
    }
    void WaktuTembak()
    {
        tembak.waktuTembak = true;
    }
    void WaktuMelee()
    {
        tembak.waktuMelee = true;
    }


    public void TombolPilihAksi()
    {
        //if (player.GetComponent<AutoMoveControl>().waktuAutoMove)
        //{
        if (waktuPilih && !bolehPilih)
        {
            bolehPilih = true;
            /*
            bolehPilihJalan = true;
            bolehPilihBerdiri = true;
            bolehPilihJongkok = true;
            bolehPilihLemparKoin = true;
            bolehPilihLemparSmoke = true;
            bolehPilihTembak = true;
            bolehPilihMelee = true;
            */
            tombolMove.allowMove = true;
            waktuPilih = false;
            Debug.Log("A = Jalan, D = Lempar, S = Jongkok, W = Berdiri");
            //tombolPilihAksi = false;
        }
        indexAksi = 0;
       // }
        
    }

    public void TombolJalan()
    {
        if (bolehPilih && commandSequence.Count < 3 && !commandSequence.Contains(WaktuJalan))
        {
            tombolMove.allowMove = true;
            indexJalan = indexAksi;
            Jalan.waypointTiles.Clear();
            PanelMapDrag.bolehDragMap = false;
            Jalan.waktuPilihJalan = true;
            AddCommand(WaktuJalan);
            //bolehPilihJalan = false;
            Debug.Log("Pilih Jalan");
            bolehPilih = false;
            //statusJalan = true;
            //tombolJalan = false;

            indexAksi = indexAksi + 1;

        }
        else if (bolehPilih && commandSequence.Contains(WaktuJalan))
        {
            tombolMove.allowMove = true;
            indexDelAksi = indexJalan;
            indexDel = true;
            playerGridMove.batal = true;
            commandSequence.RemoveAt(indexJalan);
            indexAksi = indexAksi - 1;
            //bolehPilihJalan = true;
           // statusJalan = false;
        }
        else
        {
            tombolMove.allowMove = false;
        }
        
    }

   

    public void TombolJongkok()
    {
        if (playerGridMove.jongkok == false)
        {
            if (bolehPilih && commandSequence.Count < 3 && !commandSequence.Contains(WaktuJongkok))
            {
                tombolMove.allowMove = true;
                indexJongkok = indexAksi;
                AddCommand(WaktuJongkok);
                //bolehPilihJongkok = false;
                Debug.Log("jongkok");
                //statusJongkok = true;
                //tombolJongkok = false;

                indexAksi = indexAksi + 1;
            }
            else if (bolehPilih && commandSequence.Contains(WaktuJongkok))
            {
                tombolMove.allowMove = true;
                indexDelAksi = indexJongkok;
                indexDel = true;

                commandSequence.RemoveAt(indexJongkok);
                indexAksi = indexAksi - 1;
               // bolehPilihJongkok = true;
                //statusJongkok = false;
            }
            else
            {
                tombolMove.allowMove = false;
            }
        }
        else if (playerGridMove.jongkok == true)
        {
            if (bolehPilih && commandSequence.Count < 3 && !commandSequence.Contains(WaktuBerdiri))
            {
                tombolMove.allowMove = true;
                indexBerdiri = indexAksi;
                AddCommand(WaktuBerdiri);
                //bolehPilihBerdiri = false;
                Debug.Log("berdiri");
                //statusBerdiri = true;
                //tombolBerdiri = false;

                indexAksi = indexAksi + 1;
            }
            else if (bolehPilih && commandSequence.Contains(WaktuBerdiri))
            {
                tombolMove.allowMove = true;
                indexDelAksi = indexBerdiri;
                indexDel = true;

                commandSequence.RemoveAt(indexBerdiri);
                indexAksi = indexAksi - 1;
                //bolehPilihBerdiri = true;
                //statusBerdiri = false;
            }
            else
            {
                tombolMove.allowMove = false;
            }
        }

    }

    public void TombolLemparKoin()
    {
        if (koinSisa > 0)
        {
            if (bolehPilih && commandSequence.Count < 3 && !commandSequence.Contains(WaktuLemparKoin))
            {
                tombolMove.allowMove = true;
                indexLemparKoin = indexAksi;
                PanelMapDrag.bolehDragMap = false;
                lempar.bisaPilihLemparKoin = true;
                AddCommand(WaktuLemparKoin);
                //bolehPilihLemparKoin = false;
                Debug.Log("Pilih sasaran");
                bolehPilih = false;
                //statusLemparKoin = true;
                //tombolLemparKoin = false;

                indexAksi = indexAksi + 1;
            }
            else if (bolehPilih && commandSequence.Contains(WaktuLemparKoin))
            {
                tombolMove.allowMove = true;
                indexDelAksi = indexLemparKoin;
                indexDel = true;

                commandSequence.RemoveAt(indexLemparKoin);
                indexAksi = indexAksi - 1;
                //bolehPilihLemparKoin = true;
                //statusLemparKoin = false;
            }
            else
            {
                tombolMove.allowMove = false;
            }
        }
        else
        {
            tombolMove.allowMove = false;
        }
    }
    public void TombolLemparSmoke()
    {
        if (smokeSisa > 0)
        {
            if (bolehPilih && commandSequence.Count < 3 && !commandSequence.Contains(WaktuLemparSmoke))
            {
                tombolMove.allowMove = true;
                indexLemparSmoke = indexAksi;
                PanelMapDrag.bolehDragMap = false;
                lempar.bisaPilihLemparSmoke = true;
                AddCommand(WaktuLemparSmoke);
                //bolehPilihLemparSmoke = false;
                Debug.Log("Pilih sasaran");
                bolehPilih = false;
                //statusLemparSmoke = true;
                //tombolLemparSmoke = false;

                indexAksi = indexAksi + 1;
            }
            else if (bolehPilih && commandSequence.Contains(WaktuLemparSmoke))
            {
                tombolMove.allowMove = true;
                indexDelAksi = indexLemparSmoke;
                indexDel = true;

                commandSequence.RemoveAt(indexLemparSmoke);
                indexAksi = indexAksi - 1;
                //bolehPilihLemparSmoke = true;
                //statusLemparSmoke = false;
            }
            else
            {
                tombolMove.allowMove = false;
            }
        }
        else
        {
            tombolMove.allowMove = false;
        }

    }
    public void TombolTembak()
    {
        if (pistolSisa > 0)
        {
            if (bolehPilih && commandSequence.Count < 3 && !commandSequence.Contains(WaktuTembak))
            {
                tombolMove.allowMove = true;
                indexTembak = indexAksi;
                player.GetComponent<tembak>().posEnemyTembak = null;
                PanelMapDrag.bolehDragMap = false;
                panelUtama.SetActive(false);
                bolehPilih = false;
                tembak.waktuPilihTembak = true;
                AddCommand(WaktuTembak);
                //bolehPilihTembak = false;
                //statusTembak = true;
                tombolTembak = false;
                Debug.Log("pilih enemy");

                indexAksi = indexAksi + 1;
            }
            else if (bolehPilih && commandSequence.Contains(WaktuTembak))
            {
                tombolMove.allowMove = true;
                indexDelAksi = indexTembak;
                indexDel = true;

                commandSequence.RemoveAt(indexTembak);
                indexAksi = indexAksi - 1;
               // bolehPilihTembak = true;
                //statusTembak = false;
            }
            else
            {
                tombolMove.allowMove = false;
            }
        }
        else
        {
            tombolMove.allowMove = false;
        }
    }

    public void TombolMelee()
    {
        if (bolehPilih && commandSequence.Count < 3 && !commandSequence.Contains(WaktuMelee))
        {
            tombolMove.allowMove = true;
            indexMelee = indexAksi;
            PanelMapDrag.bolehDragMap = false;
            bolehPilih = false;
            tembak.waktuPilihMelee = true;
            AddCommand(WaktuMelee);
            //bolehPilihMelee = false;
            //statusMelee = true;
            //tombolMelee = false;
            Debug.Log("pilih enemy");

            indexAksi = indexAksi + 1;
        }
        else if (bolehPilih && commandSequence.Contains(WaktuMelee))
        {
            tombolMove.allowMove = true;
            indexDelAksi = indexMelee;
            indexDel = true;

            commandSequence.RemoveAt(indexMelee);
            indexAksi = indexAksi - 1;
           // bolehPilihMelee = true;
            //statusMelee = false;
        }
        else
        {
            tombolMove.allowMove = false;
        }

    }
    public void TombolEksekusi()
    {
        if (commandSequence.Count >= 1 && bolehPilih)
        {
            tombolMove.allowMove = true;
            GetComponent<TombolVisibel>().ToggleTargetVisibility(false);
            Debug.Log("eksekusi");
            bolehPilih = false;
            /*
            statusJalan = false;
            statusJongkok = false;
            statusLemparKoin = false;
            statusLemparSmoke = false;
            statusBerdiri = false;
            statusTembak = false;
            */
            tombolMove.returnAllToOriginalPosition = true;
            StartCoroutine(ExecuteCommandsInSequence());


            //GetComponent<TombolVisibel>().bolehDelete = false;


            //tombolEksekusi = false;
            indexAksi = 0;
        }
        
    }


    private IEnumerator ExecuteCommandsInSequence()
    {

        panelDrag.SetActive(false);
        player.GetComponent<AutoMoveControl>().waktuAutoMove = false;
        waktuPilih = false; // Menonaktifkan penerimaan input sampai semua perintah selesai dieksekusi
        Destroy(player.GetComponent<lempar>().bulletKoin);
        Destroy(player.GetComponent<lempar>().bulletSmoke);

        foreach (GameObject enemy in prefabEnemys)
        {
            enemy.GetComponentInChildren<FOVScript>().efekSmokeBaru = false;
            enemy.GetComponent<pathEnemy>().lagiJalanEfekKoin = false;
            enemy.GetComponent<pathEnemy>().lagiEfekTembak = false;
        }

        player.GetComponent<lempar>().efekSmoke = false;
        foreach (var command in commandSequence)
        {
            // Memanggil metode dalam list
            yield return StartCoroutine(command.Method.Name);
            
            // Menunggu hingga pemicu menjadi true sebelum melanjutkan ke metode berikutnya
            yield return new WaitUntil(() => eksekusi);

            // Setelah pemicu menjadi true, reset pemicu untuk persiapan perintah berikutnya
            eksekusi = false;
        }

        panelDrag.SetActive(true);
        
        //GetComponent<TombolVisibel>().bolehDelete = true;

        

        commandSequence.Clear(); // Menghapus semua perintah setelah dieksekusi
        
        player.GetComponent<AutoMoveControl>().waktuAutoMove = true;
        waktuPilih = true; // Mengaktifkan kembali penerimaan input setelah semua perintah selesai dieksekusi
        yield return new WaitForSeconds(0.5f);
        indexWaktuBVior++;
    }

    

}
