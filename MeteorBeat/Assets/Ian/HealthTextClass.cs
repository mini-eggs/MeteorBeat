using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTextClass : MonoBehaviour, ICustomUIListener
{
   Text myHealthText;
   // Start is called before the first frame update
   void Start()
   {
      myHealthText = GetComponent<Text>();
   }

   public void UpdateUIElement(float info)
   {
      myHealthText.text = "Score: " + info.ToString();
   }
}
