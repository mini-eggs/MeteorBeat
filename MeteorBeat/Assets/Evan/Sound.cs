using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
 * BeatBox 
 *
 * Implements the singleton pattern to play game noise.
 *
 * This BeatBox will be used by the level, ship, rings, asteroid. The
 * first thing to play sound, the level, will need to run:
 *    `BeatBox.Instance.LoadSounds(yourGameObject)`
 * on init. All other instances of using BeatBox will only call one of
 * four methods:
 *    `PlayLevelSoundTrack`, `PlayGameLost`, `PlayGameWon`, and
 *    `PlayPointGain`.
 * You can see their usage below:
 *
 * Usage: 
 *   var s = BeatBox.Instance;
 *   // Only needs to be done at top level once.
 *   s.LoadSounds(yourGameObject); 
 *   s.PlayLevelSoundTrack();
 *   -or-
 *   s.PlayGameLost();
 *   -or-
 *   s.PlayGameWon();
 *   -or-
 *   s.PlayPointGain();
 *   -or-
 *   s.PlayPowerupGain();
 */

public enum SoundType
{
   GameComplete,
   GameLevelSound,
   GameCollision,
   GamePointGain,
   GamePowerupGain,
}

/*
 * SoundClip
 *
 * Container class for UnityEngine#SoundClip. Contain all behavior for 
 * UnityEngine#SoundClip to give a uniform interface to.
 *
 * Usage: DO NOT USE THIS OUTSIDE THIS FILE.
 */
public class SoundClip : IEquatable<SoundClip>
{

   // Self-explanatory. See contructor below.
   public AudioSource sound;
   public SoundType type;

   /*
    * SoundClip - Constructor
    *
    * Pass in a SoundType, AudioSource, and bool. Bool is only used in 
    * test mode (Unity cannot play sound off main thread). So, always 
    * pass me a bool of true.
    *
    * This class is a light wrapper over two AudioSource methods + some
    * comparison methods for use with IEquatable#Contains method. Main 
    * usage is SoundClip#Play and SoundClip#Stop.
    */
   public SoundClip(SoundType t, AudioSource a, bool dryrun)
   {
      type = t;
      sound = a;

      // For testing. Sounds cannot be played off main thread within 
      // Unity.
      if (!dryrun) Play();
   }

   // Container method for AudioSource.
   public void Play()
   {
      if (sound != null)
      {
         sound.Play();
      }
   }

   // Container method for AudioSource.
   public void Pause()
   {
      if (sound != null)
      {
         sound.Pause();
      }
   }

   // Container method for AudioSource.
   public void Unpause()
   {
      if (sound != null)
      {
         sound.UnPause();
      }
   }

   // Container method for AudioSource.
   public void Stop()
   {
      // For end of game/restart don't attempt to play old sounds.
      if (sound != null)
      {
         sound.Stop();
      }
   }

   /*
    * SoundClip
    * For comparing to instances, useless on its own. 
    * See `SoundClip#Equals` below.
    */
   public SoundClip(SoundType t)
   {
      type = t;
   }

   /*
    * Equals
    *
    * For comparisons with instances.
    *
    * Usage: 
    *   currentlyPlaying.Contains(new SoundClip(SoundType.GameComplete));
    */
   public bool Equals(SoundClip other)
   {
      return other.type == type;
   }
}

/* 
 * BeatBox 
 *
 * This is a singleton class, notice the private constructor. 
 * Do not add more constructors!
 */
public sealed class BeatBox : ScriptableObject
{
   // sounds
   private List<AudioSource> gamePointGainClips;
   private AudioSource gameWonClip;
   private AudioSource gameLostClip;
   private AudioSource gameLevelSoundClip;
   private AudioSource gameCollisionClip;
   private AudioSource gamePowerupClip;

   // in case BeatBox#LoadSounds is called more than once, we don't want 
   // to reload music files
   private bool hasLoaded;

   // Used so we don't accidently play the game winning sound when game
   // gets paused.
   private bool isPaused;

   public List<SoundClip> currentlyPlaying;

   /*
    * BeatBox
    *
    * Setup private datas.
    */
   private BeatBox()
   {
      currentlyPlaying = new List<SoundClip>();
      hasLoaded = false;
      isPaused = false;
   }

   /*
    * IsPlaying
    *
    * For external classes to determine if the game is still running.
    */
   public bool IsPlaying()
   {
      // Don't play game winning sound on pause!
      if (isPaused) 
      {
         return true;
      }

      return (gameLevelSoundClip != null &&
             gameLevelSoundClip.isPlaying &&
             hasLoaded);
   }

   /*
    * LoadSounds
    *
    * This method needs to be called first ALWAYS. 
    * See `LevelIntegration` script. The level will always call this
    * method first before any other usages of this singleton.
    */
   public void LoadSounds(GameObject gameObject)
   {
      if (hasLoaded)
      {
         return;
      }

      var winnings = new string[5] {
         "perfect",
         "impressive",
         "holyshit",
         "unstoppable",
         "wickedsick"
      };

      var randomIndex = UnityEngine.Random.Range(0, winnings.Length);
      var winning = winnings[randomIndex];
      gameWonClip = gameObject.AddComponent<AudioSource>();
      gameWonClip.clip = Resources.Load(winning) as AudioClip;

      var losings = new string[4] {
         "death1",
         "death2",
         "death3",
         "death4"
      };

      var losing = losings[UnityEngine.Random.Range(0, losings.Length)];
      gameLostClip = gameObject.AddComponent<AudioSource>();
      gameLostClip.clip = Resources.Load(losing) as AudioClip;

      gameLevelSoundClip = gameObject.AddComponent<AudioSource>();
      gameLevelSoundClip.clip = Resources.Load("soundtrack")
         as AudioClip; // ugh, line length.

      gameCollisionClip = gameObject.AddComponent<AudioSource>();
      gameCollisionClip.clip = Resources.Load("explosion") as AudioClip;

      gamePowerupClip = gameObject.AddComponent<AudioSource>();
      gamePowerupClip.clip = Resources.Load("powerup") as AudioClip;



      gamePointGainClips = new List<AudioSource>();
      // Note: point sounds same as game winning sounds.
      foreach (string sound in winnings)
      {
         var item = gameObject.AddComponent<AudioSource>();
         item.clip = Resources.Load(sound) as AudioClip;
         gamePointGainClips.Add(item);
      }

      hasLoaded = true;
   }

   /* 
    * PlayPowerupGain
    *
    * Play the powerup gaining jingle 
    */
   public void PlayPowerupGain(bool dryrun = false)
   {
      var clip = new SoundClip(
            SoundType.GamePowerupGain, 
            gamePowerupClip, 
            dryrun);
      currentlyPlaying.Add(clip);
   }

   /* 
    * PlayPointGain
    *
    * Play the point gaining jingle 
    */
   public void PlayPointGain(bool dryrun = false)
   {
      var count = gamePointGainClips.Count;
      var randomIndex = UnityEngine.Random.Range(0, count);
      var sound = gamePointGainClips[randomIndex];
      var clip = new SoundClip(SoundType.GamePointGain, sound, dryrun);
      currentlyPlaying.Add(clip);
   }

   /* 
    * PlayGameWon
    *
    * Play the game winning jingle 
    * iff a sound of SoundType#GameComplete is not already playing.
    */
   public void PlayGameWon(bool dryrun = false)
   {
      // can't play multiple game winning or losing sounds at once
      var gameComplete = new SoundClip(SoundType.GameComplete);
      if (!currentlyPlaying.Contains(gameComplete))
      {
         ClearAll(); // Stop playing level soundtrack.
         currentlyPlaying.Add(new SoundClip(
                  SoundType.GameComplete,
                  gameWonClip,
                  dryrun));
      }

      // We need to ensure we reload sounds on level restart.
      hasLoaded = false;
   }

   /* 
    * PlayGameLost
    *
    * Play the game losing jingle 
    * iff a sound of SoundType#GameComplete is not already playing.
    */
   public void PlayGameLost(bool dryrun = false)
   {
      // can't play multiple game winning or losing sounds at once
      var item = new SoundClip(SoundType.GameComplete);
      if (!currentlyPlaying.Contains(item))
      {
         ClearAll(); // Stop playing level soundtrack.
         currentlyPlaying.Add(new SoundClip(
                  SoundType.GameComplete,
                  gameLostClip,
                  dryrun));
      }

      // We need to ensure we reload sounds on level restart.
      hasLoaded = false;
   }

   /* 
    * PlayLevelSoundTrack
    *
    * Play the standard level soundtrack
    * iff a sound of SoundType#GameLeveSound is not already playing.
    */
   public void PlayLevelSoundTrack(bool dryrun = false)
   {
      var item = new SoundClip(SoundType.GameLevelSound);
      if (!currentlyPlaying.Contains(item))
      {
         ClearAll(); // Stop playing level soundtrack.
         currentlyPlaying.Add(new SoundClip(
                  SoundType.GameLevelSound,
                  gameLevelSoundClip,
                  dryrun));
      }
   }

   /* 
    * PauseLevelSoundTrack
    *
    * Temp stop level soundtrack.
    */
   public void PauseLevelSoundTrack()
   {
      var item = new SoundClip(SoundType.GameLevelSound);
      if (currentlyPlaying.Contains(item))
      {
         currentlyPlaying.ForEach((SoundClip clip) =>
         {
            if (clip.Equals(item))
            {
               clip.Pause();
               isPaused = true;
            }
         });
      }
   }

   /* 
    * UnpauseLevelSoundTrack
    *
    * Play level soundtrack again.
    */
   public void UnpauseLevelSoundTrack()
   {
      var item = new SoundClip(SoundType.GameLevelSound);
      if (currentlyPlaying.Contains(item))
      {
         currentlyPlaying.ForEach((SoundClip clip) =>
         {
            if (clip.Equals(item))
            {
               clip.Unpause();
               isPaused = false;
            }
         });
      }
   }

   /* 
    * PlayCollision
    *
    * Play the standard collision sound
    * iff a sound of SoundType#GameCollision is not already playing.
    */
   public void PlayCollision(bool dryrun = false)
   {
      currentlyPlaying.Add(new SoundClip(
               SoundType.GameCollision,
               gameCollisionClip,
               dryrun));
   }

   /* 
    * ClearAll
    *
    * Mainly used for testing and/or leve complete/end/return to main 
    * menu. Simply stop playing all current sounds.
    */
   public void ClearAll()
   {
      currentlyPlaying.ForEach((SoundClip item) =>
      {
         if (item != null) // Fixes unit test crash
         {
            item.Stop();
         }
      });
      currentlyPlaying.Clear();
      currentlyPlaying = new List<SoundClip>();
   }

   /* 
    * Instance
    *
    * Getter for instance of the BeatBox singleton.
    *
    * Usage:
    *   var s = BeatBox.Instance;
    *   s.PlayLevelSoundTrack(); // or s.PlayGameWon(); etc.
    */
   public static BeatBox Instance
   {
      get
      {
         return Nested.instance;
      }
   }

   /*
    * Nested
    *
    * Not accessable to outside world. Helper for the BeatBox singleton 
    * getter. Please don't pay this any mind.
    */
   private class Nested
   {
      static Nested() { }
      internal static readonly BeatBox instance = ScriptableObject
         .CreateInstance<BeatBox>();
   }
}