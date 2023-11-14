using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    private Vector2 position;

    [SerializeField]
    private Vector2 velocity = Vector2.one;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        position.x -= velocity.x * Time.deltaTime;

        if (position.x >= 8)
        {
            transform.position = position;
        }
    }
}
