using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float damage;
    public float health;
    public float maxHealth;
    public float moveSpeed;
    public float knockbackForce;

    public abstract void TakeDamage(float damage);
    public abstract void Attack(Player player);
    public abstract void Die();
}
