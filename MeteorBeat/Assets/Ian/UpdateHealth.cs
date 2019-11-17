using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpdateHealth : UpdateUIElements
{
   //Get health
   // Start is called before the first frame update
   public GameObject myHealthText;

   public override void UpdateUIElement(float info)
   {
      ExecuteEvents.Execute<ICustomUIListener>(myHealthText, null, (x, y) => x.UpdateUIElement(info));
      throw new System.NotImplementedException();
   }
}
