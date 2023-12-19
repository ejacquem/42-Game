using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float knockbackForce = 5f;
    BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if(collision.collider.tag == "Player")
        { 
            Player player = collision.collider.GetComponentInParent<Player>();
            player.Damage(5);
            Vector2 direction = (collision.collider.transform.position - transform.position).normalized;
            Debug.Log(direction);
            collision.collider.GetComponent<Rigidbody2D>().AddForce(direction * knockbackForce * 100, ForceMode2D.Force);
        }
    }
    
}
