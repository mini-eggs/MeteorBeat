using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor; 


namespace Tests
{
    public class PowerupSpawnTest 
    {
        
        // A Test behaves as an ordinary method
        [Test]
        public void PowerupSpawnTestSimplePasses()
        {
            //var s = Powerup.Instance;
            // Use the Assert class to test conditions
            Debug.Log("Hello World");
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PowerupSpawnToScreenTest()
        {
            Debug.Log("Powerup has been found on screen if this passes");
            Object PrefabSpawn = Resources.Load("Assets/Health");
            
            var PowerupPrefabFind = GameObject.FindWithTag("Health");

            Object PowerupPrefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(PowerupPrefabFind);
            
            Assert.AreEqual(PrefabSpawn, PowerupPrefab);

            //Debug.Log("Name: " + PrefabSpawn.name);
            return null;
        }
    }
}
