using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private SpriteInfo enemy1;
    [SerializeField]
    private SpriteInfo enemy2;

    private List<SpriteInfo> enemies;

    [SerializeField]
    private GameObject enemyContainer;
    [SerializeField]
    private GameObject projectileContainer;

    private float time;

    public List<SpriteInfo> Enemies {  get { return enemies; } }

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<SpriteInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        float randValue = Random.Range(0f, 1f);
        time += Time.deltaTime;

        //every 2 seconds spawn a new enemy
        if (time >= 2 && enemies.Count < 10)
        {
            float randomPositionY = Random.Range(0f, 10f);
            SpriteInfo enemy;
            EnemyFiring enemyFiring;
            Enemy2Firing enemy2Firing;

            //reset the time clock
            time = 0;

            //75% of the time spawn in a normal enemy
            if (randValue < .75)
            {
                enemy = Instantiate(this.enemy1);

                //setting the container element for the enemy's projectiles
                enemyFiring = enemy.GetComponent<EnemyFiring>();
                enemyFiring.ProjectileContainer = projectileContainer;
            }
            //25% of the time spawn a diagonal firing enemy
            else
            {
                enemy = Instantiate(this.enemy2);

                //setting the container element for the enemy's projectiles
                enemy2Firing = enemy.GetComponent<Enemy2Firing>();
                enemy2Firing.ProjectileContainer = projectileContainer;
            }

            //adding to the list and setting the random position
            enemies.Add(enemy);
            enemy.transform.position =
                new Vector2(15, randomPositionY);

            //setting the container element for the enemies
            enemy.transform.parent = enemyContainer.transform;
        }

        //handling eliminated enemies
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i].IsColliding)
            {
                DestroyEnemy(enemies[i]);
            }
        }
    }


    private void DestroyEnemy(SpriteInfo enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
