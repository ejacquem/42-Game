using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
/*
The turret will choose the closest enemy and target it until it is destroyed or out of range.
*/
public class Turret : MonoBehaviour
{
    private float delay = 0;
    public float fireRate;
    public float detectionRadius;
    public GameObject bulletPrefab;
    public GameObject TurretHead;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //  Debug.Log(target);
        if(target == null)
            target = GetNearestEnemy();
        else
        {
            AimAtTarget();
            if(ReadyToShoot())
            {
                Shoot();
            }
        }
    }

    private void AimAtTarget()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 270;
        //Debug.Log(angle);
        TurretHead.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, TurretHead.transform.position, TurretHead.transform.rotation);
        //AudioSource.PlayClipAtPoint(shootingSound, transform.position);
        delay = 0;
    }

    bool ReadyToShoot()
    {
        delay += Time.deltaTime;
        return delay > fireRate;
    }

    Transform GetNearestEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);

                if (distance < closestDistance)
                {
                    closestEnemy = collider.transform;
                    closestDistance = distance;
                }
            }
        }
        return closestEnemy;
    }

}
