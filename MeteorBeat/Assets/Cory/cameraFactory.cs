using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFactory
{
	static public ThirdPersonCamera ThirdPersonFactory(GameObject parent, GameObject target)
	{
		var temp = parent.AddComponent<ThirdPersonCamera>() as ThirdPersonCamera;
		temp.Target = target;
		temp.Initialize();
		return temp;
	}

	static public OverShoulderCamera OverShoulderFactory(GameObject parent, GameObject target)
	{

		var temp = parent.AddComponent<OverShoulderCamera>() as OverShoulderCamera;
		temp.Target = target;
		temp.Initialize();
		return temp;

	}
	static public CameraBase BasicCamera(GameObject parent)
	{
		var temp = parent.AddComponent<CameraBase>();
		return temp;
	}


}
