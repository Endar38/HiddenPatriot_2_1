using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSetAM : MonoBehaviour
{
    public AutoMoveControl AMControl;
    public int indexTarget;
    public string area;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("tile"))
        {
            if (collision.gameObject.GetComponent<GridStart>().area == area)
            {
                AMControl.targetTile[indexTarget] = collision.gameObject;
                Destroy(gameObject);
            }
        }
    }
}
