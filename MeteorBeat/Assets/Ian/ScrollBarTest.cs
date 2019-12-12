using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace Tests
{
    public class ScrollBarTest
    {
        Scrollbar myScrollBar;
        GameObject spaceShip, levelProgress;
        UpdateScrollBar myUpdateScrollBar;
        string initialScenePath;
        [SetUp]
        public void Setup()
        {
            initialScenePath = SceneManager.GetActiveScene().path;
            SceneManager.LoadScene(0);
        }

        [TearDown]
        public void TearDown()
        {
            SceneManager.LoadScene(initialScenePath);
        }

        // A Test behaves as an ordinary method
        [Test]
        public void ScrollBarTestSimplePasses()
        {

            // Use the Assert class to test conditions

            //Assert.AreEqual("Spaceship", spaceShip.name);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator ScrollBarTestWithEnumeratorPasses()
        {
            //levelProgress = GameObject.Find
            spaceShip = GameObject.Find("Spaceship");
            Assert.AreEqual("Spaceship", spaceShip.name);
            Assert.AreEqual(-10.0f, spaceShip.transform.position.z);

            levelProgress = GameObject.Find("LevelProgress");
            myUpdateScrollBar = spaceShip.GetComponent<UpdateScrollBar>();
            myScrollBar = levelProgress.GetComponent<Scrollbar>();

            spaceShip.transform.position = new Vector3(0, 0, ((myUpdateScrollBar.endZ - myUpdateScrollBar.startZ) / 2) + myUpdateScrollBar.startZ);

            Debug.Log(spaceShip.transform.position);
            yield return new WaitForSeconds(0.4f);
            Assert.AreEqual(0.5f, myScrollBar.value);
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
