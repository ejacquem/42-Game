using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other);
        if(other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(1);
        }
    }
}
