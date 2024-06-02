using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimasiKontrol : MonoBehaviour
{

    int arahMuka;
    Animator animEmy;
    pathEnemy pEnm;
    // Start is called before the first frame update
    void Start()
    {
        animEmy = GetComponent<Animator>();
        pEnm = GetComponent<pathEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimasiRotasiEnemy();
    }

    void AnimasiRotasiEnemy()
    {


        // float targetRotation = 0f; // Defaultnya tidak berputar (0 derajat)

        // Mengonversi nilai arah negatif menjadi nilai positif yang sesuai
        //  if (arahHadap < 0)
        //    arahHadap = (arahHadap % 4 + 4) % 4;
        arahMuka = GetComponent<pathEnemy>().arahMuka;


        if (pEnm.pointBVior == 0 && !pEnm.lagiJalanEfekKoin && !pEnm.lagiJalanEfekSmoke && !pEnm.lagiEfekTembak && arahMuka < 0 && pEnm.rotasiBiasa)
        {
            animEmy.SetBool("EnemyNgobrol", true);
            
        }
        else
        {
            animEmy.SetBool("EnemyNgobrol", false);
        }

        if (arahMuka != -1 || (pEnm.pointBVior == 0 && pEnm.rotasiBiasa))
        {
            animEmy.SetBool("EnemyIdleAtas", false);
        }
        if (arahMuka != -2 || (pEnm.pointBVior == 0 && pEnm.rotasiBiasa))
        {
            animEmy.SetBool("EnemyIdleKanan", false);
        }
        if (arahMuka != -3 || (pEnm.pointBVior == 0 && pEnm.rotasiBiasa))
        {
            animEmy.SetBool("EnemyIdleBawah", false);
        }
        if (arahMuka != -4 || (pEnm.pointBVior == 0 && pEnm.rotasiBiasa))
        {
            animEmy.SetBool("EnemyIdleKiri", false);
        }

        if (arahMuka < 0)
        {
            animEmy.SetBool("EnemyJalan", false) ;
        }
        else if (arahMuka > 0)
        {
            animEmy.SetBool("EnemyJalan", true) ;
        }

        switch (arahMuka)
        {
            case -1:
                if (pEnm.pointBVior != 0 || !pEnm.rotasiBiasa)
                {
                    animEmy.SetBool("EnemyIdleAtas", true);
                }
                
                break;
            case -2:
                if (pEnm.pointBVior == 0 && pEnm.rotasiBiasa)
                {
                    
                    animEmy.SetFloat("EnmNgobrol", 0f);
                }
                else
                {
                    animEmy.SetBool("EnemyIdleKanan", true);
                }
                
                break;
            case -3:
                if (pEnm.pointBVior != 0 || !pEnm.rotasiBiasa)
                {
                    animEmy.SetBool("EnemyIdleBawah", true);
                }
                
                break;
            case -4:
                if (pEnm.pointBVior == 0 && pEnm.rotasiBiasa)
                {
                    animEmy.SetFloat("EnmNgobrol", 1f);
                }
                else
                {
                    animEmy.SetBool("EnemyIdleKiri", true);
                    
                }
                
                break;
            case 1: // Menghadap ke atas (0 derajat)
                animEmy.SetFloat("EnmJalan", 0f);
                break;
            case 2:
                animEmy.SetFloat("EnmJalan", 0.35f);
                break;
            case 3: // Menghadap ke bawah (180 derajat)
                animEmy.SetFloat("EnmJalan", 0.70f);
                break;
            case 4: // Menghadap ke kiri (270 derajat)
                animEmy.SetFloat("EnmJalan", 1f);
                break;
            default:
                break;
        }

        // Menghitung perubahan sudut rotasi yang dibutuhkan
        //float rotationAmount = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetRotation, rotasiSpeed * Time.deltaTime) - transform.eulerAngles.z;

        // Melakukan rotasi objek
        // transform.Rotate(Vector3.forward, rotationAmount);
    }
}
