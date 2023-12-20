using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float delay = 8;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= delay)
            {
                Destroy(gameObject);
            }
        timer += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            col.GetComponent<Player>().AddAmmo(5);
            Destroy(gameObject);
        }
    }
}
