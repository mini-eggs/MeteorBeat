using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * ILogger
 *
 * Inteface type of any form of `Log` class.
 * Used for dependency injection.
 */
public interface ILogger
{
   void Run(string key);
   int Get(string key);
}

/*
 * NoOpLog
 *
 * A logger that does nothing!
 *
 * Usage:
 *    var log = new NoOpLog();
 *    log.Run('SomeCoolMethod');
 *    var callcount = log.Get('SomeCoolMethod'); // always `0`
 */
public class NoOpLog : ILogger
{

   public void Run(string key)
   {
   }

   public int Get(string key)
   {
      return 0;
   }
}


/*
 * Log
 *
 * Keep track of how many times a method has been called. Test Util.
 *
 * Usage:
 *    var log = new Log();
 *    log.Run('SomeCoolMethod');
 *    var callcount = log.Get('SomeCoolMethod'); // this is `1`
 */
public class Log : ILogger
{

   private Dictionary<string, int> stack;

   public Log() 
   {
      stack = new Dictionary<string, int>();
   }

   public void Run(string key)
   {
      int result;
      if (stack.TryGetValue(key, out result))
      {
         stack.Add(key, result + 1);
      }
      else
      {
         stack.Add(key, 1);
      }
   }

   public int Get(string key)
   {
      int result;
      if (stack.TryGetValue(key, out result))
      {
         return result;
      }
      else
      {
         return 0;
      }
   }
}
