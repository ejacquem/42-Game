using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiExplosion : MonoBehaviour
{
    public float knockbackForce = 50f;
    public int DamageToPlayer = 20;
    public int DamageToEnemy = 5;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponentInParent<Player>();
            player.Damage(DamageToPlayer);
        }        
        if(collision.collider.CompareTag("Enemy"))
        {
            Enemy enemy = collision.collider.GetComponentInParent<Enemy>();
            enemy.TakeDamage(DamageToPlayer);
        }
        Vector2 direction = (collision.collider.transform.position - transform.position).normalized;
        collision.collider.GetComponent<Rigidbody2D>().AddForce(direction * knockbackForce * 100, ForceMode2D.Force);
    }

}
