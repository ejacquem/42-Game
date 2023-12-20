using System.Collections;
using Unity.Mathematics;
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
    public SpriteRenderer rendererX;
    private Color baseColor;
    public GameObject ammoPrefab;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        currentHealth = maxHealth;
        baseColor = rendererX.color;
        target = GameObject.FindGameObjectWithTag("Player").transform;
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

    private IEnumerator ChangeColor()
    {
        rendererX.color = Color.white;
        yield return new WaitForSeconds(0.09f);
        rendererX.color = baseColor;
        yield return new WaitForSeconds(0.09f);
        rendererX.color = Color.white;
        yield return new WaitForSeconds(0.09f);
        rendererX.color = baseColor;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(ChangeColor());
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Instantiate(ammoPrefab, transform.position, Quaternion.identity);
        UIManager.instance.AddScore(1);
        Destroy(gameObject);
    }
}
