using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{

    public Transform target;
    public Vector3 offSet;
    public float rotateSpeed;
    public bool useOffSetValues;
    public Transform pivot;

    private void Start()
    {
        if (!useOffSetValues)
        {
            offSet = target.position - transform.position;
        }
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        //This makes our cursor to stop being visble when we start playing the game 
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        //All of this makes that the camera can't go beyond a certain limit, in the case of looking up or lookind down, or at least it's supposed to do.
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, -horizontal, 0);

        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);

        if (pivot.rotation.eulerAngles.x > 45 && pivot.rotation.eulerAngles.x < 180)
        {
            pivot.rotation = Quaternion.Euler(65f, 0, 0);
        }

        if (pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 315)
        {
            pivot.rotation = Quaternion.Euler(315f, 0, 0);
        }

        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offSet);

        if (transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }

        transform.LookAt(target);



    }


}