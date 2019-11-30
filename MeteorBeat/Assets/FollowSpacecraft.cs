using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSpacecraft : MonoBehaviour
{
	public GameObject spacecraft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 updateTransform = spacecraft.transform.position;
		updateTransform.z += 8.0f;
		this.transform.position = updateTransform;
		this.transform.rotation = spacecraft.transform.rotation;
    }
}
