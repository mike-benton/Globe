using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Globe globe;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(globe);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
