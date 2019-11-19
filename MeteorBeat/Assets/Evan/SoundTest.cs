using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SoundTest
    {
        [SetUp]
        public void TestLoadSounds() 
        {
          var s = BeatBox.Instance;
          s.LoadSounds(new GameObject());
        }

        [Test]
        public void CantPlayGameWonAfterGameLost() 
        {
          var s = BeatBox.Instance;

          s.PlayGameLost();
          s.PlayGameWon();

          // Only game lost should be playing
          // Can't play two SoundType.GameCompete's at the same time.
          Assert.AreEqual(1, s.currentlyPlaying.Count);
          Assert.AreEqual(SoundType.GameComplete, s.currentlyPlaying[s.currentlyPlaying.Count - 1].type);
        }

        [Test]
        public void PlaySoundTrackAndGameWonSameTime() 
        {
          var s = BeatBox.Instance;
          s.PlayLevelSoundTrack();

          Assert.AreEqual(1, s.currentlyPlaying.Count);
          Assert.AreEqual(SoundType.GameLevelSound, s.currentlyPlaying[s.currentlyPlaying.Count - 1].type);

          s.PlayGameWon();

          Assert.AreEqual(2, s.currentlyPlaying.Count);
          Assert.AreEqual(SoundType.GameComplete, s.currentlyPlaying[s.currentlyPlaying.Count - 1].type);
        }

        [Test]
        public void PlaySoundMultiThreadedWorkload() 
        {
          // Ensure all tasks play their sounds.
          var s = BeatBox.Instance;
          Task[] tasks = new Task[10];

          for (int i = 0; i < 10; i++)
          {
            tasks[i] = Task.Factory.StartNew(() =>
            {
              ThreadUnit();
            });
          }

          Task.WaitAll(tasks);

          Assert.AreEqual(10, s.currentlyPlaying.Count);
        }

        // For PlaySoundMultiThreadedWorkload
        public void ThreadUnit() 
        {
          var s = BeatBox.Instance;
          s.PlayCollision(true); // dryrun because you can't play sounds from off main thread
        }

        [TearDown]
        public void Clear() 
        {
          var s = BeatBox.Instance;
          s.ClearAll();
        }
    }
}
