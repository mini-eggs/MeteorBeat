using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : CameraBase
{
	Vector3 lastRotation = new Vector3();
	ThirdPersonCamera()
	{
		Angle = thirdPersonAngle;
		Offset = thirdPersonOffset;
	}
	public void Start()
	{
	}
	public void Initialize()
	{
		SetCameraPosition();
	}
	public override void Update()
	{
		CameraFollow();
	}
	protected override void SetCameraPosition()
	{
		transform.position = target.transform.position + Offset;
		transform.rotation = Quaternion.Euler(Angle);
	}
	protected override void CameraFollow()
	{

	}
	static private Vector3 thirdPersonAngle = new Vector3(15f, 0f, 0f);
	static private Vector3 thirdPersonOffset = new Vector3(0f, 2f, -5.5f);
}
