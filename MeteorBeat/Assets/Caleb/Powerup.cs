using UnityEngine;

/* Caleb Seely
 * Powerup prefab code w/ stress test
 * 10/10/2019
 * Breaks coding standard -> b.c.s
 */

public class Powerup : MonoBehaviour
{   // Capsule b.c.s to emphasize its critical importance to this class
    public GameObject Capsule; //The powerup object
    public int scoreToAdd = 200;
    public int powerupSpawnRate = 100;
    public bool STRESS = false;

    void Start()
    {    
        if(Capsule.tag == ("Untagged")) //Only needed for stress testing
        {   
            if(STRESS == true)
            {
                TEST();                 //STRESS TEST (See TEST() )
            }              
        }
        Capsule = GameObject.Find("Powerup");
    }

    private void Update()
    {
        //Move powerup if player is past it
        Vector3 spaceshipPosition = GameObject.Find("Spaceship").transform.position;
        Vector3 PowerupPosition = GameObject.Find("Powerup").transform.position;
        if (PowerupPosition.z +10 < spaceshipPosition.z)
        {
            //Debug.Log("ZZ: " + spaceshipPosition + PowerupPosition);
            RelocatePowerup(spaceshipPosition);
            //Debug.Log("Capsule Passed...moving ahead");
        }
    }

    /* Only called when ther is a collision with spaceship and Powerup
     * 'Uses' the powerup & relocates it 
     */
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Collision");
            Vector3 SpaceshipPosition = GameObject.Find("Spaceship").transform.position;
            PowerupControl.UsePowerup(Capsule.tag, scoreToAdd);
            //Relocate powerup to later in the game (Bigger Z axis number)
            RelocatePowerup(SpaceshipPosition);     
        }

    }

    /* This will move the powerup beyond the z position on the ship
     * and change which type of powerup it is.
     */
    public virtual void RelocatePowerup(Vector3 shipPosition)
    {
        PowerupType(Capsule);                                      //Set powerup type
        Vector3 Location = GetRandom.NewCoordinates();      //Set new 'random' coordinates
        Location.x += shipPosition.x;
        Location.y += shipPosition.y;
        Location.z += shipPosition.z + powerupSpawnRate;                           //Powerup is at least 20 units ahead of the player when its relocated
        Capsule.transform.position = Location;              //Set new location
        //Debug.Log("Capsule moved to " + Location );
    }

    //Randomly Selects the type of powerup (tag) it will be 
    public virtual void PowerupType(GameObject Capsule)
    {
        int x = GetRandom.GetRand(2);

        if (x % 2 == 0)
        {
            Capsule.tag = "Health";
        }
        else if (x % 2 == 1)
        {
            Capsule.tag = "Score";
        }

    }

    /* STRESS TEST ------------------
     * Function only used for testing!
     * B.c.s to emphasize it is not standard code.
     *
     * From 30+ fps, my machine can handle about 512 powerup objects 
     * From <20 fps, my machine can handle about 1024+ powerup objects 
     */

     void TEST()   
    {             
        Debug.Log("STRESS TEST");
        int finalCount = 0;

        for (int i = 0; i < 500; i++)
        {
            GameObject anotherCapsule = Instantiate(Capsule);
            Vector3 Location = GetRandom.NewCoordinates();
            anotherCapsule.transform.position = Location;   
            anotherCapsule.tag = "Health";  //set so clone no longer looks like the parent. 
            finalCount++;
            anotherCapsule.GetComponent<Renderer>().material.color = Color.green;
        }
        Debug.Log("Final spawn count: " +finalCount);
    }


}


