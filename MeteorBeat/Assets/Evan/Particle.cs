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

public enum ParticleType 
{
  Collision, 
  Direction
}

public abstract class Particle : MonoBehaviour
{
    public abstract void SetEffect(Transform e);
    public abstract void Run(Vector3 position);
}

class CollisionParticle : Particle
{

  private Vector3 pos;

  public override void SetEffect(Transform e) 
  {
  }

  public override void Run(Vector3 next)
  {
  }

}

class DirectionParticle : Particle
{

  private Transform effect;
  private Transform alive;

  private static Vector3 initialPosition = new Vector3(0, 0, 0);
  
  public override void SetEffect(Transform e) 
  {
    effect = e;
    alive = Instantiate(effect, initialPosition, Quaternion.identity);
  }
  
  public override void Run(Vector3 next) 
  {
    alive.position = toShip(next);
  }

  public Vector3 toShip(Vector3 next) 
  {
    next.y -= 1.75f;
    next.z -= 2f;
    return next;
  }
  
}

public static class ParticleFactory
{
    public static Particle Get(ParticleType type)
    {
        switch (type)
        {
          case ParticleType.Collision:
            return new CollisionParticle();
          case ParticleType.Direction: 
            return new DirectionParticle();
          default:
            throw new Exception("not an enum type of ParticleType");
        }
    }
}
