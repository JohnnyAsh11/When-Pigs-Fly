using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeUI
{
    Health,
    Score,
    GameOver
}

public class Health : MonoBehaviour
{

    [SerializeField]
    private TextMesh UI;

    [SerializeField]
    private CollisionManager collisionInfo;

    [SerializeField]
    private TypeUI typeOfUI;

    private void Start()
    {
        if (typeOfUI == TypeUI.GameOver)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                -50);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (typeOfUI == TypeUI.Health)
        {
            UI.text = $"{Mathf.Round(collisionInfo.Health)} Lives Remaining";
        }
        else if (typeOfUI == TypeUI.Score)
        {
            UI.text = $"Current Score: {collisionInfo.Score}";
        }

        //Ending the game
        if (collisionInfo.Health <= 0)
        {
            //move the gameover text up
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                0);
            Application.Quit();
        }
    }
}
