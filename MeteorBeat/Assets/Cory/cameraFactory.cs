using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFactory
{
	static public ThirdPersonCamera ThirdPersonFactory(GameObject parent, GameObject target)
	{
		var temp = parent.AddComponent<ThirdPersonCamera>() as ThirdPersonCamera;
		temp.Target = target;
		temp.Angle	= thirdPersonAngle;
		temp.Offset = thirdPersonOffset;
		return temp;
	}

	static OverShoulderCamera OverShoulderFactory()
	{
		
		return new OverShoulderCamera();
	}
	static private Vector3 thirdPersonAngle = new Vector3(15f, 0f, 0f);
	static private Vector3 thirdPersonOffset = new Vector3(0f, 2f, -5.5f);

}
