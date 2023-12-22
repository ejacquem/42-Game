using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    bool friendly;
   // [SerializeField]
   // CircleCollider2D circleCollider;
    [SerializeField]
    private int bulletDamage = 5;
    [SerializeField]
    private int bulletSpeed = 5;
    [SerializeField]
    private AudioSource shootingSound;

    // Start is called before the first frame update
    void Start()
    {
        // test issam Pool
        shootingSound = AudioSourcePool.Instance.GetAudioSource();
        if (shootingSound != null)
        {
            shootingSound.Play();
        }
        Destroy(gameObject, 5);
        /*shootingSound = GameObject.Find("Audio Bullet").GetComponent<AudioSource>();
        shootingSound.Play(); // Jouez le son de tir ici*/
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, bulletSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy")
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
    // test issam :
    private void OnDestroy()
    {
        if (shootingSound != null)
        {
            AudioSourcePool.Instance.ReturnAudioSource(shootingSound);
        }
    }
}
