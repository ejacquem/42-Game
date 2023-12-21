using System.Collections;
using UnityEngine;

public class EnemyCreeper : MonoBehaviour
{

    Rigidbody2D rb;
    BoxCollider2D box;
    int maxHealth = 15;
    int currentHealth;
    public float moveSpeed = 4f;
    Vector2 enemyPos;
    Vector2 targetPos;
    private Color baseColor;
    public Transform target;
    public SpriteRenderer rendererX;
    public GameObject explosionPrefab;
    
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
            explode();
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
            UIManager.instance.AddScore(2);
            explode();
        }
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }

    private void explode()
    {

            Instantiate(explosionPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject);
    }
}
