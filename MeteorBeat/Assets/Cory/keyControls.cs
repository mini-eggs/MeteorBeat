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
    // Start is called before the first frame update
    void Start()
    {
        spaceship = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float averageX = (lastAverageX * 19 + Input.GetAxis("Horizontal")) / 20;
        float averageY = (lastAverageY * 19 + Input.GetAxis("Vertical")) / 20;
        
        spaceship.velocity = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, Input.GetAxis("Vertical") * movementSpeed, flyingSpeed);
        shipModel.rotation = Quaternion.Euler(-averageY*20,0,-averageX*30);
        lastAverageX = averageX;
        lastAverageY = averageY;
    }
}
