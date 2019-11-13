using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Ship
 *
 * Meta class for simply connecting the particle effects to the ship.
 */
public class Ship : MonoBehaviour
{

    public Transform effect;

    private Rigidbody spaceship;
    private Particle particle;

    // Start is called before the first frame update
    void Start()
    {
      spaceship = GetComponent<Rigidbody>();
      particle = ParticleFactory.Get(ParticleType.Direction);
      particle.SetEffect(effect);
    }

    // Update is called once per frame
    void Update()
    {
      particle.Run(spaceship.position);
    }

}
