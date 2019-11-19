using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverShoulderCamera : CameraBase
{
	Vector3 lastRotation = new Vector3();
	OverShoulderCamera()
	{
		Angle  = overShoulderAngle;
		Offset = overShoulderOffset;
	}
	public void Initialize()
	{
		SetCameraPosition();
	}
	protected override void Update()
	{
		var transformMaster = target.transform;
		Vector3 rotationDelta = transformMaster.rotation.eulerAngles - lastRotation;
		lastRotation = transformMaster.rotation.eulerAngles;
		transform.RotateAround(transformMaster.position, Vector3.right, rotationDelta.x);
		transform.RotateAround(transformMaster.position, Vector3.forward, rotationDelta.z);
		transform.RotateAround(transformMaster.position, Vector3.up, rotationDelta.y);
	}
	protected override void SetCameraPosition()
	{
		transform.position = target.transform.position + Offset;
		transform.rotation = Quaternion.Euler(Angle);
	}
	static private Vector3 overShoulderAngle = new Vector3(5f, 5f, 0f);
	static private Vector3 overShoulderOffset = new Vector3(1f, .75f, -3f);
}
