using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    private SpriteInfo player;
    private float time;

    private List<SpriteInfo> enemyProjectiles;

    [SerializeField]
    private EnemySpawner enemySpawner;
    [SerializeField]
    private ProjectileController projectileController;

    //information for powerups
    [SerializeField]
    private PowerupSpawner powerupSpawner;
    [SerializeField]
    private TriFire playerPowerup;
    private SpriteRenderer playerRenderer;

    //UI Info
    private float health;
    private float score;

    private List<SpriteInfo> enemies;
    private List<SpriteInfo> playerProjectiles;

    public float Health { get { return health; } }
    public float Score { get { return score; } }

    private void Start()
    {
        health = 3f;
        enemies = new List<SpriteInfo>();
        enemyProjectiles = new List<SpriteInfo>();
        playerProjectiles = new List<SpriteInfo>();
        playerRenderer = player.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //------------------------------------------------------
        //  getting all of the collidable objects
        playerProjectiles.AddRange(projectileController.PlayerProjectiles);
        enemies.AddRange(enemySpawner.Enemies);

        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy2Firing enemy2Proj;
            EnemyFiring enemyProj =
                enemies[i].GetComponent<EnemyFiring>();

            //if it is null then it must be enemy2
            if (enemyProj == null)
            {
                enemy2Proj = enemies[i].GetComponent<Enemy2Firing>();
                enemyProjectiles.AddRange(enemy2Proj.Projectiles);
            }
            else
            {
                enemyProjectiles.AddRange(enemyProj.Projectiles);
            }
        }
        //------------------------------------------------------

        //if any of the enemies are colliding with the player
        foreach (SpriteInfo enemy in enemies)
        {
            if (BoundingCircleCheck(enemy, player))
            {
                score += 2.5f;
                enemy.IsColliding = true;
                player.IsColliding = true;
                continue;
            }
            else
            {
                enemy.IsColliding = false;
            }
        }

        //if any of the enemies are colliding with player projectiles
        //adding the powerup projectiles to the list
        playerProjectiles.AddRange(playerPowerup.Projectiles);
        foreach (SpriteInfo enemy in enemies)
        {
            foreach (SpriteInfo playerProj in playerProjectiles)
            {
                if (BoundingCircleCheck(enemy, playerProj))
                {
                    score += 10f;
                    enemy.IsColliding = true;
                    continue;
                }
            }
        }

        //if any of the enemy projectiles are colliding with the player
        foreach (SpriteInfo enemyProj in enemyProjectiles)
        {
            if (BoundingCircleCheck(enemyProj, player))
            {
                enemyProj.IsColliding = true;
                player.IsColliding = true;

                continue;
            }
            else
            {
                enemyProj.IsColliding = false;
            }
        }

        //checking if the player got a powerup
        if (powerupSpawner.ActivePowerup != null && 
            BoundingCircleCheck(player, powerupSpawner.ActivePowerup))
        {
            score += 50f;
            playerPowerup.IsTriFire = true;
            Destroy(powerupSpawner.ActivePowerup.gameObject);
        }

        if (player.IsColliding)
        {
            playerRenderer.color = Color.red;
            time = .1f;

            health -= .5f;
            player.IsColliding = false;
        }

        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else if (time <= 0)
        {
            playerRenderer.color = Color.white;
        }

        //clear the lists
        enemyProjectiles.Clear();
        playerProjectiles.Clear();
        enemies.Clear();
    }

    bool BoundingCircleCheck(SpriteInfo spriteA, SpriteInfo spriteB)
    {
        //getting the sum of both SpriteInfo Radii
        float sumOfRadii = spriteA.Radius + spriteB.Radius;

        //saving center locations to variables
        Vector3 location1 = spriteA.transform.position;
        Vector3 location2 = spriteB.transform.position;

        //Getting the distance between the Vectors
        float distance = Vector3.Distance(location1, location2);

        //checking the the sum of the radii is greater than the distance
        if (distance < sumOfRadii)
        {
            return true;
        }

        return false;
    }
}
