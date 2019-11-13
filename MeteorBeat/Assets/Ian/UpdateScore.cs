using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Class that contains a function which can update the score text UI element.
public class UpdateScore : MonoBehaviour
{
   public GameObject myText;

   // Function that, when called, will update the score on screen.
   public void UpdateUIElements(float score)
   {
      ExecuteEvents.Execute<ICustomUIListener>(myText, null, (x, y) => x.UpdateUIElement(score));
   }
}
