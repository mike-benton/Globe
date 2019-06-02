using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeNode : MonoBehaviour
{
    bool hasTessellated;
    public string text;
    public Globe globe;
    public GlobeNode nodePrefab;
    public List<GlobeNode> neighbors;
    public List<GlobeNode> newNeighbors = new List<GlobeNode>();
    public List<GlobeNode> newNeighbors2 = new List<GlobeNode>();
    public int listIndex;


    // Start is called before the first frame update
    void Start()
    {
        //globe = GetComponentInParent<Globe>();
        hasTessellated = false;
        //neighbors = new GlobeNode[6];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        gameObject.GetComponentInChildren<TextMesh>().text = text;
        for (int i = 0; i < neighbors.Count; i++)
        {
            neighbors[i].gameObject.GetComponentInChildren<TextMesh>().text = "" + i; //sets neighbors text to their neighbor index of this node
            //neighbors[i].gameObject.GetComponentInChildren<TextMesh>().text = "" + i;
        }
    }

    private void OnMouseExit()
    {
        gameObject.GetComponentInChildren<TextMesh>().text = "";
        foreach (GlobeNode node in neighbors)
        {
            node.gameObject.GetComponentInChildren<TextMesh>().text = ""; //clears text set by OnMouseOver
        }
    }

    public void Tessellate() //Only called by neighbors if hasTessellated = false
    {
        

        hasTessellated = true;

        for (int i = 0; i < neighbors.Count; i++)
        {
            if (!neighbors[i].hasTessellated) {


                GlobeNode newNode = Instantiate(nodePrefab, GetComponentInParent<Globe>().transform);
                
                newNode.transform.position = (transform.position + neighbors[i].transform.position) / 2;
                newNode.Orient(new Vector3(), GetComponentInParent<Globe>().radius, 1);
                newNode.text = "";
                newNode.listIndex = -1;

                newNode.newNeighbors = new List<GlobeNode>(6) //gives the baby node two neighbors it needs to succeed in life
                {
                    this, neighbors[i]
                };


                GetComponentInParent<Globe>().globeNodeList.Add(newNode);
                newNeighbors.Add(newNode);
                neighbors[i].newNeighbors2.Add(newNode);

                
            }
        }

        OrganizeNeighbors();

        AssignNeighborsNeighbors();


        



        for (int i = 0; i < neighbors.Count; i++)
        {
            if (!neighbors[i].hasTessellated)
            {
                neighbors[i].Tessellate();
            }
        }

    }


    public void Orient(Vector3 center, float radius, int generation)
    {
        transform.position = transform.position.normalized * radius * .98f;
        transform.LookAt(center);
        transform.Rotate(new Vector3(90, 0, 0));
    }

    void OrganizeNeighbors()
    {
        if (neighbors.Count == 5)
        {
            text += "(" + newNeighbors.Count + ", " + newNeighbors2.Count + ")";

            if (newNeighbors.Count == 3)
            {
                newNeighbors.Add(newNeighbors2[1]);
                newNeighbors.Add(newNeighbors2[0]);
            }
            else if (newNeighbors.Count == 2)
            {
                newNeighbors.Add(newNeighbors2[2]);
                newNeighbors.Add(newNeighbors2[0]);
                newNeighbors.Add(newNeighbors2[1]);
            }
            else if (newNeighbors.Count == 1)
            {
                newNeighbors.Add(newNeighbors2[3]);
                newNeighbors.Add(newNeighbors2[0]);
                newNeighbors.Add(newNeighbors2[1]);
                newNeighbors.Add(newNeighbors2[2]);
            }
            else
            {
                newNeighbors.AddRange(newNeighbors2);
            }
        }
        else
        {
            newNeighbors.AddRange(newNeighbors2);

        }
        
    }

    void AssignNeighborsNeighbors()
    {
        newNeighbors[0].newNeighbors.Add(newNeighbors[newNeighbors.Count-1]);
        newNeighbors[0].newNeighbors.Add(newNeighbors[1]);

        for (int i = 1; i < newNeighbors.Count - 1; i++)
        {
            newNeighbors[i].newNeighbors.Add(newNeighbors[i - 1]);
            newNeighbors[i].newNeighbors.Add(newNeighbors[i + 1]);
        }

        newNeighbors[newNeighbors.Count-1].newNeighbors.Add(newNeighbors[newNeighbors.Count-2]);
        newNeighbors[newNeighbors.Count-1].newNeighbors.Add(newNeighbors[0]);

    }
}
