using System.Collections;
using UnityEngine;

public class EnemyCreeper : Enemy
{
    private BoxCollider2D box;
    private Rigidbody2D rb;
    public Transform target;
    public SpriteRenderer rendererX;
    public GameObject explosionPrefab;
    
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        health = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 targetPos = new Vector2(target.position.x, target.position.y);
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
        Color baseColor = rendererX.color;
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
        health -= damage;
        StartCoroutine(ChangeColor());
        if (health <= 0)
        {
            UIManager.instance.AddScore(2);
            explode();
        }
    }

    private void explode()
    {
           Instantiate(explosionPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject);
    }

    public override void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Attack(Player player)
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}