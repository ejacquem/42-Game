using System.Collections;
using TMPro;
using UnityEngine;

public class Dash : MonoBehaviour
{
    Player player;
    [SerializeField]
    float dashCooldown, dashStunt, dashSpeed;
    float timer;
    bool dashing = false;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        timer = dashCooldown;
    }

    void Update()
    {
        if(!dashing && timer <= dashCooldown)
            timer += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Space) && timer >= dashCooldown)
            StartCoroutine(DashMove());

    }

    private IEnumerator DashMove()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        timer = 0;
        player.dashing = true;
        dashing = true;
        rb.velocity = player.GetSpeed() * dashSpeed;
        yield return new WaitForSeconds(dashStunt);
        GetComponent<BoxCollider2D>().isTrigger = false;
        player.dashing = false;
        dashing = false;
    }
}
