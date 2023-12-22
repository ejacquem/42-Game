using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiExplosion : MonoBehaviour
{
    public float knockbackForce = 10f;
    public int DamageToPlayer = 20;
    public int DamageToEnemy = 5;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        Vector2 direction = (other.transform.position - transform.position).normalized;
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponentInParent<Player>();
            player.Damage(DamageToPlayer);
            other.GetComponent<Rigidbody2D>().AddForce(direction * knockbackForce * 100, ForceMode2D.Force);
        }        
        if(other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();
            enemy.TakeDamage(DamageToPlayer);
            other.GetComponent<Rigidbody2D>().AddForce(direction * knockbackForce * 100, ForceMode2D.Force);
        }
        GetComponent<CircleCollider2D>().enabled = false;
    }
}
