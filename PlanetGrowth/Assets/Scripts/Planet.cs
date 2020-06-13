using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public List<Transform> surfaceHexagons;
  
    public Material biome1;
    public Material biome2;
    public Material biome3;
    public Material biome4;
    public Material biome5;



    public GameObject hexPrefab;
    public int gridWidth;
    public int gridHeigth;

    float hexWidth = 1.732f;
    float hexHeigth = 2.0f;
    public float gapHeight;
    public float gapWidth;

    Vector3 startPos = Vector3.zero;

    public int getHexagonIndex(Transform selectedHex)
    {
        for (int index = 0; index < surfaceHexagons.Count; index++)
        {
            if (selectedHex.parent.name == surfaceHexagons[index].name) return index;
        }
        return -1;
    }

    public void highlightBiome(int hexIndex, bool isHovered)
    {
        if (isHovered)
        {
            surfaceHexagons[hexIndex].GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Play();
        }
        else {
            surfaceHexagons[hexIndex].GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Stop();
        }
    }

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
        hexHeigth += hexHeigth * gapHeight;
        hexWidth += hexWidth * gapWidth;
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

        float radius = hexWidth * 2;
        float offset = 0;
        if (gridPos.y % 2 != 0)
        {
            offset = 0.5f;
        }
        float ang = (360 / gridHeigth) * (gridPos.x+offset);
        Vector3 pos;
        pos.z = startPos.z + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = startPos.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);




        pos.x = startPos.x + (gridPos.y )* (hexHeigth)*0.75f;
        return pos;
    }
    private Quaternion CalcRotation(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
        {
            offset = 0.5f;
        }
        return Quaternion.Euler((gridPos.x + offset) * 360 / gridHeigth+90, 0, 0);
    }

    public void raiseTempOfXRandomHex(int x) {
        for (int i = 0; i < x; i++) {
            Hexagon hex  = surfaceHexagons[UnityEngine.Random.Range(0, surfaceHexagons.Count)].GetComponent<Hexagon>();
            hex.setTemperatur(hex.getTemperatur() + 1);
        }
    }
    public void lowerTempOfXRandomHex(int x)
    {
        for (int i = 0; i < x; i++)
        {
            Hexagon hex = surfaceHexagons[UnityEngine.Random.Range(0, surfaceHexagons.Count)].GetComponent<Hexagon>();
            hex.setTemperatur(hex.getTemperatur() - 1);
        }
    }
    public void raiseHumidOfXRandomHex(int x)
    {
        for (int i = 0; i < x; i++)
        {
            Hexagon hex = surfaceHexagons[UnityEngine.Random.Range(0, surfaceHexagons.Count)].GetComponent<Hexagon>();
            hex.setHumidity(hex.getHumidity() + 1);
        }
    }
    public void lowerHumidOfXRandomHex(int x)
    {
        for (int i = 0; i < x; i++)
        {
            Hexagon hex = surfaceHexagons[UnityEngine.Random.Range(0, surfaceHexagons.Count)].GetComponent<Hexagon>();
            hex.setHumidity(hex.getHumidity() - 1);
        }
    }
}
