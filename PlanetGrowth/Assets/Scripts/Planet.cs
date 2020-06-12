using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public List<Transform> surfaceHexagons;
    public float temperature;

    public Transform hexPrefab;
    public int gridWidth = 11;
    public int gridHeigth = 11;

    float hexWidth = 1.732f;
    float hexHeigth = 2.0f;
    public float gap = 0.0f;

    Vector3 startPos = Vector3.zero;

 
    // Start is called before the first frame update
    void Start()
    {
        AddGap();
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void AddGap()
    {
        hexHeigth += hexHeigth * gap;
        hexWidth += hexWidth * gap;
    }



    void CreateGrid()
    {
        for (int i = 0; i < gridHeigth; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                Transform hex = Instantiate(hexPrefab) as Transform;
                Vector2 gridPos = new Vector2(i, j);
                hex.position = CalcWorldPos(gridPos);
                hex.parent = this.transform;
                hex.name = "Hexagon" + i + "|" + j;
                surfaceHexagons.Add(hex);
            }

        }

    }

    Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
        {
            offset = hexWidth / 2;
        }
        float x = startPos.x + gridPos.x * hexWidth + offset;
        float z = startPos.z - gridPos.y * hexHeigth * 0.75f;
        return new Vector3(x, 0, z);
    }

}
