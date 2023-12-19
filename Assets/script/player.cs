using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private Transform movement;
    private Rigidbody2D rb;
    private Vector2 vect;
    public Vector2 mousePosition;
    public float velocity = 0;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private float delay;
    public float fireRate = 2;
    private int ammo, health;
    public int maxAmmo = 20;
    public TextMeshProUGUI ammotxt;
    public TextMeshProUGUI healthtxt;
    public TextMeshProUGUI speedtxt;
    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo;
        health = 100;
        ammotxt.text = ammo.ToString();
        movement = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        delay = fireRate;
        Debug.Log(movement.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vect.x = Input.GetAxisRaw("Horizontal");
        vect.y = Input.GetAxisRaw("Vertical");

        delay += Time.deltaTime;
        healthtxt.text = health.ToString();
        speedtxt.text = (vect.normalized*velocity).ToString();

        if(ammo > 0 && Input.GetMouseButton(0) && delay > fireRate){
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rb.transform.rotation);
            ammo--;
            health--;
            ammotxt.text = ammo.ToString();
            delay = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(vect.normalized * 1000 * velocity * Time.fixedDeltaTime);
        //pointer.AddForce(vect.normalized * 1000 * velocity * Time.fixedDeltaTime);
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        Debug.Log(aimAngle);
        rb.rotation = aimAngle;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
            Destroy(gameObject);
    }

    public void Damage(int damage)
    {
        health -= damage;
    }

}
