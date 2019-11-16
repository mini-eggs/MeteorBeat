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
 * Usage: 
 *   var s = BeatBox.Instance;
 *   s.LoadSounds(yourGameObject);
 *   s.PlayLevelSoundTrack();
 *   s.PlayGameLost();
 *   s.PlayGameWon();
 */

public enum SoundType {
  GameComplete,
  GameLevelSound,
  GameCollision
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
   * Pass in a SoundType, AudioSource, and bool. Bool is only used in test mode
   * (Unity cannot play sound off main thread). So, always pass me a bool of 
   * true.
   *
   * This class is a light wrapper over two AudioSource methods + some
   * comparison methods for use with IEquatable#Contains method. Main usage is
   * SoundClip#Play and SoundClip#Stop.
   */
  public SoundClip(SoundType t, AudioSource a, bool dryrun) 
  {
    type = t; 
    sound = a;

    // For testing. Sounds cannot be played off main thread within Unity.
    if (!dryrun) Play();
  }

  // Container method for AudioSource.
  public void Play() 
  {
    sound.Play();
  }

  // Container method for AudioSource.
  public void Stop() 
  {
    sound.Stop();
  }

  /*
   * SoundClip
   * For comparing to instances, useless on its own. See `SoundClipp#Equals
   * below.
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
 * This is a singleton class, notice the private constructor. 
 * Do not add more constructors!
 */
public sealed class BeatBox : ScriptableObject
{
  // sounds
  private AudioSource gameWonClip;
  private AudioSource gameLostClip;
  private AudioSource gameLevelSoundClip;
  private AudioSource gameCollisionClip;

  // in case BeatBox#LoadSounds is called more than once, we don't want to
  // reload music files
  private bool hasLoaded;

  public List<SoundClip> currentlyPlaying;

  private BeatBox() 
  {
    currentlyPlaying = new List<SoundClip>();
    hasLoaded = false;
  }

  public void LoadSounds(GameObject gameObject) 
  {
    if(hasLoaded)
    {
      return;
    }

    var winnings = new string[5]{"perfect","impressive","holyshit","unstoppable","wickedsick"};
    var winning = winnings[UnityEngine.Random.Range(0, winnings.Length)];
    gameWonClip = gameObject.AddComponent<AudioSource>();
    gameWonClip.clip = Resources.Load(winning) as AudioClip;

    var losings = new string[4]{"death1","death2","death3","death4"};
    var losing = losings[UnityEngine.Random.Range(0, losings.Length)];
    gameLostClip = gameObject.AddComponent<AudioSource>();
    gameLostClip.clip = Resources.Load(losing) as AudioClip;

    gameLevelSoundClip = gameObject.AddComponent<AudioSource>();
    gameLevelSoundClip.clip = Resources.Load("soundtrack") as AudioClip;

    // Is this really needed?
    gameCollisionClip = gameObject.AddComponent<AudioSource>();
    gameCollisionClip.clip = Resources.Load("explosion") as AudioClip;

    hasLoaded = true;
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
    if ( !currentlyPlaying.Contains(new SoundClip(SoundType.GameComplete)) )
    {
      currentlyPlaying.Add(new SoundClip(SoundType.GameComplete, gameWonClip, dryrun));
    }
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
    if ( !currentlyPlaying.Contains(new SoundClip(SoundType.GameComplete)) )
    {
      ClearAll(); // Stop playing level soundtrack.
      currentlyPlaying.Add(new SoundClip(SoundType.GameComplete, gameLostClip, dryrun));
    }
  }

  /* 
   * PlayLevelSoundTrack
   *
   * Play the standard level soundtrack
   * iff a sound of SoundType#GameLeveSound is not already playing.
   */
  public void PlayLevelSoundTrack(bool dryrun = false) 
  {
    if ( !currentlyPlaying.Contains(new SoundClip(SoundType.GameLevelSound)) )
    {
      ClearAll(); // Stop playing level soundtrack.
      currentlyPlaying.Add(new SoundClip(SoundType.GameLevelSound, gameLevelSoundClip, dryrun));
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
    currentlyPlaying.Add(new SoundClip(SoundType.GameCollision, gameCollisionClip, dryrun));
  }

  /* 
   * ClearAll
   *
   * Mainly used for testing and/or leve complete/end/return to main menu.
   * Simply stop playing all current sounds.
   */
  public void ClearAll() 
  {
    currentlyPlaying.ForEach((SoundClip item) => 
    {
      item.Stop();
    });
    currentlyPlaying.Clear();
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
   * Not accessable to outside world. Helper for the BeatBox singleton getter.
   * Please don't pay this any mind.
   */
  private class Nested 
  {
    static Nested() {}
    internal static readonly BeatBox instance = ScriptableObject.CreateInstance<BeatBox>();
  }
}
