using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeNode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Orient(Vector3 center, float radius)
    {
        transform.LookAt(center);
        transform.Rotate(new Vector3(90, 0, 0));

        //Vector3.Normalize(transform.position);
        //transform.position *= radius;

        //Debug.Log(Vector3.Distance(transform.position, center));
    }
}
