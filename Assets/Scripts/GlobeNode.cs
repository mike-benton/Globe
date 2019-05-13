using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeNode : MonoBehaviour
{
    bool hasTessellated;
    public string text;
    public Globe globe;
    public GlobeNode nodePrefab;
    public GlobeNode[] neighbors;
    public List<GlobeNode> newNeighbors = new List<GlobeNode>();
    public List<GlobeNode> newNeighbors2 = new List<GlobeNode>();


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
        for (int i = 0; i < newNeighbors.Count; i++)
        {
            /**
            if (i == 0) { newNeighbors[i].transform.GetComponent<MeshRenderer>().material.color = Color.red; }
            if (i == 1) { newNeighbors[1].transform.GetComponent<MeshRenderer>().material.color = Color.yellow; }
            if (i == 2) { newNeighbors[2].transform.GetComponent<MeshRenderer>().material.color = Color.green; }
            if (i == 3) { newNeighbors[3].transform.GetComponent<MeshRenderer>().material.color = Color.blue; }
            if (i == 4) { newNeighbors[4].transform.GetComponent<MeshRenderer>().material.color = Color.magenta; }**/
            newNeighbors[i].gameObject.GetComponentInChildren<TextMesh>().text = "" + i;

            /**
            if (i == 0) { newNeighbors[i].transform.GetComponent<MeshRenderer>().material.color = Color.red; }
            if (i == 1) { newNeighbors[1].transform.GetComponent<MeshRenderer>().material.color = Color.yellow; }
            if (i == 2) { newNeighbors[2].transform.GetComponent<MeshRenderer>().material.color = Color.green; }
            if (i == 3) { newNeighbors[3].transform.GetComponent<MeshRenderer>().material.color = Color.blue; }
            if (i == 4) { newNeighbors[4].transform.GetComponent<MeshRenderer>().material.color = Color.magenta; }**/
        }

        //foreach (GlobeNode node in newNeighbors)
        //{
        //    node.transform.GetComponent<MeshRenderer>().material.color = Color.cyan;
        //}
    }

    private void OnMouseExit()
    {
        gameObject.GetComponentInChildren<TextMesh>().text = "";
        foreach (GlobeNode node in newNeighbors)
        {
            //node.transform.GetComponent<MeshRenderer>().material.color = Color.white;
            node.gameObject.GetComponentInChildren<TextMesh>().text = "";
        }
    }

    public void Tessellate() //Only called by neighbors if hasTessellated = false
    {
        hasTessellated = true;

        for (int i = 0; i < neighbors.Length; i++)
        {
            if (!neighbors[i].hasTessellated) {
                

                GlobeNode newNode = Instantiate(nodePrefab, GetComponentInParent<Globe>().transform);
                //newNode.neighbors = new GlobeNode[6];
                newNode.transform.position = (transform.position + neighbors[i].transform.position) / 2;
                newNode.Orient(new Vector3(), GetComponentInParent<Globe>().radius, 1);
                newNode.text = "";

                newNeighbors.Add(newNode);
                neighbors[i].newNeighbors2.Add(newNode);


                //newNode.newNeighbors.Add(this);
                //newNode.newNeighbors.Add(newNeighbors[i]);
            }
        }

        for (int i = 0; i < neighbors.Length; i++)
        {
            if (!neighbors[i].hasTessellated)
            {
                neighbors[i].Tessellate();
            }
        }

        OrganizeNeighbors();
    }


    public void Orient(Vector3 center, float radius, int generation)
    {
        transform.position = transform.position.normalized * radius * .98f;
        transform.LookAt(center);
        transform.Rotate(new Vector3(90, 0, 0));
    }

    void OrganizeNeighbors()
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

        /**
        for (int i = 0; i < newNeighbors2.Count - 1; i++)
        {
            newNeighbors.Add(newNeighbors2[i + 1]);
        }
        if (newNeighbors2.Count > 0)
        {
            newNeighbors.Add(newNeighbors2[0]);
        }**/

        //newNeighbors.AddRange(newNeighbors2);
    }

    void AssignNeighborsNeighbors()
    {
        
    }
}
