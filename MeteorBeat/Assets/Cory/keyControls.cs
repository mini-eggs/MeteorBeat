using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class keyControls : MonoBehaviour
{
   Rigidbody spaceship;
   public float flyingSpeed = 40f;
   public float movementSpeed = 40f;
   public Transform shipModel;
   private float lastAverageX = 0f;
	private float lastAverageY = 0f;
	private Quaternion vrRotation = new Quaternion();
	public void VRInput(Transform e)
	{
		vrRotation = e.rotation;
	}
    // Start is called before the first frame update
    void Start()
    {
        spaceship = GetComponent<Rigidbody>();
    }
	Vector2 VRRotationToInput(Quaternion rot)
	{
		Vector3 euler = rot.eulerAngles;
		Vector2 temp;
		if (euler.x > 270f || euler.x < 90f)
			temp.y = Mathf.Sin(Mathf.Deg2Rad * euler.x) *  -1.0f;
		else
			temp.y = 0f;
		if (euler.y > 270f || euler.y < 90f)
			temp.x = Mathf.Sin(Mathf.Deg2Rad * euler.y);
		else
			temp.x = 0f;
		Debug.Log("x " + Mathf.Sin(euler.x) + "y: " + Mathf.Sin(euler.y) + " " + temp);
		return temp;
	}
    // Update is called once per frame
	void Update()
	{
		Vector2 input = VRRotationToInput(vrRotation);
		float averageX = (lastAverageX * 19 + input.x) / 20;
		float averageY = (lastAverageY * 19 + input.y) / 20;
		spaceship.velocity  = new Vector3(input.x * movementSpeed, input.y * movementSpeed, flyingSpeed);
		shipModel.rotation = Quaternion.Euler(-averageY * 20, 0, -averageX * 30);
		lastAverageX = averageX;
		lastAverageY = averageY;
	}
}
