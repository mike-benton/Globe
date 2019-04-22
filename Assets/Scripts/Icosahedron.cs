using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icosahedron : MonoBehaviour
{

    readonly float t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;
    readonly float radius = Mathf.Sqrt((5 + Mathf.Sqrt(5)) / 2);
    public GlobeNode nodePrefab;
    List<GlobeNode> nodeList;
    List<Vector3> vertexList;
    List<Vector3Int> triIndexList;
    // Start is called before the first frame update
    void Start()
    {
        InitPoints();
        Tessellate();
        DisplayPoints();
        //DisplayTris();
        //Debug.Log(radius);

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

        //GameObject cube;

        //for (int i = 0; i < vertexList.Count; i++)
        //{
        //    cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    cube.transform.localScale = new Vector3(.1f, .1f, .1f);
        //    cube.transform.position = vertexList[i];
        //}

        nodeList = new List<GlobeNode>();

        for (int i = 0; i < vertexList.Count; i++)
        {
            nodeList.Add(Instantiate(nodePrefab));
            nodeList[i].transform.position = vertexList[i];
            nodeList[i].Orient(transform.position, radius, i);
        }
    }

    void DisplayTris()
    {
        //Gizmos.color = Color.blue;
        
        for (int i = 0; i < triIndexList.Count; i++)
        {
            //Debug.DrawLine(vertexList[triIndexList[i].x], vertexList[triIndexList[i].y], Color.blue, 100);
            //Debug.DrawLine(vertexList[triIndexList[i].y], vertexList[triIndexList[i].z], Color.red, 100);
            Debug.DrawLine(vertexList[triIndexList[i].z], vertexList[triIndexList[i].x], Color.green, 100);
        }

    }

    void Tessellate()
    {
        List<Vector3Int> newTriIndexList = new List<Vector3Int>();
        List<Vector3> newVertexList = new List<Vector3>();

        Vector3 midpointXY;
        Vector3 midpointYZ;
        Vector3 midpointZX;

        int indexXY;
        int indexYZ;
        int indexZX;

        for (int i = 0; i < triIndexList.Count; i++)
        {
            midpointXY = (((vertexList[triIndexList[i].x] + vertexList[triIndexList[i].y]) / 2).normalized) * radius;
            midpointYZ = (((vertexList[triIndexList[i].y] + vertexList[triIndexList[i].z]) / 2).normalized) * radius;
            midpointZX = (((vertexList[triIndexList[i].z] + vertexList[triIndexList[i].x]) / 2).normalized) * radius;

            indexXY = -1;
            indexYZ = -1;
            indexZX = -1;

            for (int j = 0; j < newVertexList.Count; j++)
            {
                if (midpointXY == newVertexList[j])
                {
                    indexXY = j;

                    newVertexList.Add(midpointYZ);
                    indexYZ = newVertexList.Count;

                    newVertexList.Add(midpointZX);
                    indexZX = newVertexList.Count;

                    Debug.Log("Duplicate Found at " + midpointXY);
                    break;
                }
                else if (midpointYZ == newVertexList[j])
                {
                    newVertexList.Add(midpointXY);
                    indexXY = newVertexList.Count;

                    indexYZ = j;

                    newVertexList.Add(midpointZX);
                    indexZX = newVertexList.Count;

                    Debug.Log("Duplicate Found at " + midpointYZ);
                    break;
                }
                else if (midpointZX == newVertexList[j])
                {
                    newVertexList.Add(midpointXY);
                    indexXY = newVertexList.Count;

                    newVertexList.Add(midpointYZ);
                    indexYZ = newVertexList.Count;

                    indexZX = j;

                    Debug.Log("Duplicate Found at " + midpointZX);
                    break;
                }
            }

            if (indexXY == -1)
            {
                newVertexList.Add(midpointXY);
                indexXY = newVertexList.Count;

                newVertexList.Add(midpointYZ);
                indexYZ = newVertexList.Count;

                newVertexList.Add(midpointZX);
                indexZX = newVertexList.Count;
            }

            //vertexList.Add(midpointXY); //Count - 2
            //vertexList.Add(midpointYZ); //Count - 1
            //vertexList.Add(midpointZX); //Count

            newTriIndexList.Add(new Vector3Int(triIndexList[i].x, indexXY, indexZX));
            newTriIndexList.Add(new Vector3Int(indexXY, triIndexList[i].y, indexYZ));
            newTriIndexList.Add(new Vector3Int(indexZX, indexYZ, triIndexList[i].z));
            newTriIndexList.Add(new Vector3Int(indexXY, indexYZ, indexZX));
        }

        vertexList.AddRange(newVertexList);
        triIndexList = newTriIndexList;

    }
}
