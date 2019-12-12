using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyControls : MonoBehaviour
{
	Rigidbody spaceship;
	public float flyingSpeed = 40f;
	public float movementSpeed = 40f;
	public  Transform shipModel;
	public  Movement movement;
	public Func<Vector2, Vector2, float, float> averager;
	private Vector2 lastAverage;
	// Start is called before the first frame update
	void Start()
	{
		spaceship = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 average = movement.Average(lastAverage, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		float averageX = 0;
		float averageY = 0; ;// = (lastAverageY * 19 + Input.GetAxis("Vertical")) / 20;

		spaceship.velocity = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, Input.GetAxis("Vertical") * movementSpeed, flyingSpeed);
		shipModel.rotation = Quaternion.Euler(-averageY * 20, 0, -averageX * 30);
		lastAverage = average;
	}
}
