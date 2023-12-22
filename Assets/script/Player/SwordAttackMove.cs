using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackMove : MonoBehaviour
{
    [SerializeField]
    float cooldown;
    float timer;
    [SerializeField]
    GameObject swordPrefab;
    bool attacking = false;
    private void Start()
    {
        timer = cooldown;
    }
    void Update()
    {
        if(timer <= cooldown)
            timer += Time.deltaTime;
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
