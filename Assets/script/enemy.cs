using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{

    public float knockbackForce = 5f;
    BoxCollider2D box;
    int maxHealth = 15;
    int currentHealth;
    public float moveSpeed = 2f;
    Rigidbody2D rb;
   public Transform target;
    Vector2 enemyPos;
    Vector2 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        enemyPos = new Vector2(transform.position.x, transform.position.y);
        targetPos = new Vector2(target.position.x, target.position.y);
        transform.position = Vector2.MoveTowards(enemyPos, targetPos, moveSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        { 
            Player player = collision.collider.GetComponentInParent<Player>();
            player.Damage(5);
            Vector2 direction = (collision.collider.transform.position - transform.position).normalized;
            //Debug.Log(direction);
            collision.collider.GetComponent<Rigidbody2D>().AddForce(direction * knockbackForce * 100, ForceMode2D.Force);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }


}
