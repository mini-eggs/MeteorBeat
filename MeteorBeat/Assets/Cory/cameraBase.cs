using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBase : MonoBehaviour
{
	protected Vector3		angle;
	protected Vector3		offset;
	public    GameObject target;
	virtual public GameObject Target
	{
		get { return target;  }
		set { target = value; }
	}
	virtual public Vector3 Angle
	{
		get { return angle;  }
		set { angle = value; }
	}
	virtual public Vector3 Offset
	{
		get { return offset;  }
		set { offset = value; }
	}
	public 
    // Start is called before the first frame update
    virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
		CameraFollow();
    }
	protected virtual void SetCameraPosition()
	{

	}
	protected virtual void CameraFollow()
	{

	}
}
