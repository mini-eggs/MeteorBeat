using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
   public class ParticleTest
   {

      [Test]
      public void EnsureCollisionLoadCalled()
      {
         var l = new Log();
         var t = ParticleType.Collision;
         var p = ParticleFactory.Get(t, l);
         Assert.AreEqual(l.Get("BaseParticle#Load"), 1);
      }

      [Test]
      public void EnsureCollisionRanCalled()
      {
         var l = new Log();
         var t = ParticleType.Collision;
         var p = ParticleFactory.Get(t, l);
         var go = new GameObject("test");
         var rb = go.AddComponent<Rigidbody>();
         p.Run(go.GetComponent<Rigidbody>());
         Assert.AreEqual(l.Get("CollisionParticle#Run"), 1);
      }

      [Test]
      public void EnsureCollisionCurrentGameObjectsCalled()
      {
         var l = new Log();
         var t = ParticleType.Collision;
         var p = ParticleFactory.Get(t, l);
         p.CurrentGameObjects();
         Assert.AreEqual(l.Get("BaseParticle#CurrentGameObjects"), 1);
      }

      [Test]
      public void EnsureCollisionAllCalled()
      {
         var l = new Log();
         var t = ParticleType.Collision;
         var p = ParticleFactory.Get(t, l);
         var go = new GameObject("test");
         var rb = go.AddComponent<Rigidbody>();
         Assert.AreEqual(l.Get("BaseParticle#Load"), 1);
         p.Run(go.GetComponent<Rigidbody>());
         Assert.AreEqual(l.Get("CollisionParticle#Run"), 1);
         p.CurrentGameObjects();
         Assert.AreEqual(l.Get("BaseParticle#CurrentGameObjects"), 1);
      }

      // This crashes Unity.
      // [Test]
      // public void StressTestCollision()
      // {
      //    for(int i = 0; i < 1000000; i++) 
      //    {
      //       var t = ParticleType.Collision;
      //       var p = ParticleFactory.Get(t);
      //       var go = new GameObject("test");
      //       var rb = go.AddComponent<Rigidbody>();
      //       p.Run(go.GetComponent<Rigidbody>());
      //    }
      // }

      [Test]
      public void EnsureDirectionLoadCalled()
      {
         var l = new Log();
         var t = ParticleType.Direction;
         var p = ParticleFactory.Get(t, l);
         Assert.AreEqual(l.Get("BaseParticle#Load"), 1);
      }

      [Test]
      public void EnsureDirectionRanCalled()
      {
         var l = new Log();
         var t = ParticleType.Direction;
         var p = ParticleFactory.Get(t, l);
         var go = new GameObject("test");
         var rb = go.AddComponent<Rigidbody>();
         p.Run(go.GetComponent<Rigidbody>());
         Assert.AreEqual(l.Get("DirectionParticle#Run"), 1);
      }

      [Test]
      public void EnsureDirectionCurrentGameObjectsCalled()
      {
         var l = new Log();
         var t = ParticleType.Direction;
         var p = ParticleFactory.Get(t, l);
         p.CurrentGameObjects();
         Assert.AreEqual(l.Get("BaseParticle#CurrentGameObjects"), 1);
      }

      [Test]
      public void EnsureDirectionnAllCalled()
      {
         var l = new Log();
         var t = ParticleType.Direction;
         var p = ParticleFactory.Get(t, l);
         var go = new GameObject("test");
         var rb = go.AddComponent<Rigidbody>();
         Assert.AreEqual(l.Get("BaseParticle#Load"), 1);
         p.Run(go.GetComponent<Rigidbody>());
         Assert.AreEqual(l.Get("DirectionParticle#Run"), 1);
         p.CurrentGameObjects();
         Assert.AreEqual(l.Get("BaseParticle#CurrentGameObjects"), 1);
      }

      [Test]
      public void EnsureDirectionCurrentGameObjectsDynamicCalled()
      {
         var l = new Log();
         var t = ParticleType.Direction;
         DirectionParticle p = ParticleFactory.Get(t, l) 
            as DirectionParticle;
         p.CurrentGameObjects();
         var val = l.Get("DirectionParticle#CurrentGameObjects");
         Assert.AreEqual(val, 1);
      }

      [Test]
      public void EnsureDirectionnAllDynamicCalled()
      {
         var l = new Log();
         var t = ParticleType.Direction;
         DirectionParticle p = ParticleFactory.Get(t, l) 
            as DirectionParticle;
         var go = new GameObject("test");
         var rb = go.AddComponent<Rigidbody>();
         Assert.AreEqual(l.Get("BaseParticle#Load"), 1);
         p.Run(go.GetComponent<Rigidbody>());
         Assert.AreEqual(l.Get("DirectionParticle#Run"), 1);
         p.CurrentGameObjects();
         var val = l.Get("DirectionParticle#CurrentGameObjects");
         Assert.AreEqual(val, 1);
      }

      // This crashes Unity.
      // [Test]
      // public void StressTestDirection()
      // {
      //    for(int i = 0; i < 1000000; i++) 
      //    {
      //       var t = ParticleType.Direction;
      //       var p = ParticleFactory.Get(t);
      //       var go = new GameObject("test");
      //       var rb = go.AddComponent<Rigidbody>();
      //       p.Run(go.GetComponent<Rigidbody>());
      //    }
      // }

   }
}
