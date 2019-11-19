using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractCameraFactory : MonoBehaviour
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
		switch (cameraType)
		{
			case CameraType.thirdPerson:
				var camera = CameraFactory.ThirdPersonFactory(gameObject, target);
				break;
			case CameraType.overTheShoulder:
				var shoulderCamera = CameraFactory.OverShoulderFactory(gameObject, target);
				break;
		};
	}

	// Update is called once per frame
	void Update()
	{

	}
}
