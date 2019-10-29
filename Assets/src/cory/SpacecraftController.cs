using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpacecraftController : MonoBehaviour
{
	public float speedForward = 5.0f; //Forward Velocity of craft
	public float translationSpeed = 5.0f; //Speeds of movement in x / y directions
	public Vector2 bounds = new Vector2(10.0f, 6.0f); //Maximun bounds of craft
	public Vector3 maxTiltDelta = new Vector3(30.0f, 30.0f); //Degrees

	public float pitchChange = 0.0f; //Pitch Rate of Change
	public float rollChange = 0.0f; //Roll Rate of Change
	public float returnToCenter = 0.0f; //Return to 0 of rotation
	public GameObject childMesh; //Child Object

	protected Vector3 positionDelta;
	protected Vector3 rotationDelta;

	protected Vector3 maxTilt = new Vector2();
	protected Vector3 centerTilt = new Vector2();
	protected Vector3 minTilt = new Vector2();
	private enum Axis { x = 0, y = 1, z = 2};

	public SpacecraftController()
	{
		positionDelta = new Vector3(.0f, .0f, .0f);
		rotationDelta = new Vector3(.0f, .0f, .0f);
	}
	public void Start()
	{
		CalculateInitialTilt();
		positionDelta.z = speedForward;
	}
	public void Update()
	{
		PlayerInput();
		ApplyTranslation();
		ApplyRotation();
	}
	void CalculateInitialTilt()
	{
		centerTilt = childMesh.transform.rotation.eulerAngles;
		maxTilt = new Vector3(centerTilt.x + maxTiltDelta.x, 0.0f, centerTilt.z + maxTiltDelta.z);
		minTilt = new Vector3(centerTilt.x - maxTiltDelta.x, 0.0f, centerTilt.z - maxTiltDelta.z);
		Debug.Log("CenterTilt: " + centerTilt);
	}
	/* PlayerInput
	 * This transplate player input into a delta for movement and rotation through getting 
	 * raw input and applying it to 2 different vectors
	 */
	protected void PlayerInput()
	{
		var horizontalInput	= Input.GetAxisRaw("Horizontal");
		var verticleInput		= Input.GetAxisRaw("Vertical");
		Vector2 sign = new Vector2(((transform.position.x > 0.0f) ? 1.0f : -1.0f),
										   ((transform.position.y > 0.0f) ? 1.0f : -1.0f));
		if (CheckBounds((int)Axis.x, sign.x))
		{
			HorizontalMotion(horizontalInput, sign);
		}
		else
		{
			positionDelta.x = sign.x * -.001f;
		}
		if (CheckBounds((int)Axis.y, sign.y))
		{
			VerticalMotion(verticleInput, sign);
		}
		else
		{
			positionDelta.y = sign.y * -.001f;
		}
	}
	/* CheckBounds
	 * Checks if current transform position is with in bounds of playable area
	 * @param axis   This is indexer for position
	 * @param sign sign of position. It is cheaper to use older sign then recalculate within function
	 * @return If within bounds
	 */
	protected bool CheckBounds(int axis, float sign)
	{
		var pos = transform.position;
		if ((sign * pos[axis]) <= bounds[axis])
		{
			return true;
		}
		return false;
	}
	/* ApplyTranslation
	 * Applies positionDelta to current position while making sure the delta does not exede current bounds 
	 */
	protected void ApplyTranslation()
	{
		var position = transform.position;
		Vector2 sign = new Vector2(((position.x > 0.0f) ? 1.0f : -1.0f), ((position.y > 0.0f) ? 1.0f : -1.0f));
		if (Mathf.Abs(position.x + positionDelta.x) > bounds[(int)Axis.x])
		{
			positionDelta.x = (bounds[(int)Axis.x] - Mathf.Abs(position.x)) * sign.x;
		}
		if (Mathf.Abs(position.y + positionDelta.y) > bounds[(int)Axis.y])
		{
			positionDelta.y = (bounds[(int)Axis.y] - Mathf.Abs(position.y)) * sign.y;
		}
		transform.position += positionDelta;
	}
	/* Applies rotation to the child mesh so not interfere with parent object's position.
	 * 
	 */
	protected void ApplyRotation()
	{
		var current				= childMesh.transform.rotation.eulerAngles;
		Vector3 newRotation	= rotationDelta;
		/*
		Vector3 sign			= new Vector3((current.x > .0f) ? 1.0f : -1.0f, 
													  (current.y > .0f) ? 1.0f : -1.0f, 
													  (current.z > .0f) ? 1.0f : -1.0f);
		if (Mathf.Abs((current.x + rotationDelta.x) - centerTilt.x) > maxTiltDelta.x)
		{
			newRotation.x = maxTiltDelta.x * sign.x + centerTilt.x;
		}
		if (Mathf.Abs((current.z + rotationDelta.z) - centerTilt.z) > maxTilt.z) 
		{
			newRotation.z = maxTiltDelta.z * sign.z - centerTilt.z;
		}*/
		childMesh.transform.Rotate(rotationDelta);
		rotationDelta = new Vector3(0.0f, 0.0f, 0.0f);
	}
	void HorizontalMotion(float input, Vector2 sign)
	{
		var childRotation = childMesh.transform.rotation.eulerAngles;
		if (Mathf.Abs(input) > .5f)
		{
			positionDelta.x = input * translationSpeed * Time.deltaTime;
			rotationDelta.z = input * Time.deltaTime * rollChange * -1.0f;
		}
		else
		{
			//This code is repetitive
			positionDelta.x = 0.0f;
			//Debug.Log("ChildRotation: " + childRotation + " " + centerTilt + " " + (childRotation.z - centerTilt.z));
		}
	}
	void VerticalMotion(float input, Vector2 sign)
	{
		var childRotation = childMesh.transform.rotation.eulerAngles;
		if (Mathf.Abs(input) > .5f)
		{
			positionDelta.y = input * translationSpeed * Time.deltaTime;
			rotationDelta.x = input * Time.deltaTime * pitchChange * -1.0f;
		}
		else
		{
			positionDelta.y = 0.0f;
		}
	}
}
