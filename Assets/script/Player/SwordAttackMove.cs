using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordAttackMove : MonoBehaviour
{
    [SerializeField]
    float cooldown;
    float timer;
    [SerializeField]
    GameObject swordPrefab;
    bool attacking = false;
    [SerializeField]
    Slider swordSlider;
    [SerializeField]
    Image swordImage;
    private void Start()
    {
        timer = cooldown;
        swordSlider.maxValue = cooldown;
    }
    void Update()
    {
        swordSlider.value = timer;
        if(timer <= cooldown)
        {
            swordImage.color = Color.red;
            timer += Time.deltaTime;
        }
        else
        {
            swordImage.color = Color.green;
        }
        if(Input.GetMouseButtonDown(1) && timer >= cooldown && attacking == false)
        {
            timer = 0;
            Instantiate(swordPrefab, transform);
        }       
    }
    IEnumerator Attack()
    {
        attacking = true;
        yield return new WaitForSeconds(1);
        attacking = false;
    }
}
