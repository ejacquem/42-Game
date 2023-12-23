using System.Collections;
using UnityEngine;

public class EnemyCreeper : Enemy
{
    public SpriteRenderer rendererX;
    public GameObject explosionPrefab;
    
    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
            Explode();
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

    private void Explode()
    {
        Instantiate(explosionPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Die();
    }

    public override void TakeDamage(float damage)
    {        
        health -= damage;
        StartCoroutine(ChangeColor());
        if (health <= 0)
        {
            UIManager.instance.AddScore(2);
            Explode();
        }
    }

    public override void Attack(Player player)
    {
        Explode();
    }
}
