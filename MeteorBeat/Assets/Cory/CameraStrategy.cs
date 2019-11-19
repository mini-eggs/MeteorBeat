using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* CameraStrategy 
 *	Class that follows the strategy pattern attmpting to polymorphize functionality with the CameraBase subclass
 * 
 * 
 */
public class CameraStrategy : MonoBehaviour
{
	
	public CameraType type; //Type of Camera in use
	public GameObject target; //Target for Camera
	protected CameraBase strategy; //Dynamically binded camera
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
