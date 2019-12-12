using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Object Pattern Class for movement
//So that auto testing is easier
public class Movement
{
	public float avgNumer = 19;
	public float avgDemon = 20;
	public virtual Vector2 Average(Vector2 prev, float x, float y)
	{
		return new Vector2();
	}
	public virtual Vector3 Calculate(float h, float v, float deltaT)
	{
		return new Vector3();
	}
}
