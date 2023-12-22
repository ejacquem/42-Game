using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : Enemy
{
    BoxCollider2D box;
    Rigidbody2D rb;
    private Transform target;
    public Transform firePoint;
    public SpriteRenderer rendererX;
    public GameObject enemyBulletPrefab;
    public GameObject ammoPrefab;
    public GameObject speedPrefab;
    public GameObject fireRatePrefab;
    public GameObject maxHealthPrefab;

    private float delay;
    public float campdist = 5f;
    public float fireRate = 2;

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
        delay += Time.deltaTime;
        if (Vector2.Distance(enemyPos, targetPos) > campdist)
        {
            transform.position = Vector2.MoveTowards(enemyPos, targetPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            if (delay > fireRate)
            {
                GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, transform.rotation);
                delay = 0;
            }
        }
        Vector2 aimDirection = new Vector3(targetPos.x, targetPos.y, 0) - new Vector3(transform.position.x, transform.position.y, 0);
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        transform.up = aimDirection.normalized * Time.deltaTime;
        // transform.LookAt(target, new Vector3(0,0,1));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        { 
            Player player = collision.collider.GetComponentInParent<Player>();
            player.Damage(5);
            Vector2 direction = (collision.collider.transform.position - transform.position).normalized;
            collision.collider.GetComponent<Rigidbody2D>().AddForce(direction * knockbackForce * 100, ForceMode2D.Force);
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
        throw new System.NotImplementedException();
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
