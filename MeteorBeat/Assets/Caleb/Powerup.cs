using System;
using System.Collections;
using UnityEngine;
using System.Linq;

//Caleb Seely 

public class Powerup : MonoBehaviour
{
   // public GameObject pickupEffect;
    public GameObject Health;
       
    void Start()
    {
        //print("Hello");
    }

    private void Update()
    {
        Vector3 SpaceshipPosition = GameObject.Find("Spaceship").transform.position;
        Vector3 PowerupPosition = GameObject.Find("Health").transform.position;

        if (PowerupPosition.z < SpaceshipPosition.z)
        {
            Spawn(SpaceshipPosition.z);
            Debug.Log("Moved powerup");
        }
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");

        Vector3 SpaceshipPosition = GameObject.Find("Spaceship").transform.position;
        print("Cords: " + SpaceshipPosition);
        float Z_Cord = SpaceshipPosition.z;
        
        Pickup();
        Spawn(Z_Cord);
    }

    void Pickup()
    {
        //Will call different commands depending on the powerup type
    }

    void UsePowerup()
    {
        // PlayerStats stats = player.GetComponent<PlayerStats>();
        //stats.health = health + 20;
    }

    // This will move the powerup when hit or if the spaceship has passed it.
    void Spawn(float Z_Cord)
    {
        Vector3 Location = new Vector3(-5, 1, Z_Cord + 50);
        Health.transform.position = new Vector3(-5,1,Z_Cord+50);
        Debug.Log("Powerup moved");
    }
}
