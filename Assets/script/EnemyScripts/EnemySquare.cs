using System.Collections;
using UnityEngine;

public class EnemySquare : Enemy
{
    public Transform target;
    public SpriteRenderer rendererX;
    public GameObject ammoPrefab;
    public GameObject speedPrefab;
    public GameObject fireRatePrefab;
    public GameObject maxHealthPrefab;
    
    void Start()
    {
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
            Attack(collision.collider.GetComponentInParent<Player>());
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
    public override void TakeDamage(float damage)
    {
        health -= damage;
        StartCoroutine(ChangeColor());
        if (health <= 0)
        {
            Die();
        }
    }

    public override void Attack(Player player)
    {
        player.Damage(5);
        Vector2 direction = (player.transform.position - transform.position).normalized;
        //Debug.Log(direction);
        player.GetComponent<Rigidbody2D>().AddForce(direction * knockbackForce * 100, ForceMode2D.Force);
    }

    public override void Die()
    {
        if (Random.Range(0f,1f) <=  0.02f)
        {
            Instantiate(maxHealthPrefab, transform.position , Quaternion.identity);
        }
        if (Random.Range(0f,1f) <=  0.05f)
        {
            Instantiate(fireRatePrefab, transform.position , Quaternion.identity);
        }
        if (Random.Range(0f,1f) <= 0.1f)
        {
            Instantiate(speedPrefab, transform.position , Quaternion.identity);
        }
        if (Random.Range(0f,1f) <= 0.7f)
        {
            Instantiate(ammoPrefab, transform.position , Quaternion.identity);
        }
        UIManager.instance.AddScore(1);
        Destroy(gameObject);
    }
}
