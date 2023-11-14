using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpriteInfo : MonoBehaviour
{
    [SerializeField]
    private float radius = 1f;

    [SerializeField]
    private SpriteRenderer renderer;

    [SerializeField]
    private bool isColliding = false;

    [SerializeField]
    private bool canDestroy;
    private float startingX;

    private Vector2 position;

    public float Radius { get { return radius; } }

    //CanDestroy is only relevant if the GameObject is a projectile
    public bool CanDestroy { get { return canDestroy; } }

    public bool IsColliding 
    { 
        get { return isColliding; }
        set { isColliding = value; } 
    }

    private void Start()
    {
        position = transform.position;
        startingX = position.x;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;

        if (isColliding ||
            position.x > startingX + 7 ||
            position.x < startingX - 7)
        {
            canDestroy = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
