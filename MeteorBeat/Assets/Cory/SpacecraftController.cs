using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpacecraftController : MonoBehaviour
{
	public float speedForward = 5.0f;
	
	public float translationSpeed = 5.0f; //Speeds of movement in x / y directions
	public Vector2 bounds = new Vector2(10.0f, 6.0f); //Maximun bounds of craft
	public Vector3 maxTiltDelta = new Vector3(30.0f, 0.0f, 30.0f); //Degrees

	public float pitchChange = 0.0f; //Pitch Rate of Change
	public float rollChange = 0.0f; //Roll Rate of Change
	public float returnToCenter = 0.0f; //Return to 0 of rotation
	/* The heiarchy must be so that camera nor movement is messed up by rotation
	 * Spacecraft : Gameobject with this script
	 * -> GameObject : (Empty and is pointed by childMesh)
	 *		-> Prefab 
	 *			-> Shapes
	 */
	public GameObject childMesh; //Child Object
	protected Vector3 positionDelta; //Deltas of each transformation per tick
	protected Vector3 rotationDelta;
	private enum Axis { x = 0, y = 1, z = 2};
	private float startPosition;
	/* Accessors */
	public float ForwardSpeed
	{
		get
		{
			return speedForward;
		}
		set
		{
			speedForward = value;
		}
	}//Forward Velocity of craft
	public float TransitionSpeed
	{
		get
		{
			return translationSpeed;
		}
		set
		{
			translationSpeed = value;
		}
	}
	public float PitchChange
	{
		get
		{
			return pitchChange;
		}
		set
		{
			pitchChange = value;
		}
	}
	public float RollChange
	{
		get
		{
			return rollChange;
		}
		set
		{
			rollChange = value;
		}
	}


	public SpacecraftController()
	{
		positionDelta = new Vector3(.0f, .0f, .0f);
		rotationDelta = new Vector3(.0f, .0f, .0f);
	}
	public void Start()
	{
		positionDelta.z = speedForward;
		startPosition = transform.position.z;
	}
	public void Update()
	{
		PlayerInput();
		ApplyTranslation();
		ApplyRotation();
	}
	/* PlayerInput
	 * This transplate player input into a delta for movement and rotation through getting 
	 * raw input and applying it to 2 different vectors
	 */
	protected void PlayerInput()
	{
		var horizontalInput	= Input.GetAxisRaw("Horizontal");
		var verticleInput		= Input.GetAxisRaw("Vertical");
		Vector2 sign = new Vector2(CheckSign(transform.position.x), CheckSign(transform.position.y));
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
		positionDelta = new Vector3(0.0f, 0.0f, speedForward);
	}
	/* Applies rotation to the child mesh so not interfere with parent object's position.
	 * Half of the function is removed due to issues with the spacecraft returning to level flight
	 */
	protected void ApplyRotation()
	{
		var childRotation		= childMesh.transform.rotation.eulerAngles;
		Vector3 boundedDelta = rotationDelta;
		if (childRotation.x > 160.0f)
			childRotation.x	= childRotation.x - 360.0f;
		if (childRotation.z > 160.0f)
			childRotation.z	= childRotation.z - 360.0f;
		if (Mathf.Abs(rotationDelta.x + childRotation.x) > maxTiltDelta.x)
			boundedDelta.x = ((maxTiltDelta.x - .25f) - Mathf.Abs(childRotation.x)) * CheckSign(childRotation.x);
		if (Mathf.Abs(rotationDelta.z + childRotation.z) > maxTiltDelta.z)
			boundedDelta.z = ((maxTiltDelta.z - .25f) - Mathf.Abs(childRotation.z)) * CheckSign(childRotation.z);
		childMesh.transform.Rotate(boundedDelta);
		rotationDelta = new Vector3(0.0f, 0.0f, 0.0f);
	}
	/* Compartimentalization of Movement function */
	void HorizontalMotion(float input, Vector2 sign)
	{
		if (Mathf.Abs(input) > .5f)
		{
			positionDelta.x = input * translationSpeed * Time.deltaTime;
			rotationDelta.z = input * Time.deltaTime * rollChange * -1.0f;
		}
		else
		{
			//This code is repetitive
			positionDelta.x = 0.0f;
			ReturnToCenter((int)Axis.z);
		}
	}
	void VerticalMotion(float input, Vector2 sign)
	{
		if (Mathf.Abs(input) > .5f)
		{
			positionDelta.y = input * translationSpeed * Time.deltaTime;
			rotationDelta.x = input * Time.deltaTime * pitchChange * -1.0f;
		}
		else
		{
			positionDelta.y = 0.0f;
			ReturnToCenter((int)Axis.x);
		}
	}
	/*
	 * 
	 * 
	 */
	void ReturnToCenter(int axis)
	{
		float childRotation = childMesh.transform.rotation.eulerAngles[axis];
		if (childRotation > 160.0f)
			childRotation = childRotation - 360.0f; //Makes rotation signed
		if(Mathf.Abs(childRotation) > 1.0f)
		{
			rotationDelta[axis] = returnToCenter * Time.deltaTime * -1.0f * CheckSign(childRotation);
		}
	}
	float CheckSign(float value)
	{
		return ((value > 0.0f) ? 1.0f : -1.0f);
	}
}
