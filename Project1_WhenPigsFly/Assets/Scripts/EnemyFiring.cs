using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    [SerializeField]
    private SpriteInfo projectile;

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
        time += Time.deltaTime;

        //fires one projectile every second
        if (time >= 1 && projectiles.Count < 6)
        {
            time = 0;

            SpriteInfo projectile = Instantiate(this.projectile);
            ProjectileMovement projMovement;

            projectiles.Add(projectile);
            projectile.transform.position = self.transform.position;

            //setting the projectiles direction and which way it faces
            projMovement = projectile.GetComponent<ProjectileMovement>();
            projMovement.Direction = Vector2.left;
            projMovement.ProjRenderer.flipX = true;
            projMovement.ProjRenderer.color = Color.red;

            //setting the projectile's container object
            if (projContainer != null)
            {
                projectile.transform.parent = projContainer.transform;
            }
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
