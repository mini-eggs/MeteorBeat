using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarClass : MonoBehaviour, ICustomUIListener
{
    public Scrollbar myScrollBar;
    // Start is called before the first frame update
    void Start()
    {
        myScrollBar = GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateUIElement(float info)
    {
        myScrollBar.value = info;
    }
}
