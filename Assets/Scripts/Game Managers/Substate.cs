using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Substate : MonoBehaviour
{
    [SerializeField] private Transform entryPoint;
    [SerializeField] private List<Enemy> data;
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private int numOfWaves;
    private List<Waves> enemyWaves = new List<Waves>();
    private List<Enemy> enemies;
    private Substate nextState;
    private Waves currentWave;
    private bool waveDefeated = false;


    void Start()
    {
        enemies = data;
        GenerateEnemies();
    }

    void Update()
    {
        if (currentWave.EnemyLength <= 0)
            waveDefeated = true;
    }

    public void GenerateEnemies()
    {
        for (int i = 0; i < numOfWaves; i++)
        {
            Waves wave = new Waves();
            int numOfEnemies = UnityEngine.Random.Range(0, 11);
            for (int j = 0; j < numOfEnemies; j++)
            {
                int rNum1 = UnityEngine.Random.Range(0, enemies.Count);
                int rNum2 = UnityEngine.Random.Range(0, enemies.Count);

                Enemy e1 = enemies[rNum1];
                Enemy e2 = enemies[rNum2];
                Enemy result = CompareWeight(e1, e2);

                wave.AddEnemy(result);
            }
            enemyWaves.Add(wave);
        }
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        StructExtensions.Shuffle(spawnPoints);

        foreach (Waves wave in enemyWaves)
        {
            currentWave = wave;
            for (int i = 0; i < wave.EnemyLength; i++)
            {
                GameObject spawnPoint = spawnPoints[i];
                Enemy e = Instantiate(wave.Enemies[i], spawnPoint.transform.position, quaternion.identity);
                e.ParentWave = currentWave;
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitUntil(() => waveDefeated = true);
        }
        StopCoroutine(SpawnEnemies());
    }


    public Enemy CompareWeight(Enemy e1, Enemy e2)
    {
        if (e1.Priority == e2.Priority)
            return e1;
        Enemy e3 = e1.Priority > e2.Priority ? e1 : e2;
        return e3;
    }
    public Substate NextState { get { return nextState; } set { nextState = value; } }
}
