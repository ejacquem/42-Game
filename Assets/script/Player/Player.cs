using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public int maxHealth = 100;
    public bool dashing = false;
    [SerializeField]
    public AudioClip shootingSound; // Ajoutez cette ligne
    public AudioClip damageSound; // Ajoutez cette ligne
    [SerializeField]
    bool mainMenu = true;

    void Start()
    {
        if(mainMenu)
        {
            CheckIfMainMenuExist();
        }
        ammo = maxAmmo;
        health = 100;
        movement = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        delay = fireRate;
    }

    void CheckIfMainMenuExist()
    {
        if (MenuManager.Instance == null)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vect.x = Input.GetAxisRaw("Horizontal");
        vect.y = Input.GetAxisRaw("Vertical");

        delay += Time.deltaTime;

        if(ammo > 0 && Input.GetMouseButton(0) && delay > fireRate && GetComponent<BuildingScript>().buildingMode == false)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rb.transform.rotation);
        AudioSource.PlayClipAtPoint(shootingSound, transform.position);
        delay = 0;
        AddAmmo(-1);
    }

    private void FixedUpdate()
    {
        
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
        if(dashing == false)
            rb.AddForce(1000 * Time.fixedDeltaTime * GetSpeed());
    }

    public int GetAmmo()
    { return ammo; }

    public int GetHealth()
    { return health; }

    public Vector2 GetSpeed()
    { return (vect.normalized * velocity); }

    public void Damage(int damage)
    {
        AudioSource.PlayClipAtPoint(damageSound, transform.position);
        health -= damage;
        if (health <= 0)
        {
            MenuManager.Instance.LoadGameOver();
            Destroy(gameObject);
        }
        UIManager.instance.UpdateUIHealth(health);
    }

    public void AddAmmo(int nbr)
    {
        ammo += nbr;
        UIManager.instance.UpdateUIAmmo(ammo);
    }

    public void AddMaxHealth(int nbr)
    {
        maxHealth += nbr;
        UIManager.instance.UpdateUIMaxHealth(maxHealth);
    }
    
    public void AddSpeed(float nbr)
    {
        velocity += nbr;
        //UIManager.instance.UpdateUISpeed(velocity);
    }
    public void AddFireRate(float nbr)
    {
        fireRate *= 1 - nbr/100;
        //UIManager.instance.UpdateUISpeed(velocity);
    }
}
