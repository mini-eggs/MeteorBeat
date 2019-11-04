using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpdateScore : MonoBehaviour
{
    public GameObject myText;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUIElements(float score)
    {
        ExecuteEvents.Execute<ICustomUIListener>(myText, null, (x, y) => x.UpdateUIElement(score));
    }
}
