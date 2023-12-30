using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNM : MonoBehaviour
{
    [SerializeField] Transform target;
    public SpriteRenderer rendererX;
    public float damage;
    public float health;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        StartCoroutine(ChangeColor());
        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator ChangeColor()
    {
        rendererX.color = Color.white;
        yield return new WaitForSeconds(0.09f);
        rendererX.color = Color.red;
        yield return new WaitForSeconds(0.09f);
        rendererX.color = Color.white;
        yield return new WaitForSeconds(0.09f);
        rendererX.color = Color.red;;
    }
    
    public void Die()
    {
        Destroy(gameObject);
    }

}
