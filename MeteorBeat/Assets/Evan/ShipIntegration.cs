using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ShipIntegration
 *
 * Meta class for simply connecting the particle effects to the ship. 
 * Connect this class to the ship within Unity editor.
 */
public class ShipIntegration : MonoBehaviour
{

   // The ship itself.
   private Rigidbody spaceship;

   // The ship's thrusters.
   private Particle thrusters;
   // health and score variables (cosette)
   UpdateHealth myHealth;
   UpdateScore myScore;
   public int score = 0;
   public int health = 5;

   /*
    * Start
    *
    * Grab ship rigidbody, create the desired particle type (specified 
    * in Unity editor) and begin displaying particles to ship.
    */
   void Start()
   {
      // Grab spaceship from attached game object.
      spaceship = GetComponent<Rigidbody>();

      // Create ship thruster particles.
      thrusters = ParticleFactory.Get(ParticleType.Direction);
      //adding to the ship score and health (cosette)
      myHealth = GetComponent<UpdateHealth>();
      myScore = GetComponent<UpdateScore>();
   }

   void Update()
   {
      thrusters.Run(spaceship);
   }

   /*
    * CollideWithAsteroid
    *
    * User has collided with asteroid. Show explosion, remove ship from 
    * view, lock camera, and play game losing sound.
    */
   public void CollideWithAsteroid()
   {
      // Create explosion particles.
      ParticleFactory.Get(ParticleType.Collision).Run(spaceship);

      // Lock camera.
      /* 
       *not working
      GameObject.FindGameObjectWithTag("MainCamera")
         .GetComponent<CameraIntegration>()
         .Lock();
       */
      // Lock astoid creation states. Do NOT let them disappear from 
      // user view.
      
      GetComponent<keyControls>().movementSpeed = 0;
      GetComponent<keyControls>().flyingSpeed = 0;

      GetComponent<LevelGeneration>()
         .StopCoroutines();

      // Play game over sounds.
      var s = BeatBox.Instance;
      s.PlayGameLost();
      s.PlayCollision();

      // Hide spacehip by disabling all children renders.
      Renderer[] childs = this.gameObject
         .GetComponentsInChildren<Renderer>();
      foreach (Renderer item in childs)
      {
         item.enabled = false;
      }
   }

   /*
    * UserHasWon
    *
    * Varous cleanup activities when user has completed game 
    * successfully.
    */
   public void UserHasWon()
   {
      // Lock astoid creation states. Do NOT let them disappear from 
      // user view.
      GameObject.FindGameObjectWithTag("Player")
         .GetComponent<LevelGeneration>()
         .StopCoroutines();

      // Play game over sounds.
      BeatBox.Instance.PlayGameWon();
   }
   /*
   * Cosette's addition
   * Adds a health and score mechanism and attaches them to the GUI
   */
   
   public void AsteroidHit(){
      
      health--; 
      GameObject.FindGameObjectWithTag("Health").GetComponent<HealthTextClass>().UpdateUIElement((float)health/5f);
      if (health <= 0){
         CollideWithAsteroid();
      }
   }
   public void RingHit(){

   }


}