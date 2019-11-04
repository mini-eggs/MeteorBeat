using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpdateUIElements : MonoBehaviour
{
    abstract public IEnumerator UpdateUIElement();
}
