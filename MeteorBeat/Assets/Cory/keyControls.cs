using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpacecraftController : MonoBehaviour
{
	public float speedForward = 5.0f; //Forward Velocity of craft
	public float translationSpeed = 5.0f; //Speeds of movement in x / y directions

	public Vector2 bounds = new Vector2(10.0f, 6.0f); //Maximun bounds of craft
	public Vector2 maxTilt = new Vector2(30.0f, 30.0f); //Degrees

	public float pitchChange = 0.0f; //Pitch Rate of Change
	public float rollChange = 0.0f; //Roll Rate of Change
	public float returnToCenter = 0.0f; //Return to 0 of rotation

	public GameObject childMesh; //Child Object
    
	protected Vector3 positionDelta;
	protected Vector3 rotationDelta;
    protected Rigidbody spacecraft;
	public SpacecraftController()
	{
		positionDelta = new Vector3(.0f, .0f, .0f);
		rotationDelta = new Vector3(.0f, .0f, .0f);
	}
	public void Start()
	{
		positionDelta.z = speedForward;
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
	 * 
	 * 
	 */
	protected void PlayerInput()
	{
		var horizontalInput = Input.GetAxisRaw("Horizontal");
		var verticleInput   = Input.GetAxisRaw("Vertical");
		var childRotation	  = childMesh.transform.rotation;
		Vector2 sign        = new Vector2(((transform.position.x > 0.0f) ? 1.0f : -1.0f), 
													 ((transform.position.y > 0.0f) ? 1.0f : -1.0f));
		if (CheckBounds((int)Axis.x, sign.x))
		{
			if (Mathf.Abs(horizontalInput) > .5f)
			{
				positionDelta.x = horizontalInput * translationSpeed * Time.deltaTime;
				rotationDelta.y = sign.x * rollChange * Time.deltaTime;
				Debug.Log(rotationDelta.y);
			}
			else
			{
				//This code is repetitive
				positionDelta.x = 0.0f;
				rotationDelta.y = childRotation.y / maxTilt.y * returnToCenter * sign.x; //A slow return to center axis
			}
		}
		else
		{
			positionDelta.x = sign.x * -.001f;
			rotationDelta.x = childRotation.x / maxTilt.y * returnToCenter * sign.x;
		}
		if (CheckBounds((int)Axis.y, sign.y))
		{
			if (Mathf.Abs(verticleInput) > .5f)
			{
				positionDelta.y = verticleInput * translationSpeed * Time.deltaTime;
				rotationDelta.x = sign.y * pitchChange * Time.deltaTime;

			}
			else
			{
				positionDelta.y = 0.0f;
				rotationDelta.x = childRotation.x / maxTilt.x * returnToCenter * sign.y;

			}
		}
		else
		{
			positionDelta.y = sign.y * -.001f;
			rotationDelta.x = childRotation.x / maxTilt.x * returnToCenter * sign.y;
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
		var current = childMesh.transform.rotation.eulerAngles;
		Vector3 newRotation = rotationDelta; 
    Vector2 sign = new Vector2((current.x > .0f) ? 1.0f : -1.0f, (current.y > .0f) ? 1.0f : -1.0f);
		if (Mathf.Abs(current.x + rotationDelta.x) > maxTilt.x)
		{
			newRotation.x = maxTilt.y * sign.x;
		}
		if (Mathf.Abs(current.y + rotationDelta.y) > maxTilt.y)
		{
			newRotation.y = maxTilt.y * sign.y;
		}
		childMesh.transform.Rotate(newRotation);
		rotationDelta = new Vector3();
	}
}
