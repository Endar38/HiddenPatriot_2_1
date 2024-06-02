using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStart : MonoBehaviour
{
    public int visited = -1;
    public int x = 0;
    public int y = 0;
    public bool terkenaGranat;
    public string area;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("SmokeEfek"))
        {
            terkenaGranat = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("SmokeEfek"))
        {
            terkenaGranat = false;
        }


    }
}
