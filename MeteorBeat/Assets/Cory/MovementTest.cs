using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class RotationTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void RotationTestHorizontalSimplePasses()
        {
			Movement movement = new Movement();
			Quaternion quat = new Quaternion();
			for (int i = 0; i < 1e6; i++)
			{
				quat = movement.Rotation(1, 0);
			}
			for (int i = 0; i < 1e6; i++)
			{
				quat = movement.Rotation(-1, 0);
			}
			Assert.IsTrue(Mathf.Abs(quat.eulerAngles.z) <= 30.1f);
			Debug.Log(quat.eulerAngles);
			// Use the Assert class to test conditions
		}
			[Test]
			public void RotationTestVerticalSimplePasses()
			{
				Movement movement = new Movement();
				Quaternion quat = new Quaternion();
				for (int i = 0; i < 1e6; i++)
				{
					quat = movement.Rotation(0, 1);
				}
				for (int i = 0; i < 1e6; i++)
				{
					quat = movement.Rotation(0, -1);
				}
				Assert.IsTrue(Mathf.Abs(quat.eulerAngles.x) <= 20.1f);
			// Use the Assert class to test conditions
		}

    }
}
