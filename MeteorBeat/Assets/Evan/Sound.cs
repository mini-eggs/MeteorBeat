using System;
using UnityEngine;

/* 
 * BeatBox 
 * This is a singleton class, notice the private constructor. 
 * Do not add more constructors!
 */
public sealed class BeatBox 
{
  private BeatBox() 
  {
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
