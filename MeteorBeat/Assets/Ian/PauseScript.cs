using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
// Class that will bring up the pause menu and set the time scale of the game to zero.
public class PauseScript : MonoBehaviour
{
   public GameObject pausePlane;
   public Button restartButton, quitButton;

   // Update is called once per frame
   void Update()
   {

      if((Input.GetKeyDown(KeyCode.Escape) || SteamVR_Actions._default.Pause.GetStateDown(SteamVR_Input_Sources.Any)) && Time.timeScale == 1)
      {
         pausePlane.SetActive(true);
         restartButton.gameObject.SetActive(true);
         quitButton.gameObject.SetActive(true);
         Time.timeScale = 0;
         BeatBox.Instance.PauseLevelSoundTrack();
      }
      else if((Input.GetKeyDown(KeyCode.Escape)|| SteamVR_Actions._default.Pause.GetStateDown(SteamVR_Input_Sources.Any)) && Time.timeScale == 0)
      {
         pausePlane.SetActive(false);
         restartButton.gameObject.SetActive(false);
         quitButton.gameObject.SetActive(false);
         Time.timeScale = 1;
         BeatBox.Instance.UnpauseLevelSoundTrack();
      }
   }
}
