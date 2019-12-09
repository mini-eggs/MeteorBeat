using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * RingIntegration
 *
 * Meta class. Simply play point gain sound when user passes through a 
 * ring.
 */
public class RingIntegration : MonoBehaviour
{
   /*
    * OnTriggerEnter
    *
    * User has hit a ring. Play the sound!
    */
   void OnTriggerEnter()
   {
      BeatBox.Instance.PlayPointGain();
    
      GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreTextClass>().UpdateUIElement(); 
   }
}