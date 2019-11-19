using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* A subclass of powerup
 * Only adds points to the score 
 * if they can click on it.
 */
public class MiniPowerup : Powerup
{
    public float coolDown = 1f;
    float delayTimer = 0;

    void Start()
    {
        Physics.queriesHitTriggers = true;
    }

    void Update()
    {
        delayTimer += Time.deltaTime;

        if (Input.GetKeyDown("space") )
        {
            if(delayTimer < coolDown)
            {
                //Do nothing
            }
            else
            {
                delayTimer = 0;
                StartCoroutine("MiniPower");
            }
        }
    }


    // Creates a new powerup that only adds to score
    IEnumerator MiniPower()
    {
        Vector3 spaceshipPosition = GameObject.Find("Spaceship").transform.position;
        GameObject mini = Instantiate(Capsule);
        mini.name = "MiniPower";
        PowerupType(mini);
        if (mini.GetComponent<Renderer>())
        {
            mini.GetComponent<Renderer>().material.color = Color.green;
        }        
        RelocatePowerup(spaceshipPosition);
        Destroy(mini,20);
        yield return null;
    }

    //Dynamic binding of how much to add to the score with this mini powerup 
    public override void PowerupType(GameObject mini)
    {
        mini.tag = "Score";
        scoreToAdd = 20;
    }

    //When the player clicks the mini powerup
    public virtual void OnMouseDown()
    {
        if(this.name == "MiniPower")
        {
            PowerupControl.UsePowerup(this.name, 20);
            //AddScore();
            Destroy(this.gameObject);
        }
    }
}
