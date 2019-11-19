using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CameraType
{
	thirdPerson = 0,
	overTheShoulder = 1,
	sideScroller = 2
};
public class AbstractCameraFactory
{
	/* Abstract Factory Calling Concrete Factories of Cameras */
	static public CameraBase Factory(CameraType type, GameObject gameObject, GameObject target) {
		switch (type)
		{
			case CameraType.thirdPerson:
				var camera = CameraFactory.ThirdPersonFactory(gameObject, target);
				return camera;
				break;
			case CameraType.overTheShoulder:
				var shoulderCamera = CameraFactory.OverShoulderFactory(gameObject, target);
				return shoulderCamera;
				break;
			default:
				return CameraFactory.BasicCamera(gameObject);
				break;
		};
	}
}
