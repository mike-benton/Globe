using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globe : MonoBehaviour
{
    public readonly float t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;
    public readonly float radius = Mathf.Sqrt((5 + Mathf.Sqrt(5)) / 2);
    public GlobeNode nodePrefab;
    public int numTessellations = 0;

    public List<GlobeNode> globeNodeList;
    public List<GlobeNode> newNodeList;

    //public Vector3[] vertexArr;

    // Start is called before the first frame update
    void Start()
    {
        InitPoints();
        AssignInitialNeighbors();
        Tessellate(numTessellations);
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
            newNode.transform.localScale /= (Mathf.Pow(numTessellations, 2) + 1);
            newNode.Orient(transform.position, radius, 0);
            newNode.listIndex = i;
            globeNodeList.Add(newNode);
        }
    }

    void AssignInitialNeighbors()
    {
        globeNodeList[0].neighbors = new List<GlobeNode>(5) { globeNodeList[1], globeNodeList[7], globeNodeList[10], globeNodeList[11], globeNodeList[5] };
        globeNodeList[1].neighbors = new List<GlobeNode>(5) { globeNodeList[0], globeNodeList[5], globeNodeList[9], globeNodeList[8], globeNodeList[7] };
        globeNodeList[2].neighbors = new List<GlobeNode>(5) { globeNodeList[10], globeNodeList[6], globeNodeList[3], globeNodeList[4], globeNodeList[11] };
        globeNodeList[3].neighbors = new List<GlobeNode>(5) { globeNodeList[6], globeNodeList[8], globeNodeList[9], globeNodeList[4], globeNodeList[2] };
        globeNodeList[4].neighbors = new List<GlobeNode>(5) { globeNodeList[11], globeNodeList[2], globeNodeList[3], globeNodeList[9], globeNodeList[5] };
        globeNodeList[5].neighbors = new List<GlobeNode>(5) { globeNodeList[0], globeNodeList[11], globeNodeList[4], globeNodeList[9], globeNodeList[1] };
        globeNodeList[6].neighbors = new List<GlobeNode>(5) { globeNodeList[8], globeNodeList[3], globeNodeList[2], globeNodeList[10], globeNodeList[7] };
        globeNodeList[7].neighbors = new List<GlobeNode>(5) { globeNodeList[1], globeNodeList[8], globeNodeList[6], globeNodeList[10], globeNodeList[0] };
        globeNodeList[8].neighbors = new List<GlobeNode>(5) { globeNodeList[1], globeNodeList[9], globeNodeList[3], globeNodeList[6], globeNodeList[7] };
        globeNodeList[9].neighbors = new List<GlobeNode>(5) { globeNodeList[5], globeNodeList[4], globeNodeList[3], globeNodeList[8], globeNodeList[1] };
        globeNodeList[10].neighbors = new List<GlobeNode>(5) { globeNodeList[7], globeNodeList[6], globeNodeList[2], globeNodeList[11], globeNodeList[0] };
        globeNodeList[11].neighbors = new List<GlobeNode>(5) { globeNodeList[10], globeNodeList[2], globeNodeList[4], globeNodeList[5], globeNodeList[0] };
    }

    void Tessellate(int times)
    {
        for (int i = 0; i < times; i++)
        {
            globeNodeList[0].Tessellate();
            CleanUp();
        }

    }

    void CleanUp()
    {
        
        for (int i = 0; i < globeNodeList.Count; i++)
        {
            if (globeNodeList[i].listIndex == -1)
            {
                globeNodeList[i].listIndex = i;

                globeNodeList[i].newNeighbors = new List<GlobeNode>(6)
                {
                    globeNodeList[i].newNeighbors[0],
                    globeNodeList[i].newNeighbors[2],
                    globeNodeList[i].newNeighbors[5],
                    globeNodeList[i].newNeighbors[1],
                    globeNodeList[i].newNeighbors[4],
                    globeNodeList[i].newNeighbors[3]
                };
            }

            globeNodeList[i].hasTessellated = false;
            globeNodeList[i].name = "Node " + i;
            globeNodeList[i].neighbors = globeNodeList[i].newNeighbors;
            globeNodeList[i].newNeighbors = new List<GlobeNode>();
            globeNodeList[i].newNeighbors2 = new List<GlobeNode>();
        }
    }
}
