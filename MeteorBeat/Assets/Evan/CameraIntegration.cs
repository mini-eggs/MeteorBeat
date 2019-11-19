using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CameraIntegration
 *
 * Meta script. Attach to camera object. 
 *
 * On ship and asteroid collision this script will lock camera so user sees
 * their explosion. Camera is locked until game is exited or level restarts.
 */
public class CameraIntegration : MonoBehaviour
{

  // Signifies if user is in loss state, i.e. ship collided with asteroid.
  private bool isLocked = false;
  
  // The position when the camera was locked.
  private Vector3 lastPosition;

  // The camera game object itself;
  private GameObject cam;

  void Start() 
  {
    cam = GameObject.FindGameObjectWithTag("MainCamera");
  }

  void Update() 
  {
    // Hold lastPosition if camera is in lock mode.
    if (isLocked) 
    {
      cam.transform.position = lastPosition;
    }
  }

  public void Lock() 
  {
    isLocked = true;
    lastPosition = cam.transform.position;
  }

  public void Unlock() 
  {
    isLocked = false;
  }

}
