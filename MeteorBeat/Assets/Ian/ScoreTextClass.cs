using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextClass : MonoBehaviour, ICustomUIListener
{
    Text myText;
    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
    }


    public void UpdateUIElement(float info)
    {
        myText.text = "Score: " + info.ToString();
    }
}
