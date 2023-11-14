using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    private Vector3 direction = Vector3.zero;
    private Vector3 position = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    private float height;
    private float width;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;

        //calculating the height and width of the screen
        camera = Camera.main;
        height = 2f * camera.orthographicSize;
        width = height * camera.aspect;

        //setting the look rotation of the player
        transform.rotation = new Quaternion(0, 1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //performing velocity math
        velocity = direction * speed * Time.deltaTime;

        //appliying the velocity to position
        position += velocity;

        //making it so that player cant go off the sides
        if (this.position.x > width)
        {
            position.x = width;
        }
        else if (this.position.x < (width - width))
        {
            position.x = width - width;
        }

        if (this.position.y > height)
        {
            position.y = height;
        }
        else if (this.position.y < (height - height))
        {
            position.y = height - height;
        }

        //setting the player's position to the calculated position
        transform.position = position;
    }

    public void SetDirection(Vector3 newDirection)
    {
        if (newDirection != null)
        {
            direction = newDirection.normalized;
        }
    }
}
