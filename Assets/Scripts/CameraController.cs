using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float cameraRadius;
    GameObject globeCamera;

    // Start is called before the first frame update
    void Start()
    {
        globeCamera = gameObject.transform.GetChild(0).gameObject;
        cameraRadius = Vector3.Distance(transform.position, globeCamera.transform.position);
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

            globeCamera.transform.position = globeCamera.transform.position.normalized * cameraRadius;
            
        }
        
        if (Input.GetMouseButton(0))
        {
            float x = 8f * Input.GetAxis("Mouse X");
            float y = 8f * Input.GetAxis("Mouse Y");

            if (transform.eulerAngles.z + y <= 0.1f || transform.eulerAngles.z + y >= 179.9f)
                y = 0;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x + y, transform.eulerAngles.y + x, transform.eulerAngles.z);

            globeCamera.transform.rotation = Quaternion.Euler(globeCamera.transform.rotation.x, globeCamera.transform.rotation.y, 0);
            //globeCamera.transform.LookAt(transform.position);
        }
    }
}
