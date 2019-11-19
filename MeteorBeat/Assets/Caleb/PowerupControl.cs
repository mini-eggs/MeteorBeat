using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Command Design pattern
 * This class takes a command from the powerup
 * and tells the health or score to do something
 * depending on the tag it was sent. * 
 */
public class PowerupControl : MonoBehaviour
{
    public static void UsePowerup(string tag, int scoreToAdd )
    {
        //Will call different commands depending on the powerup type
        if (tag == "Health")
        {
            //Ship.health += 1; 
        }
        else if (tag == "Score")
        {
            //Ship.score += scoreToAdd;
        }
        else if (tag == "Super")
        {
            //Ship.health += 20;
            //Ship.score += 500;
        }
    }
}
