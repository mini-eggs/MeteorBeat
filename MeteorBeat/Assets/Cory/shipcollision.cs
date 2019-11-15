using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipcollision : MonoBehaviour
{
	public GameObject childMesh;
    // Start is called before the first frame update
    void Start()
    {
		BeatBox.Instance.LoadSounds(gameObject);
    }
	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Asteroid")
		{
			BeatBox.Instance.PlayCollision();
		}

	}
}
