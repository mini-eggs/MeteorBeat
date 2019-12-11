﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * LevelIntegration
 *
 * Meta class. Simply play level soundtrack when level begins and stop 
 * playing when level is over. Connect this script to the top level 
 * scene within Unity editor.
 */
public class LevelIntegration : MonoBehaviour
{

   private bool isRunning; // for tracking if we need to call UserHasWon

   /*
    * Start
    *
    * Grab instance of BeatBox singleton and begin playing soundtrack.
    */
   void Start()
   {
      var s = BeatBox.Instance;
		var music = PlayerPrefs.GetString("Music");
		Debug.Log("Music: " + music);
		if (music == null)
		{
			s.LoadSounds(this.gameObject, "soundtrack");
		}
		else
			s.LoadSounds(this.gameObject, music);
		// First sound that plays always. Load all game sounds on start.
      s.PlayLevelSoundTrack();
      isRunning = true;
   }

   /*
    * Update
    *
    * Check if game is over and if so restart it and let ship know.
    */
   void Update()
   {
      if (!BeatBox.Instance.IsPlaying() && isRunning)
      {
         // Book keeping, don't enter this conditional twice in a row.
         isRunning = false;

         // Tell other components game is over.
         GameObject.FindGameObjectWithTag("Player")
            .GetComponent<ShipIntegration>()
            .UserHasWon();

         // Restart the game after three seconds
         Invoke("RestartGame", 3);
      }
   }

   /*
    * RestartGame
    *
    * A simple level restart.
    */
   public void RestartGame()
   {
      Application.LoadLevel(Application.loadedLevel);
   }

}