using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
        {
            Debug.Log("Enemy attacks base");
            health-=5;
            EnemyNM enemy = col.GetComponentInParent<EnemyNM>();
            enemy.TakeDamage(100_000);
        }
    }

}
