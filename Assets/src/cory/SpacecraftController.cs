using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpacecraftController : MonoBehaviour
{
    public float speedForward = 5.0f;
    public float translationSpeed = 5.0f;
    public Vector2 bounds = new Vector2( 30.0f, 30.0f );
    public Vector2 maxTilt = new Vector2( 30.0f, 30.0f ); //Degrees
    public float pitchChange = 0.0f; //Pitch Rate of Change
    public float rollChange = 0.0f; //Roll Rate of Change
    public float returnToCenter = 0.0f;

    protected Vector3 positionDelta;
    protected Vector3 rotationDelta;
    private enum Axis { x = 0, y = 1};

    public SpacecraftController()
    {
        positionDelta = new Vector3(.0f, .0f, .0f);
    }
    public void Start()
    {
        positionDelta.z = speedForward;
    }
    public void Update()
    {
        PlayerInput();
        ApplyTranslation();
        //ApplyRotation();
    }
    protected void PlayerInput()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticleInput = Input.GetAxisRaw("Vertical");
        Vector2 parity = new Vector2(((transform.position.x > 0.0f) ? 1.0f : -1.0f), ((transform.position.y > 0.0f) ? 1.0f : -1.0f));
        if(CheckBounds((int)Axis.x, parity.x))
        {
            if (Mathf.Abs(horizontalInput) > .5f) {
                positionDelta.x = horizontalInput * translationSpeed * Time.deltaTime;
                rotationDelta.y = parity.x * rollChange * Time.deltaTime;
            } else
            {
                positionDelta.x = 0.0f;
                rotationDelta.y = transform.rotation.y / maxTilt.y * returnToCenter * parity.x;
            }
        } else
        {
            positionDelta.x = parity.x * -.001f;
            rotationDelta.x = transform.rotation.x / maxTilt.y* returnToCenter * parity.x;
        }
        if (CheckBounds((int)Axis.y, parity.y))
        {
            if (Mathf.Abs(verticleInput) > .5f)
            {
                positionDelta.y = verticleInput * translationSpeed * Time.deltaTime;
                rotationDelta.x = parity.y * pitchChange * Time.deltaTime;

            } else
            {
                positionDelta.y = 0.0f;
                rotationDelta.x = transform.rotation.x / maxTilt.x * returnToCenter * parity.y;
                
            }
        } else
        {
            positionDelta.y = parity.y * -.001f;
            rotationDelta.x = transform.rotation.x / maxTilt.x * returnToCenter * parity.y;
        }
    }
    protected bool CheckBounds(int axis, float parity)
    {
        var pos = transform.position;
        if ((parity * pos[axis]) <= bounds[axis])
        {
            return true;
        }
        return false;
    }
    protected void ApplyTranslation()
    {
        var position = transform.position;
        float[] parity = { ((position.x > 0.0f) ? 1.0f : -1.0f), ((position.y > 0.0f) ? 1.0f : -1.0f) };
        if (Mathf.Abs(position.x + positionDelta.x) > bounds[(int)Axis.x])
        {
            positionDelta.x = (bounds[(int)Axis.x] - Mathf.Abs(position.x)) * parity[(int)Axis.x];
        } 
        if(Mathf.Abs(position.y + positionDelta.y) > bounds[(int)Axis.y])
        {
            positionDelta.y = (bounds[(int)Axis.y] - Mathf.Abs(position.y)) * parity[(int)Axis.y];
        }
        transform.position += positionDelta;
    }
    protected void ApplyRotation()
    {
        Vector3 zero = new Vector3( 0.0f, 0.0f, 0.0f );
        var current = transform.rotation.eulerAngles;
        if (rotationDelta != zero)
        {
            Debug.Log(rotationDelta);
        }
        Vector3 newRotation = current;
        if (Mathf.Abs(current.x + rotationDelta.x) > maxTilt.x)
        {
            newRotation.x = maxTilt.y;
        } else {
            newRotation.x += rotationDelta.x;
        }
        if (Mathf.Abs(current.y + rotationDelta.y) > maxTilt.y)
        {
            newRotation.y = maxTilt.y;
        }
        else
        {
            newRotation.y += rotationDelta.y;
        }
        transform.rotation = Quaternion.Euler(newRotation);
        rotationDelta = new Vector3();
    }
}