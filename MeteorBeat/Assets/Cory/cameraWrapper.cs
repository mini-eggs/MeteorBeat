﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraWrapper : MonoBehaviour
{
	
	public enum CameraType
	{
		thirdPerson = 0,
		overTheShoulder = 1,
		sideScroller = 2
	};
	public CameraType cameraType;
	public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        if(cameraType == CameraType.thirdPerson)
		{
			var camera = CameraFactory.ThirdPersonFactory(gameObject, target);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
