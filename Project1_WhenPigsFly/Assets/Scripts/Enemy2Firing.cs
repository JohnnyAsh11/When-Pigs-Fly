using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Firing : MonoBehaviour
{
    [SerializeField]
    private SpriteInfo diagonalProjectile;

    [SerializeField]
    private SpriteInfo self;

    private List<SpriteInfo> projectiles = new List<SpriteInfo>();

    private float time;
    private GameObject projContainer;

    public GameObject ProjectileContainer { set { projContainer = value; } }

    public List<SpriteInfo> Projectiles { get { return projectiles; } }

    // Update is called once per frame
    void Update()
    {
        float randValue = Random.Range(0f, 1f);
        time += Time.deltaTime;


        //fires one projectile every second
        if (time >= 1 && projectiles.Count < 6)
        {            
            SpriteInfo projectile = Instantiate(this.diagonalProjectile);
            DiagonalProjectileMovement projMovement;

            projectiles.Add(projectile);
            projectile.transform.position = self.transform.position;

            //setting the projectiles direction and which way it faces
            projMovement = projectile.GetComponent<DiagonalProjectileMovement>();

            //50% chance it goes diagonally up
            if (randValue < .5)
            {
                projMovement.Direction = new Vector2(-1, 1);
                projMovement.LookRotation = Quaternion.Euler(0, 0, -45);
            }
            //50% chance it goes diagonally down
            else
            {
                projMovement.Direction = new Vector2(-1, -1);
                projMovement.LookRotation = Quaternion.Euler(0, 0, 45);
            }

            //projMovement.ProjRenderer.flipX = true;
            //projMovement.ProjRenderer.color = Color.red;

            //setting the projectile's container object
            if (projContainer != null)
            {
                projectile.transform.parent = projContainer.transform;
            }

            //resetting the time counter
            time = 0;
        }

        //handling expired projectiles
        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            if (projectiles[i].CanDestroy)
            {
                DestroyProjectile(projectiles[i]);
            }
        }
    }

    private void DestroyProjectile(SpriteInfo projectile)
    {
        projectiles.Remove(projectile);
        Destroy(projectile.gameObject);
    }
}
