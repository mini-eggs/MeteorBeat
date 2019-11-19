using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * RingIntegration
 *
 * Meta class. Simply play point gain sound when user passes through a 
 * ring.
 */
public class RingScore : MonoBehaviour
{
   static int score = 0;
   /*
    * OnTriggerEnter
    *
    * User has hit a ring. Play the sound!
    */
   void OnTriggerEnter()
   {
      BeatBox.Instance.PlayPointGain();
      score += 100;
   }
}
