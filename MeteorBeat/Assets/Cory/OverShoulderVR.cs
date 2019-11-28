using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverShoulderVR : OverShoulderCamera
{
	public OverShoulderVR()
	{
		Angle  = overShoulderAngleVR;
		Offset = overShoulderOffsetVR;
	}
	public void Initialize()
	{
		SetCameraPosition();
	}
	static private Vector3 overShoulderAngleVR = new Vector3(0f, 0f, 0f);
	static private Vector3 overShoulderOffsetVR = new Vector3(.75f, -.2f, -3.0f);
}
