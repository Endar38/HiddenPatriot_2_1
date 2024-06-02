using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NgobrolKontrol : MonoBehaviour
{

    NgobrolSet ngobrol;
    // Start is called before the first frame update
    void Start()
    {
        ngobrol = transform.parent.GetComponent<NgobrolSet>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ngobrol.indexNgobrol++;
            //gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ngobrol.indexNgobrol--;
        }
    }
}
