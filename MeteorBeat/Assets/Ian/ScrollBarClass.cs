using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class for the scroll bar. Implements UpdateUIElement from ICustomUIListener class.
public class ScrollBarClass : MonoBehaviour
{
   Scrollbar myScrollBar;
   // Start is called before the first frame update
   float timePlayed = 0;
   float songDuration = 322;
   void Start()
   {
      myScrollBar = GetComponent<Scrollbar>();
      
   }
   void Update(){
      timePlayed += Time.deltaTime;
      float progress = timePlayed/songDuration;
      myScrollBar.value=progress;
   }
}
