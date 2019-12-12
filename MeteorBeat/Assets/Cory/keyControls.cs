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
	// Start is called before the first frame update
	void Start()
	{
		movement = new Movement();
		spaceship = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	//No Delta T?
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.K))
		{
			flyingSpeed += 1e2f;
		}
		else if (Input.GetKeyDown(KeyCode.L))
		{
			flyingSpeed += 1e4f;
		}
		var x = Input.GetAxis("Horizontal");
		var y = Input.GetAxis("Vertical");
		spaceship.velocity = movement.Calculate(x, y, movementSpeed, flyingSpeed);
		shipModel.rotation = movement.Rotation(x, y);
	}
}
