using System;
using UnityEngine;

/*
 * Particle System implements a factory pattern.
 *
 * Usage:
 *   var p = ParticleFactory.Get(ParticleType.Collision);
 *   p.SetPosition(new Vector3(1, 2, 3));
 *   p.Run();
 *
 */

public enum ParticleType 
{
  Collision, 
  Direction
}

public abstract class Particle
{
    public abstract void SetPosition(Vector3 position);
    public abstract void Run();
}

class CollisionParticle : Particle
{
  private Vector3 pos;
  public override void SetPosition(Vector3 next) 
  {
    pos = next;
  }
  public override void Run() 
  {
  }
}

class DirectionParticle : Particle
{
  private Vector3 pos;
  public override void SetPosition(Vector3 next) 
  {
    pos = next;
  }
  public override void Run() 
  {
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
