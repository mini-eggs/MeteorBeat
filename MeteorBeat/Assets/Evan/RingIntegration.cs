﻿using System.Collections;
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
  
   //the scoreing mechanism is Cosette's 
   UpdateScore myScore;
   static int score = 0;
   
   void Start(){
      myScore = GameObject.FindGameObjectWithTag("Player").GetComponent<UpdateScore>();
   }
   
   /*
    * OnTriggerEnter
    *
    * User has hit a ring. Play the sound!
    */
   void OnTriggerEnter()
   {
      BeatBox.Instance.PlayPointGain(); // part of Evan's work
      score += 100;
      myScore.UpdateUIElement(score);
      
   }
}