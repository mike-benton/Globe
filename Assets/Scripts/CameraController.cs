using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float cameraRadius;

    // Start is called before the first frame update
    void Start()
    {
        cameraRadius = Vector3.Distance(transform.position, new Vector3());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y != 0) //Zooming logic
        {
            cameraRadius -= .5f * Input.mouseScrollDelta.y;

            if (cameraRadius < 3)
                cameraRadius = 3;
            if (cameraRadius > 10)
                cameraRadius = 10;

            transform.position = transform.position.normalized * cameraRadius;
            
        }
        /**
        if (Input.GetMouseButton(0))
        {
            float x = 8f * Input.GetAxis("Mouse X");
            float y = 8f * Input.GetAxis("Mouse Y");

            if (transform.eulerAngles.z + y <= 0.1f || transform.eulerAngles.z + y >= 179.9f)
                y = 0;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + x, transform.eulerAngles.z + y);
            
            

        }**/



    }
}
