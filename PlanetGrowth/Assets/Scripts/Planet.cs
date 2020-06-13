using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public List<Transform> surfaceHexagons;
    public float temperature;

    public GameObject hexPrefab;
    public int gridWidth = 12;
    public int gridHeigth = 12;

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
                Transform hex = Instantiate(hexPrefab).transform;
                Vector2 gridPos = new Vector2(i, j);
                hex.position = CalcWorldPos(gridPos);
                hex.rotation = CalcRotation(gridPos);
                hex.parent = this.transform;
                hex.name = "Hexagon" + i + "|" + j;
                surfaceHexagons.Add(hex);
            }

        }

    }
    Vector3 CalcWorldPos(Vector2 gridPos)
    {

        float radius = 3.5f;
        float offset = 0;
        if (gridPos.y % 2 != 0)
        {
            offset = 0.5f;
        }
        float ang = (360 / gridHeigth) * (gridPos.x + offset);
        Vector3 pos;
        pos.x = startPos.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.z = startPos.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);




        pos.y = startPos.y + gridPos.y * hexHeigth * 0.75f;
        return pos;
    }
    private Quaternion CalcRotation(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
        {
            offset = 0.5f;
        }
        return Quaternion.Euler(0, (gridPos.x + offset) * 360 / gridHeigth, 0);
    }
}
