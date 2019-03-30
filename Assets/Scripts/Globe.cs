using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globe : MonoBehaviour
{
    public List<GlobeTile> globeTileList;
    public Vector3[] vertices;
    public GlobeTile tilePrefab;
    public 

    // Start is called before the first frame update
    void Start()
    {
        
        globeTileList = new List<GlobeTile>();
        
        CreateGlobe(1);
        DisplayGlobeTiles();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateGlobe(int size)
    {
        
        globeTileList.Add(Instantiate(tilePrefab, new Vector3(0, 0), Quaternion.identity));
        globeTileList[0].CreateTile(5);

        globeTileList.Add(Instantiate(tilePrefab, new Vector3(1, 1), Quaternion.identity));
        globeTileList[1].CreateTile(6);
        
    }

    void DisplayGlobeTiles()
    {
        foreach (GlobeTile tile in globeTileList)
        {

        }
    }
    
}
