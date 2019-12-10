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
        ScrollBarClass myScrollBar;
        ScoreTextClass myScoreText;
        GameObject spaceShip, levelProgress, scoreText;
        string initialScenePath;
        [SetUp]
        public void Setup()
        {
            initialScenePath = SceneManager.GetActiveScene().path;
            SceneManager.LoadScene(1);
        }

        [TearDown]
        public void TearDown()
        {
            SceneManager.LoadScene(initialScenePath);
        }

      // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
      // `yield return null;` to skip a frame.
      [UnityTest]
        public IEnumerator ScrollBarTestWithEnumeratorPasses()
        {

            levelProgress = GameObject.Find("Scrollbar");
            myScrollBar = levelProgress.GetComponent<ScrollBarClass>();
		 
            myScrollBar.progress = 0.5f;
            Assert.AreEqual(0.5f, myScrollBar.progress);
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
      [UnityTest]
      public IEnumerator ScoreTextTestWithEnumeratorPasses()
      {
         scoreText = GameObject.Find("ScoreText");
         myScoreText = scoreText.GetComponent<ScoreTextClass>();

         myScoreText.Score = 2147483547;
         myScoreText.UpdateUIElement();
         Assert.AreEqual("Score: 2147483647", myScoreText.myText.text);
         yield return null;
      }

      [UnityTest]
      public IEnumerator ScoreTextOverFlowTestWithEnumeratorPasses()
      {
         scoreText = GameObject.Find("ScoreText");
         myScoreText = scoreText.GetComponent<ScoreTextClass>();

         myScoreText.Score = 2147483647;
         myScoreText.UpdateUIElement();
         Assert.AreNotEqual("Score: 2147483747", myScoreText.myText.text);
         yield return null;
      }
   }
}
