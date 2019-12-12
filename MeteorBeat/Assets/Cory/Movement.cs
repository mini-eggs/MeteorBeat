using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Object Pattern Class for movement
//So that auto testing is easier
public class Movement
{
	public float avgNumer = 19;
	public float avgDemon = 20;
	public Vector2 maxRotation = new Vector2(20f, 30f);
	private Vector2 lastAverage;
	public Movement()
	{
		avgNumer = 19;
		avgDemon = 20;
	}
	public virtual Vector2 Average(float x, float y)
	{
		//f(a_n, i_n) = (f(a_n-1, i_n-1) * Numer + i_n ) / Demon;
		return new Vector2((lastAverage.x * avgNumer + x) / avgDemon, (lastAverage.y * avgNumer + y) / avgDemon);
	}
	public virtual Vector3 Calculate(float x, float y, float speed, float speedZ)
	{
		return new Vector3(x * speed, y * speed, speedZ);
	}
	public virtual Quaternion Rotation(float x, float y)
	{
		lastAverage = Average(x, y);
		Quaternion rot = Quaternion.Euler(-lastAverage.y * maxRotation.x, 0, -lastAverage.x * maxRotation.y);
		return rot;
	}
}
