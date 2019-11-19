using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * LevelGeneration
 *
 * Meta class. Simply play level soundtrack when level begins and stop playing
 * when level is over. Connect this script to the top level scene within Unity
 * editor.
 */
public class LevelIntegration : MonoBehaviour
{
  
  /*
   * Start
   *
   * Grab instance of BeatBox singleton and begin playing soundtrack.
   */
  void Start()
  {
    var s = BeatBox.Instance;
    s.LoadSounds(this.gameObject);
    s.PlayLevelSoundTrack();
  }

}
