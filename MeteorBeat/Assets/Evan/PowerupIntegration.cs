using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * PowerupIntegration
 *
 * Meta class. Used for triggering powerup sound when user collides with 
 * the powerup.
 */
public class PowerupIntegration : MonoBehaviour
{
   /* 
    * OnTriggerEnter
    *
    * When ship collides with powerup play the associated sound.
    *
    */
   void OnTriggerEnter()
   {
      BeatBox.Instance.PlayPowerupGain();
   }
}
