using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType {
  GameComplete,
  GameLevelSound,
  GameCollision
}

public class SoundClip : IEquatable<SoundClip>
{
  public AudioSource sound;
  public SoundType type;

  public SoundClip(SoundType t, AudioSource a, bool dryrun) 
  {
    type = t;
    sound = a;
    if (!dryrun)
      Play();
  }

  public SoundClip(SoundType t) 
  {
    type = t;
  }

  public void Play() 
  {
    sound.Play();
  }

  public void Stop() 
  {
    sound.Stop();
  }

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
public sealed class BeatBox 
{
  // sounds
  private AudioSource gameWonClip;
  private AudioSource gameLostClip;
  private AudioSource gameLevelSoundClip;
  private AudioSource gameCollisionClip;

  public List<SoundClip> currentlyPlaying;

  private BeatBox() 
  {
    currentlyPlaying = new List<SoundClip>();
  }

  public void LoadSounds(GameObject gameObject) 
  {
    gameWonClip = gameObject.AddComponent<AudioSource>();
    gameWonClip.clip = Resources.Load("../Evan/placeholder.mp3") as AudioClip;

    gameLostClip = gameObject.AddComponent<AudioSource>();
    gameLostClip.clip = Resources.Load("../Evan/placeholder.mp3") as AudioClip;

    gameLevelSoundClip = gameObject.AddComponent<AudioSource>();
    gameLevelSoundClip.clip = Resources.Load("../Evan/soundtrack.mp3") as AudioClip;

    gameCollisionClip = gameObject.AddComponent<AudioSource>();
    gameCollisionClip.clip = Resources.Load("../Evan/placeholder.mp3") as AudioClip;
  }

  public void PlayGameWon(bool dryrun = false) 
  {
    // can't play multiple game winning or losing sounds at once
    if ( !currentlyPlaying.Contains(new SoundClip(SoundType.GameComplete)) )
      currentlyPlaying.Add(new SoundClip(SoundType.GameComplete, gameWonClip, dryrun));
  }

  public void PlayGameLost(bool dryrun = false) 
  {
    // can't play multiple game winning or losing sounds at once
    if ( !currentlyPlaying.Contains(new SoundClip(SoundType.GameComplete)) )
      currentlyPlaying.Add(new SoundClip(SoundType.GameComplete, gameLostClip, dryrun));
  }

  public void PlayLevelSoundTrack(bool dryrun = false) 
  {
    if ( !currentlyPlaying.Contains(new SoundClip(SoundType.GameLevelSound)) )
      currentlyPlaying.Add(new SoundClip(SoundType.GameLevelSound, gameLevelSoundClip, dryrun));
  }

  public void PlayCollision(bool dryrun = false) 
  {
    currentlyPlaying.Add(new SoundClip(SoundType.GameCollision, gameCollisionClip, dryrun));
  }

  public void ClearAll() 
  {
    currentlyPlaying.ForEach((SoundClip item) => 
    {
      item.Stop();
    });
    currentlyPlaying.Clear();
  }

  public static BeatBox Instance
  {
    get 
    {
      return Nested.instance;
    }
  }

  /*
   * Nested
   * Not accessable to outside world
   */
  private class Nested 
  {
    static Nested() 
    {
    }

    internal static readonly BeatBox instance = new BeatBox();
  }
}
