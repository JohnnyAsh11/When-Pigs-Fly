using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalProjectileMovement : MonoBehaviour
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

    public Quaternion LookRotation
    {
        get { return transform.rotation; }
        set { transform.rotation = value; }
    }


    public SpriteRenderer ProjRenderer { get { return selfRenderer; } }

    void Start()
    {
        //default velocity
        velocity = new Vector2(4, 4);
        position = transform.position;

        //default look rotation
        //transform.rotation = Quaternion.Euler(0, 0, 45);
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == Vector2.zero)
        {
            //default direction
            direction = new Vector2(-1, -1);
        }

        position += velocity * direction * Time.deltaTime;

        transform.position = position;
    }
}
