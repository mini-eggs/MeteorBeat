using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ShipIntegration
 *
 * Meta class for simply connecting the particle effects to the ship. Connect
 * this class to the ship within Unity editor.
 */
public class ShipIntegration : MonoBehaviour
{

    // Particle effect for ship exhaust.
    public Transform particleShipExhaust;

    // Particle effect for ship collision.
    public Transform particleShipAsteroidCollision;
    
    // The ship itself.
    private Rigidbody spaceship; 
    
    // The particle instance (from the ShipIntegration#effect).
    private Particle particle; 

    // User has _not_ hit an asteroid yet.
    private bool isPlaying;

    /*
     * Start
     *
     * Grab ship rigidbody, create the desired particle type (specified in Unity
     * editor) and begin displaying particles to ship.
     */
    void Start()
    {
      isPlaying = true;
      spaceship = GetComponent<Rigidbody>();
      particle = ParticleFactory.Get(ParticleType.Direction);
      particle.SetEffect(particleShipExhaust);
    }

    /*
     * Update
     *
     * Update position of the ship particles.
     */
    void Update()
    {
      particle.Run(spaceship.position);
      
      // // testing
      // if (spaceship.position.z > 50 && isPlaying)
      // {
      //   collideWithAsteroid();
      // }
    }


    /*
     * OnCollisionEnter
     *
     * Check if ship has collided with asteroid. If so lock camera so user sees
     * their explosion.
     */
    void OnCollisionEnter(Collision item) 
    {
      if(item.collider.CompareTag("Asteroid")) 
      {
        // We've hit an asteroid.
        collideWithAsteroid();
      }
    }

    /*
     * collideWithAsteroid
     *
     * User has collided with asteroid. Show explosion, remove ship from view,
     * lock camera, and play game losing sound.
     */
    private void collideWithAsteroid() 
    {
      // So we don't call this function multiple times.
      isPlaying = false;

      // Create explosion particles.
      particle = ParticleFactory.Get(ParticleType.Collision);
      particle.SetEffect(particleShipAsteroidCollision);
      particle.Run(spaceship.position);

      // Lock camera.
      GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraIntegration>().Lock();

      // Play game over sounds.
      var s = BeatBox.Instance;
      s.LoadSounds(this.gameObject);
      s.PlayGameLost();
      s.PlayCollision();

      // Hide spacehip by disabling all children renders.
      Renderer[] childs = this.gameObject.GetComponentsInChildren<Renderer>();
      foreach (Renderer item in childs)
      {
        item.enabled = false;
      }
    }

}
