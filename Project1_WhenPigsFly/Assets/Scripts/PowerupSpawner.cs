using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField]
    private SpriteInfo powerup;
    private float time;

    private SpriteInfo activePowerup;

    public SpriteInfo ActivePowerup { get { return activePowerup; } }

    // Update is called once per frame
    void Update()
    {
        //add the change in time since last frame to time
        time += Time.deltaTime; 

        //if time is > or = 15 then 15 seconds have passed
        if (time >= 15)
        {
            //spawn a powerup
            activePowerup = Instantiate(powerup);
            time = 0;
        }
    }
}
