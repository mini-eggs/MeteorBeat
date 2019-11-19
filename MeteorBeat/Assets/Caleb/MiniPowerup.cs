using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPowerup : Powerup
{
    public float coolDown = 1f;
    float delayTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        Physics.queriesHitTriggers = true;
    }

    // Update is called once per frame
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

    IEnumerator MiniPower()
    {
        //yield return new WaitForSeconds(1);
        Vector3 spaceshipPosition = GameObject.Find("Spaceship").transform.position;
        GameObject mini = Instantiate(Capsule);
        mini.name = "MiniPower";
        mini.tag = "Score";
        if (mini.GetComponent<Renderer>())
        {
            mini.GetComponent<Renderer>().material.color = Color.green;
        }        
        RelocatePowerup(spaceshipPosition);
        Destroy(mini,20);
        yield return null;
    }

    public virtual void OnMouseDown()
    {
        if(this.name == "MiniPower")
        {
            //AddScore();
            Destroy(this.gameObject);
        }
    }


}
