using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileController : MonoBehaviour
{
    private bool isFiring;

    [SerializeField]
    private SpriteInfo projectileInfo;

    [SerializeField]
    private SpriteInfo playerInfo;
    [SerializeField]
    private GameObject projectileContainer;


    private List<SpriteInfo> projectiles;
    public List<SpriteInfo> PlayerProjectiles { get { return projectiles; } }

    public bool IsFiring
    {
        set { isFiring = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        isFiring = false;

        projectiles = new List<SpriteInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFiring && projectiles.Count < 6)
        {
            //creating the projectile
            SpriteInfo projectile = Instantiate(projectileInfo);

            //adding the projectile to the list and setting its position
            projectiles.Add(projectile);
            projectile.transform.position = playerInfo.transform.position;

            //setting the container object
            if (projectileContainer != null)
            {
                projectile.transform.parent =
                    projectileContainer.transform;
            }

            //setting isFiring back to false
            isFiring = false;
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
