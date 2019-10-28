using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class CheckAsteroid
    {
        // A Test behaves as an ordinary method
        [Test]
        public void CheckAsteroidSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator CheckAsteroidWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            SceneManager.LoadScene("SampleScene");
            yield return null;
            Assert.IsNotNull(GameObject.Find("Spaceship"));
            Assert.IsNotNull(GameObject.Find("Asteroid"));
        }
    }
}
