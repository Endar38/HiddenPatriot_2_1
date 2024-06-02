using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaNgobrolPas : MonoBehaviour
{

    NgobrolSet ngob;
    public GameObject panelDialog;
    public GameObject dialogBar;
    BarDialogControl barDialogControl;
    // Start is called before the first frame update
    void Start()
    {
        ngob = transform.parent.GetComponent<NgobrolSet>();
        barDialogControl = dialogBar.GetComponent<BarDialogControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ngob.mulaiNgobrol = true;
            panelDialog.SetActive(true);
            barDialogControl.SetCondition(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ngob.mulaiNgobrol = false;
            barDialogControl.SetCondition(false);
            panelDialog.SetActive(false);
            
        }
    }
}
