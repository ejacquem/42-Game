using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public GameObject coinPrefab;
    private float time;
    public float spawnRate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= spawnRate)
        {
            time = 0;
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }
}
