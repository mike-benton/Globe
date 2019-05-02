using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeTile : MonoBehaviour
{
    public Globe parentGlobe;
    public Vector3[] vertices;
    public GlobeTile[] neighbors;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateTile(int sides)
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mf.mesh = mesh;
        neighbors = new GlobeTile[sides];

        //Verticies
        vertices = new Vector3[sides];

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = Quaternion.Euler(0, 0, 360 / vertices.Length * -i) * Vector3.up;
        }
        if (sides == 5)
        {
            float scaleMult = (1 / Vector3.Distance(vertices[0], vertices[1]));
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = Quaternion.Euler(0, 0, 360 / vertices.Length * -i) * Vector3.up * scaleMult;
            }
        }

        Debug.Log(Vector3.Distance(vertices[0], vertices[1]));

        //Triangles
        int[] tri;

        if (sides == 6)
        {
            tri = new int[12];
            tri[9] = 0;
            tri[10] = 4;
            tri[11] = 5;
        }
        else
        {
            tri = new int[9];
        }
        

        tri[0] = 0;
        tri[1] = 3;
        tri[2] = 4;

        tri[3] = 0;
        tri[4] = 2;
        tri[5] = 3;

        tri[6] = 0;
        tri[7] = 1;
        tri[8] = 2;

        

        //Normals
        Vector3[] normals = new Vector3[sides];

        normals[0] = -Vector3.forward;
        normals[1] = -Vector3.forward;
        normals[2] = -Vector3.forward;
        normals[3] = -Vector3.forward;
        normals[4] = -Vector3.forward;
        if (sides == 6)
        {
            normals[5] = -Vector3.forward;
        }

        //UVs
        Vector2[] uv = new Vector2[sides];

        uv[0] = new Vector2(.5f, 1);
        uv[1] = new Vector2(1, .625f);
        uv[2] = new Vector2(.8125f, 0);
        uv[3] = new Vector2(.1875f, 0);
        uv[4] = new Vector2(0, .625f);
        if (uv.Length == 6)
        {
            uv[5] = new Vector2(0, .625f);
        }

        //Assign Arrays
        mesh.vertices = vertices;
        mesh.triangles = tri;
        mesh.normals = normals;
        mesh.uv = uv;
        //GetComponent<Renderer>().material.
        //    = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
