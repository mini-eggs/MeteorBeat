using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


/*
 * Test classes.
 *
 * Static and dynamic binding references.
 */

class Grandfather
{
  public string FirstName() 
  {
    return "Neil";
  }

  public virtual string LastName() 
  {
    return "Newlun";
  }
}

class Father : Grandfather
{
  public string FirstName() 
  {
    return "Michael";
  }

  public string LastName() 
  {
    return "Miller";
  }
}

class Son : Father
{
  public string FirstName() 
  {
    return "Evan";
  }

  public string LastName()
  {
    return "Jones";
  }
}

/*
 * The actual tests.
 */

namespace Tests
{
    public class BindingTest
    {

        [Test]
        public void AllEqual()
        {
          Grandfather g1 = new Grandfather();
          Grandfather f1 = new Father();
          Grandfather s1 = new Son();
          Assert.AreEqual(g1.LastName(), f1.LastName(), s1.LastName());
        }

        [Test]
        public void FatherNotEqual()
        {
          Grandfather g1 = new Grandfather();
          Father f1 = new Father();
          Grandfather s1 = new Son();
          Assert.AreEqual(g1.LastName(), s1.LastName());
          Assert.AreNotEqual(g1.LastName(), f1.LastName());
        }

        [Test]
        public void GrandfatherNotEqual()
        {
          Grandfather g1 = new Grandfather();
          Father f1 = new Father();
          Father s1 = new Son();
          Assert.AreEqual(s1.LastName(), s1.LastName());
          Assert.AreNotEqual(g1.LastName(), f1.LastName());
        }

        [Test]
        public void NoneEqual()
        {
          Grandfather g1 = new Grandfather();
          Father f1 = new Father();
          Son s1 = new Son();
          Assert.AreNotEqual(g1.LastName(), f1.LastName(), s1.LastName());
        }

        [Test]
        public void ParticleBaseClassCountCollision()
        {
          Particle col = ParticleFactory.Get(ParticleType.Collision);
          Assert.AreEqual(col.CurrentGameObjects().Count, 1);
        }

        [Test]
        public void ParticleCollisionClassCountCollision()
        {
          CollisionParticle col = ParticleFactory.Get(ParticleType.Collision) as CollisionParticle;
          Assert.AreEqual(col.CurrentGameObjects().Count, 1);
        }

        [Test]
        public void ParticleBaseClassCountDirectional()
        {
          Particle dir = ParticleFactory.Get(ParticleType.Direction);
          Assert.AreEqual(dir.CurrentGameObjects().Count, 1);
        }

        [Test]
        public void ParticleDirectionalClassCountDirectional()
        {
          DirectionParticle dir = ParticleFactory.Get(ParticleType.Direction) as DirectionParticle;
          Assert.AreEqual(dir.CurrentGameObjects().Count, 3);
        }

    }
}
