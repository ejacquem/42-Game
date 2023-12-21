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
    
    void Start()
    {
        ammo = maxAmmo;
        health = 100;
        movement = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        delay = fireRate;
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vect.x = Input.GetAxisRaw("Horizontal");
        vect.y = Input.GetAxisRaw("Vertical");

        delay += Time.deltaTime;

        if(ammo > 0 && Input.GetMouseButton(0) && delay > fireRate){
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rb.transform.rotation);
            delay = 0;
            AddAmmo(-1);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(vect.normalized * 1000 * velocity * Time.fixedDeltaTime);
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    public int GetAmmo()
    { return ammo; }

    public int GetHealth()
    { return health; }

    public Vector2 GetSpeed()
    { return (vect.normalized * velocity); }

    public void Damage(int damage)
    {
        Debug.Log("player hit");
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        UIManager.instance.UpdateUIHealth(health);
    }

    public void AddAmmo(int nbr)
    {
        ammo += nbr;
        UIManager.instance.UpdateUIAmmo(ammo);
    }

}
