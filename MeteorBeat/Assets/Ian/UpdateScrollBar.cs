using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpdateScrollBar : UpdateUIElements
{
    float delay = 0.2f;
    public float startZ, endZ;
    public GameObject myScrollBar;
    // Start is called before the first frame update
    void Start()
    {
        startZ = transform.position.z;
        StartCoroutine(UpdateUIElement());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override IEnumerator UpdateUIElement()
    {
        while (true)
        {
            ExecuteEvents.Execute<ICustomUIListener>(myScrollBar, null, (x, y) => x.UpdateUIElement((transform.position.z - startZ) / (endZ - startZ)));
            yield return new WaitForSeconds(delay);
        }
    }
}
