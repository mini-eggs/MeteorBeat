using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UsePowerup(string tag)
    {
        //Will call different commands depending on the powerup type
        //Debug.Log("TAG: "+tag);

        if (tag == "Health")
        {

            //AddHealth(heal);
        }
        else if (tag == "ScoreMultiply")
        {
            //multiplyScore();
        }
        else if (tag == "Invincibility")
        {
            //Invincibility();
        }
        else //Capsule.tag == "Super"
        {
            //AddHealth();
            //multiplyScore();
            //Invincibility();
        }
    }
}
