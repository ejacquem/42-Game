using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    bool friendly;
    [SerializeField]
    private int bulletDamage = 5;
    [SerializeField]
    private int bulletSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, bulletSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (friendly && collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(bulletDamage);
            //Destroy(gameObject);
        }
        else if (!friendly && collision.gameObject.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            player.Damage(bulletDamage);
            //Destroy(gameObject);
        }
    }
}
