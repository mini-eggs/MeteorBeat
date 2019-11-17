using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AsteroidIntegration
 *
 * Meta class. Simply play level soundtrack when level begins and stop playing
 * when level is over. Connect this script to the top level scene within Unity
 * editor.
 */
public class AsteroidIntegration : MonoBehaviour
{
  /* 
   * OnTriggerEnter
   *
   * When user an asteroid hits the user call ShipIntegration#collideWithAsteroid
   * that will handle playing sound/particle effects/other.
   */
  void OnTriggerEnter(){
    GameObject.FindGameObjectWithTag("Player").GetComponent<ShipIntegration>().collideWithAsteroid();
  }
}
