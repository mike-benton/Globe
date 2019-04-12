using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icosahedron : MonoBehaviour
{

    readonly float t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;
    List<Vector3> vertexList;
    List<Vector3Int> triIndexList;
    // Start is called before the first frame update
    void Start()
    {
        InitPoints();
        DisplayPoints();
        //DisplayTris();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitPoints()
    {
        vertexList = new List<Vector3>
        {
            new Vector3(-1, t, 0),
            new Vector3(1, t, 0),
            new Vector3(-1, -t, 0),
            new Vector3(1, -t, 0),

            new Vector3(0, -1, t),
            new Vector3(0, 1, t),
            new Vector3(0, -1, -t),
            new Vector3(0, 1, -t),

            new Vector3(t, 0, -1),
            new Vector3(t, 0, 1),
            new Vector3(-t, 0, -1),
            new Vector3(-t, 0, 1)
        };

        triIndexList = new List<Vector3Int>
        {
            new Vector3Int(0, 11, 5),
            new Vector3Int(0, 5, 1),
            new Vector3Int(0, 1, 7),
            new Vector3Int(0, 7, 10),
            new Vector3Int(0, 10, 11),

            new Vector3Int(1, 5, 9),
            new Vector3Int(5, 11, 4),
            new Vector3Int(11, 10, 2),
            new Vector3Int(10, 7, 6),
            new Vector3Int(7, 1, 8),

            new Vector3Int(3, 9, 4),
            new Vector3Int(3, 4, 2),
            new Vector3Int(3, 2, 6),
            new Vector3Int(3, 6, 8),
            new Vector3Int(3, 8, 9),

            new Vector3Int(4, 9, 5),
            new Vector3Int(2, 4, 11),
            new Vector3Int(6, 2, 10),
            new Vector3Int(8, 6, 7),
            new Vector3Int(9, 8, 1)


        };
    }

    void DisplayPoints()
    {
        GameObject cube;

        for (int i = 0; i < vertexList.Count; i++)
        {
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(.1f, .1f, .1f);
            cube.transform.position = vertexList[i];
        }
    }

    void DisplayTris()
    {
        //Gizmos.color = Color.blue;
        
        for (int i = 0; i < triIndexList.Count; i++)
        {
            //Debug.DrawLine(vertexList[triIndexList[i].x], vertexList[triIndexList[i].y], Color.blue, 100);
            Debug.DrawLine(vertexList[triIndexList[i].y], vertexList[triIndexList[i].z], Color.red, 100);
            Debug.DrawLine(vertexList[triIndexList[i].z], vertexList[triIndexList[i].x], Color.green, 100);
        }

    }

    void Tessellate()
    {


    }
}
