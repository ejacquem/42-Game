using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildArea : MonoBehaviour
{
    public bool playerInsideBuildArea;
    // Start is called before the first frame update
    void Start()
    {
        playerInsideBuildArea = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            playerInsideBuildArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            playerInsideBuildArea = false;
        }
    }

}
