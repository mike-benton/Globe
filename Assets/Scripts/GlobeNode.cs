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

    public void Orient(Vector3 center, float radius, int generation)
    {
        transform.LookAt(center);
        transform.Rotate(new Vector3(90, 0, 0));

        if (generation < 12)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else if (generation < 51)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }

        Debug.Log(generation);

        //Vector3.Normalize(transform.position);
        //transform.position *= radius;

        //Debug.Log(Vector3.Distance(transform.position, center));
    }
}
