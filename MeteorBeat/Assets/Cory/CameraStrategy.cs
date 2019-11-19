using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStrategy : MonoBehaviour
{
	
	public CameraType type;
	public GameObject target;
	protected CameraBase strategy;
	public CameraType Strategy
	{
		get { return type; }
		set { type = value; strategy = AbstractCameraFactory.Factory(type, gameObject, target); }
	}
    // Start is called before the first frame update
    void Start()
    {
		strategy = AbstractCameraFactory.Factory(type, gameObject, target);
    }

    // Update is called once per frame
    void Update()
    {
		strategy.Update();
    }
}
