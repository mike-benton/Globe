using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globe : MonoBehaviour
{
    public readonly float t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;
    public readonly float radius = Mathf.Sqrt((5 + Mathf.Sqrt(5)) / 2);
    public GlobeNode nodePrefab;

    public List<GlobeNode> globeNodeList;
    //public Vector3[] vertexArr;

    // Start is called before the first frame update
    void Start()
    {
        InitPoints();
        AssignInitialNeighbors();
        Tessellate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitPoints() //Create the initial 
    {
        globeNodeList = new List<GlobeNode>();

        Vector3[] vertexArr = new Vector3[]
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

        for (int i = 0; i < vertexArr.Length; i++)
        {
            GlobeNode newNode = Instantiate(nodePrefab, transform);

            newNode.transform.position = vertexArr[i];
            newNode.Orient(transform.position, radius, 0);
            //newNode.transform.parent = transform;
            newNode.text = "Node " + i;
            globeNodeList.Add(newNode);
        }
    }

    void AssignInitialNeighbors()
    {
        globeNodeList[0].neighbors = new GlobeNode[] { globeNodeList[1], globeNodeList[7], globeNodeList[10], globeNodeList[11], globeNodeList[5] };
        globeNodeList[1].neighbors = new GlobeNode[] { globeNodeList[0], globeNodeList[5], globeNodeList[9], globeNodeList[8], globeNodeList[7] };
        globeNodeList[2].neighbors = new GlobeNode[] { globeNodeList[10], globeNodeList[6], globeNodeList[3], globeNodeList[4], globeNodeList[11] };
        globeNodeList[3].neighbors = new GlobeNode[] { globeNodeList[6], globeNodeList[8], globeNodeList[9], globeNodeList[4], globeNodeList[2] };
        globeNodeList[4].neighbors = new GlobeNode[] { globeNodeList[11], globeNodeList[2], globeNodeList[3], globeNodeList[9], globeNodeList[5] };
        globeNodeList[5].neighbors = new GlobeNode[] { globeNodeList[0], globeNodeList[11], globeNodeList[4], globeNodeList[9], globeNodeList[1] };
        globeNodeList[6].neighbors = new GlobeNode[] { globeNodeList[8], globeNodeList[3], globeNodeList[2], globeNodeList[10], globeNodeList[7] };
        globeNodeList[7].neighbors = new GlobeNode[] { globeNodeList[1], globeNodeList[8], globeNodeList[6], globeNodeList[10], globeNodeList[0] };
        globeNodeList[8].neighbors = new GlobeNode[] { globeNodeList[1], globeNodeList[9], globeNodeList[3], globeNodeList[6], globeNodeList[7] };
        globeNodeList[9].neighbors = new GlobeNode[] { globeNodeList[5], globeNodeList[4], globeNodeList[3], globeNodeList[8], globeNodeList[1] };
        globeNodeList[10].neighbors = new GlobeNode[] { globeNodeList[7], globeNodeList[6], globeNodeList[2], globeNodeList[11], globeNodeList[0] };
        globeNodeList[11].neighbors = new GlobeNode[] { globeNodeList[10], globeNodeList[2], globeNodeList[4], globeNodeList[5], globeNodeList[0] };
        
    }

    void Tessellate()
    {
        globeNodeList[0].Tessellate();
    }
}
