using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementCursorScript : MonoBehaviour
{
    public bool collide = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("collide2D");
        collide = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        collide = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        collide = false;
    }

    public bool getCollide()
    {
        return collide;
    }
}
