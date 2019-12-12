using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class that updates the score UI element
public class ScoreTextClass : MonoBehaviour
{
   public int Score = 0;
   public Text myText;
    
   // Start is called before the first frame update
   
   // Set the score equal to the value passed in info.
   public void UpdateUIElement()
   {
      Score += 100;
      myText = GetComponent<Text>();
      myText.text = "Score: " + Score.ToString();
   }
}
