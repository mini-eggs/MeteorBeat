using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenMotion : MonoBehaviour
{
   float speed = 50f;
   public Vector3 pivot;

    // Update is called once per frame
    void Update()
    {
      transform.RotateAround(pivot, -Vector3.forward, speed * Time.deltaTime);
    }
}
