﻿using System.Collections;
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

   /*
    * Start
    *
    * Grab ship rigidbody, create the desired particle type (specified 
    * in Unity editor) and begin displaying particles to ship.
    */
   void Start()
   {
      spaceship = GetComponent<Rigidbody>();

      // Create ship thruster particles.
      thrusters = ParticleFactory.Get(ParticleType.Direction);
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
      GameObject.FindGameObjectWithTag("MainCamera")
         .GetComponent<CameraIntegration>()
         .Lock();

      // Lock astoid creation states. Do NOT let them disappear from 
      // user view.
      GameObject.FindGameObjectWithTag("Player")
         .GetComponent<LevelGeneration>()
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
      // Lock astoid creation states. Do NOT let them disappear from user view.
      GameObject.FindGameObjectWithTag("Player")
         .GetComponent<LevelGeneration>()
         .StopCoroutines();

      // Play game over sounds.
      BeatBox.Instance.PlayGameWon();
   }

}