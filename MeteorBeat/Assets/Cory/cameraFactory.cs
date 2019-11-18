using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFactory
{
	static public ThirdPersonCamera ThirdPersonFactory(GameObject parent, GameObject target)
	{
		var temp = parent.AddComponent<ThirdPersonCamera>() as ThirdPersonCamera;
		temp.Target = target;
		temp.Angle = thirdPersonAngle;
		temp.Offset = thirdPersonOffset;
		temp.Initialize();
		return temp;
	}

	static public ThirdPersonCamera OverShoulderFactory(GameObject parent, GameObject target)
	{

		var temp = parent.AddComponent<ThirdPersonCamera>() as ThirdPersonCamera;
		temp.Target = target;
		temp.Angle = overShoulderAngle;
		temp.Offset = overShoulderOffset;
		temp.Initialize();
		return temp;

	}
	static public CameraBase BasicCamera(GameObject parent)
	{
		var temp = parent.AddComponent<CameraBase>();
		return temp;
	}
	static private Vector3 thirdPersonAngle = new Vector3(15f, 0f, 0f);
	static private Vector3 thirdPersonOffset = new Vector3(0f, 2f, -5.5f);
	static private Vector3 overShoulderAngle = new Vector3(5f, 5f, 0f);
	static private Vector3 overShoulderOffset = new Vector3(1f, .75f, -3f);

}
