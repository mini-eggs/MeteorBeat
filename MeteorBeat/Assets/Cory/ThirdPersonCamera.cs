using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : CameraBase
{
	Vector3 lastRotation = new Vector3();
	public void Start()
	{
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
}
