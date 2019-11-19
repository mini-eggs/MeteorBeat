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
            //AddHealth(heal);
        }
        else if (tag == "Score")
        {
            //multiplyScore();
        }
        else if (tag == "Super")
        {
            //Invincibility();
        }
    }
}
