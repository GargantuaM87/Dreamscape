using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    private List<Enemy> enemies = new List<Enemy>();
    private int enemyLength;

    public void AddEnemy(Enemy e)
    {
        enemies.Add(e);
        enemyLength++;
    }
    public List<Enemy> Enemies { get { return enemies; } set { enemies = value; } }
    public int EnemyLength { get { return enemyLength; } set { enemyLength = value; } }
}
