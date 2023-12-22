using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform playerPos;
    public float minRange = 50;
    public float maxRange = 100;
    [SerializeField]
    public GameObject[] enemyPrefab;
    public float delay = 1;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if (timer >= delay)
        {
            SpawnEnemy();
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    void SpawnEnemy()
    {
        float angle = Mathf.Deg2Rad * Random.Range(0, 360);
        float range = Random.Range(minRange, maxRange);
        Vector2 spawnPos = new Vector2(playerPos.position.x + Mathf.Cos(angle) * range, playerPos.position.y + Mathf.Sin(angle) * range);

        Instantiate(enemyPrefab[Random.Range(0,enemyPrefab.Length)], spawnPos, Quaternion.identity);
    }
}
