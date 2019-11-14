using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Particle System implements a factory pattern.
 *
 * Usage:
 *   var p = ParticleFactory.Get(ParticleType.Collision);
 *   p.SetEffect(new Vector3(1, 2, 3));
 *   p.Run();
 *
 */

/*
 * ParticleType
 *
 * Type of particles.
 *
 * Direction - trailing particles for ship and asteroids.
 * Collision - explosion paticles for ship collision with asteroid.
 */
public enum ParticleType 
{
  Collision, 
  Direction
}

/*
 * Particle
 *
 * The base particle type. Specifies what particles should implemment.
 */
public abstract class Particle : ScriptableObject
{
    public abstract void SetEffect(Transform e);
    public abstract void Run(Vector3 position);
}

/*
 * CollisionParticle
 *
 * TODO: impl. + docs.
 */
class CollisionParticle : Particle
{

  // The particle object shown on screen. 
  private Transform alive; 

  public override void SetEffect(Transform e) 
  {
    alive = Instantiate(e, new Vector3(0, 0, 0), Quaternion.identity);
  }

  public override void Run(Vector3 next)
  {
    alive.position = next;
  }

}

/* 
 * DirectionParticle
 *
 * Given an effect (Unity#Transform) instantiate the effect and continue to
 * trail the position (UnityEngine#Vector3) supplied.
 */
class DirectionParticle : Particle
{

  private Transform effect; // The effect to be instantiated.
  private Transform alive; // The particle object shown on screen. 

  // Initial screen location of particle before Unity#Vector3 is given.
  private static Vector3 initialPosition = new Vector3(0, 0, 0); 
  
  /* 
   * SetEffect
   *
   * Given an effect instantiate it and add it to the scene.
   */
  public override void SetEffect(Transform e) 
  {
    effect = e;
    alive = Instantiate(effect, initialPosition, Quaternion.identity);
  }
  
  /* 
   * Run
   *
   * Update live effect. Trail the Unity#Vector3.
   */
  public override void Run(Vector3 next) 
  {
    if (alive != null)
    {
      alive.position = toShip(next);
    }
  }

  /*
   * toShip
   *
   * To transform Unity#Vector3 to account for ship size, location, dimensions.
   */
  private Vector3 toShip(Vector3 next) 
  {
    next.y -= 1.75f;
    next.z -= 2f;
    return next;
  }
  
  /*
   * Kill
   *
   * Remove item from scene.
   */
  public void Kill() 
  {
    Destroy(alive.gameObject);
  }

}

/*
 * ParticleFactory
 *
 * Return a Particle instance. Users of Particle class do not care nor should
 * they care about WHAT type of particle they may be trying to instantiate. Just
 * that they need particles instantiated. Minimize cognitive overhead.
 */
public static class ParticleFactory
{
  
  /* 
   * ParticleContainer
   *
   * Use to kill particles that should be dead (i.e. don't display on screen
   * when they should not be - like ship exhaust when collision explosion
   * occurs).
   */
  class ParticleContainer 
  {

    private List<Particle> current;

    public ParticleContainer() 
    {
      current = new List<Particle>();
    }
    
    public void Add(Particle item)
    {
      current.Add(item);
    }

    public void Add(CollisionParticle item) 
    {
      // Kill all ship trailing particles when explosion occurs.
      current.ForEach((Particle some) => 
      {
        if (some is DirectionParticle) 
        {
          (some as DirectionParticle).Kill();
        }
      });
      
      current.Clear();
    }
  }

  static ParticleContainer container = new ParticleContainer();

  /* 
   * Particle#Get
   *
   * Given a ParticleType enum return a Particle instance or throw excepti on 
   * invalid argument.
   */
  public static Particle Get(ParticleType type)
  {
    switch (type)
    {
      case ParticleType.Collision:
        var collision = ScriptableObject.CreateInstance<CollisionParticle>();
        container.Add(collision);
        return collision;
      case ParticleType.Direction: 
        var direction = ScriptableObject.CreateInstance<DirectionParticle>();
        container.Add(direction);
        return direction;
      default:
        throw new Exception("not an enum type of ParticleType");
    }
  }
}
