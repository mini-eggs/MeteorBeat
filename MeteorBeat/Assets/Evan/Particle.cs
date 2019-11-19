using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Particle System implements a factory pattern.
 *
 * The particle system will create the necessary particle class instance
 * for you (via `ParticleFactory.Get(...)`, provide it with the
 * ParticleType enum you are interested in. Either of `Collision` or
 * `Direction`.
 *
 * Once created it does not immediately appear on the scene in game. You
 * need to use the `Run` method of the instance returned from 
 * `ParticleFactory.Get(...)`. The Run method creates the particle
 * effects at the location of the `Rigidbody` provided to it. This
 * `Rigidbody` will be the particle effect's parent. For the `Direction`
 * paricle effect you likely want to show this continuelly. In your
 * `MonoBehaviour`'s `Start` method run the `ParticleFactory.Get(...)`, 
 * then within the `Update` method of your class you'll likely want to
 * run `theParticleYouCreated.Run(someRigidBody)` to update the particle
 * effect's location. For the `Collision` particle effects you likely
 * only want to run `Run` once, when your `Rigidbody` colides with
 * something. Minimal usage of both these particle effects can be seen
 * below.
 *
 * Usage:
 *   ParticleFactory.Get(ParticleType.Collision).Run(someRigidBody);
 *   -or-
 *   ParticleFactory.Get(ParticleType.Direction).Run(someRigidBody);
 */

/*
 * ParticleType
 *
 * Type of particles. Use this with `ParticleFactory.Get` to receive an
 * instance of a particle effect class.
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
 * BaseParticle
 *
 * A simple class to extend from that loads in the prefabs and has a
 * logger instance attached.
 *
 * USAGE: DO NOT USE OUTSIDE THIS CLASS
 *
 * Needs to be public to satisfy compiler.
 */
public class BaseParticle : ScriptableObject
{

   // The prefab for the particle particle effect. See factory below.
   public GameObject prefab;

   // For testing use. Makes testing a h*ll of a lot easier.
   protected ILogger log = new NoOpLog();

   // For testing use. Allow setting custom loggers.
   public void SetLogger(ILogger nextLogger)
   {
      log = nextLogger;
   }

   // Load in prefab resource.
   public void Load(string prefabName)
   {
      log.Run("BaseParticle#Load");
      prefab = Resources.Load(prefabName) as GameObject;
   }

   /*
    * CurrentPrefabs
    *
    * Get all `GameObject`s in use for class instance.
    * Return the count accurate to their usage.
    */
   public virtual List<GameObject> CurrentGameObjects()
   {
      log.Run("BaseParticle#CurrentGameObjects");
      var list = new List<GameObject>();
      list.Add(prefab);
      return list;
   }

}

/*
 * Particle
 *
 * The abstract particle type. Specifies what particles should 
 * implemment.
 *
 * USAGE: DO NOT USE OUTSIDE THIS CLASS
 */
public abstract class Particle : BaseParticle
{
   public abstract void Run(Rigidbody parent);
}

/*
 * CollisionParticle
 *
 * Once instantiated, give `Run` a rigid body to show collition 
 * explosion at the rigid body's location.
 *
 * Instantiated via `ParticleFactory.Get`.
 *
 * Usage:
 *   ParticleFactory.Get(ParticleType.Collision).Run(someRigidBody);
 *
 */
class CollisionParticle : Particle
{

   public override void Run(Rigidbody parent)
   {
      log.Run("CollisionParticle#Run");

      // Instantiate and set position.
      var item = Instantiate(
            prefab,
            Vector3.zero,
            Quaternion.identity);

      item.transform.position = parent.transform.position;
   }

}

/* 
 * DirectionParticle
 *
 * Once instantiated, give `Run` a rigid body to show collition 
 * explosion at the rigid body's location.
 *
 * Instantiated via `ParticleFactory.Get`.
 *
 * Usage:
 *   ParticleFactory.Get(ParticleType.Direction).Run(someRigidBody);
 *
 */
class DirectionParticle : Particle
{

   // The prefab used for thrusters.
   new private GameObject prefab;

   // Section of the thrusters.
   private GameObject left;
   private GameObject center;
   private GameObject right;

   /* 
    * Run
    *
    * Update live particles. Trail the Unity#Vector3.
    */
   public override void Run(Rigidbody parent)
   {
      log.Run("DirectionParticle#Run");

      // Load prefab in if not available.
      if (prefab == null)
      {
         prefab = Resources.Load("thruster_prefab") as GameObject;
      }

      // Insantiate object if not in scene.
      if (left == null)
      {
         left = Instantiate(prefab, Vector3.zero, Quaternion.identity);
         left.transform.parent = parent.transform;
      }

      if (center == null)
      {
         center = Instantiate(prefab, Vector3.zero, Quaternion.identity);
         center.transform.parent = parent.transform;
      }

      if (right == null)
      {
         right = Instantiate(prefab, Vector3.zero, Quaternion.identity);
         right.transform.parent = parent.transform;
      }

      // Calculate new positions.
      var pos = new Vector3(parent.transform.position.x,
                            parent.transform.position.y + 0.3f,
                            parent.transform.position.z + 3.1f);

      left.transform.position = new Vector3(pos.x - 0.6f,
                                            pos.y - 0.5f,
                                            pos.z - 4.659f);

      center.transform.position = new Vector3(pos.x - 0.0f,
                                              pos.y - 0.29f,
                                              pos.z - 5.22f);

      right.transform.position = new Vector3(pos.x + 0.6f,
                                             pos.y - 0.5f,
                                             pos.z - 4.659f);
   }

   /**
    * CurrentPrefabs
    *
    * Get all `GameObject`s in use for class instance.
    * Return the count accurate to their usage.
    */
   new public virtual List<GameObject> CurrentGameObjects()
   {
      log.Run("DirectionParticle#CurrentGameObjects");
      var list = new List<GameObject>();
      list.Add(left);
      list.Add(right);
      list.Add(center);
      return list;
   }

}

/*
 * ParticleFactory
 *
 * Return a Particle instance. Users of Particle class do not care nor 
 * should they care about WHAT type of particle they may be trying to 
 * instantiate. Just that they need particles instantiated. Minimize 
 * cognitive overhead.
 *
 * Usage:
 *   ParticleFactory.Get(ParticleType.Collision).Run(someRigidBody);
 *   -or-
 *   ParticleFactory.Get(ParticleType.Direction).Run(someRigidBody);
 */
public static class ParticleFactory
{

   /* 
    * Particle#Get
    *
    * Given a ParticleType enum return a Particle instance or throw 
    * exception invalid argument.
    */
   public static Particle Get(ParticleType type)
   {
      switch (type)
      {
         case ParticleType.Collision:
            var col = ScriptableObject
               .CreateInstance<CollisionParticle>();
            col.Load("explosion_prefab");
            return col;
         case ParticleType.Direction:
            var dir = ScriptableObject
               .CreateInstance<DirectionParticle>();
            dir.Load("thruster_prefab");
            return dir;
         default:
            throw new Exception("not an enum type of ParticleType");
      }
   }
   
   /* 
    * Particle#Get
    *
    * Given a ParticleType enum and a logger interface return a 
    * Particle instance or throw exception invalid argument.
    */
   public static Particle Get(
         ParticleType type,
         ILogger logger)
   {
      switch (type)
      {
         case ParticleType.Collision:
            var col = ScriptableObject
               .CreateInstance<CollisionParticle>();
            col.SetLogger(logger);
            col.Load("explosion_prefab");
            return col;
         case ParticleType.Direction:
            var dir = ScriptableObject
               .CreateInstance<DirectionParticle>();
            dir.SetLogger(logger);
            dir.Load("thruster_prefab");
            return dir;
         default:
            throw new Exception("not an enum type of ParticleType");
      }
   }

}