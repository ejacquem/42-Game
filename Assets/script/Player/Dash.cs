using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    Player player;
    [SerializeField]
    float dashCooldown, dashStunt, dashSpeed;
    [SerializeField]
    Slider dashSlider;
    [SerializeField]
    Image dashImage;
    float timer;
    bool dashing = false;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        timer = dashCooldown;
        dashSlider.maxValue = dashCooldown;
    }

    void Update()
    {
        dashSlider.value = timer;
        if(!dashing && timer <= dashCooldown)
        {
            timer += Time.deltaTime;
            dashImage.color = Color.red;
        }
        else
        {
            dashImage.color = Color.green;
        }

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
