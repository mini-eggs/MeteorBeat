using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AsteroidIntegration
 *
 * Meta class. Simply play level soundtrack when level begins and stop 
 * playing when level is over. Connect this script to the top level 
 * scene within Unity editor.
 */

public class score : MonoBehaviour
{
   /* Cosette's addition: I added a health mechanism
    * the health says 5 however with the if statement it will be 6 lives
    */
   static int health = 5;

   // The ship itself.
   private Rigidbody spaceship;

   /* 
    * Start
    *
    * Setup spaceship for use in `OnTriggerEnter`
    */
   void Start() 
   {
      spaceship = GameObject.FindGameObjectWithTag("Player")
         .GetComponent<Rigidbody>();
   }

   /* 
    * OnTriggerEnter
    *
    * When user an asteroid hits the user call 
    * ShipIntegration#CollideWithAsteroid that will handle playing 
    * sound/particle effects/other.
    */
   void OnTriggerEnter()
   {
      if (health > 0)
      {
         health--;

         // Create explosion particles while travelling through
         // asteroid.
         ParticleFactory.Get(ParticleType.Collision).Run(spaceship);

         // Play game over sounds.
         BeatBox.Instance.PlayCollision();
      }
      else
      {
         health = 5;
         GameObject.FindGameObjectWithTag("Player").GetComponent<ShipIntegration>().CollideWithAsteroid();
      }
   }
}