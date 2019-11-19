using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyControls : MonoBehaviour
{
    Rigidbody spaceship;
    public float flyingSpeed = 40f;
    public float movementSpeed = 40f;
    // Start is called before the first frame update
    void Start()
    {
        spaceship = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        spaceship.velocity = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, Input.GetAxis("Vertical") * movementSpeed, flyingSpeed);
    }
}
