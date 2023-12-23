using System.Collections;
using UnityEditor;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public static BonusData bonusData; // Reference to your ScriptableObject
    public float damage;
    public float health;
    public float maxHealth;
    public float moveSpeed;
    public float knockbackForce;
    public Transform target;

    public void Start()
    {
        if (bonusData == null)
        {
            //bonusData = Resources.Load<BonusData>("Bonus/BonusData");
            bonusData = AssetDatabase.LoadAssetAtPath<BonusData>("Assets/script/Bonus/BonusData.asset");

            if (bonusData == null) Debug.LogError("Can't find bonus data");
        }
        
        health = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Update()
    {
        if(target != null){
            Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 targetPos = new Vector2(target.position.x, target.position.y);
            transform.position = Vector2.MoveTowards(enemyPos, targetPos, moveSpeed * Time.deltaTime);
        }
    }

    public abstract void TakeDamage(float damage);
    public abstract void Attack(Player player);
    
    public void Die()
    {
        Destroy(gameObject);
        if(bonusData != null){
            int randomIndex = Random.Range(0, bonusData.bonusPrefabs.Length);
            GameObject selectedBonus = bonusData.bonusPrefabs[randomIndex];
            Instantiate(selectedBonus, transform.position, Quaternion.identity);
        }
        
    }
    
}
