using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupMovement : MonoBehaviour
{
    private Vector2 position;

    [SerializeField]
    private Vector2 velocity = Vector2.one;
    private Vector2 direction = Vector2.one;

    private float height;
    private float width;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;

        camera = Camera.main;
        height = 2f * camera.orthographicSize;
        width = height * camera.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        position -= direction * velocity * Time.deltaTime;

        if (position.x <= (width - width))
        {
            Destroy(gameObject);
        }

        if (position.y > height)
        {
            direction.y = -1;
        }
        else if (position.y < (height - height))
        {
            direction.y = 1;
        }

        transform.position = position;
    }
}
