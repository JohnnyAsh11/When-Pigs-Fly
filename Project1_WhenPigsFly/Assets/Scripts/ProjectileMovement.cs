using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer selfRenderer;

    private Vector2 position;
    private Vector2 velocity;
    private Vector2 direction;

    public Vector2 Direction
    {
        set { direction = value; }
    }

    public SpriteRenderer ProjRenderer { get { return selfRenderer; } }

    void Start()
    {
        //default velocity
        velocity = new Vector2(4, 4);
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == Vector2.zero)
        {
            //default direction
            direction = Vector2.right;
        }

        position.x += velocity.x * direction.x * Time.deltaTime;

        transform.position = position;
    }
}
