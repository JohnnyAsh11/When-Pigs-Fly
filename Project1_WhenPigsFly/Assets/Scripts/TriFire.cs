using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriFire : MonoBehaviour
{
    [SerializeField]
    private SpriteInfo diagonalProjectile;

    [SerializeField]
    private GameObject projectileContainer;

    private List<SpriteInfo> projectiles;

    private bool isTriFire;
    private float time;

    public bool IsTriFire
    {
        get { return isTriFire; }
        set { isTriFire = value; } 
    }

    public List<SpriteInfo> Projectiles { get {  return projectiles; } }

    // Start is called before the first frame update
    void Start()
    {
        projectiles = new List<SpriteInfo>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //homemade timer for the powerup (10 seconds)
        if (isTriFire)
        {
            time += Time.deltaTime;

            if (time >= 10)
            {
                isTriFire = false;
                time = 0;
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

    public void OnFire()
    {
        if (isTriFire && projectiles.Count < 14)
        {
            // ~~ variables ~~
            SpriteInfo projectileUp = Instantiate(this.diagonalProjectile);
            SpriteInfo projectileDown = Instantiate(this.diagonalProjectile);
            DiagonalProjectileMovement projUpMovement;
            DiagonalProjectileMovement projDownMovement;
            SpriteRenderer projUpRenderer;
            SpriteRenderer projDownRenderer;

            //adding the projectiles to the lists
            projectiles.Add(projectileUp);
            projectiles.Add(projectileDown);

            //setting their starting point
            projectileUp.transform.position = transform.position;
            projectileDown.transform.position = transform.position;

            //getting the movement controllers
            projUpMovement = projectileUp.GetComponent<DiagonalProjectileMovement>();
            projDownMovement = projectileDown.GetComponent<DiagonalProjectileMovement>();

            //Changing the projectile's colors
            projUpRenderer = projectileUp.GetComponent<SpriteRenderer>();
            projDownRenderer = projectileDown.GetComponent<SpriteRenderer>();
            projDownRenderer.color = Color.white;
            projUpRenderer.color = Color.white;

            //setting the projectiles direction and which way it faces
            projUpMovement.Direction = new Vector2(1, 1);
            projUpMovement.LookRotation = Quaternion.Euler(0, 0, -135);
            projDownMovement.Direction = new Vector2(1, -1);
            projDownMovement.LookRotation = Quaternion.Euler(0, 0, 135);

            //setting the projectile's container object
            if (projectileContainer != null)
            {
                projectileUp.transform.parent = projectileContainer.transform;
                projectileDown.transform.parent = projectileContainer.transform;
            }
        }
    }

    private void DestroyProjectile(SpriteInfo projectile)
    {
        projectiles.Remove(projectile);
        Destroy(projectile.gameObject);
    }
}
