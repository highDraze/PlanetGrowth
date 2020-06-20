using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ZylinderPlanet : MonoBehaviour {

    public string Name {
        get;
        set;
    }
    public int age {
        get;
        set;
    }

    public List<Transform> surfaceHexagons;
    public GameObject hexPrefab;

    public int gridHeigth;
    public int gridWidth;
    float hexWidth = 1.732f;
    float hexHeigth = 2.0f;
    public float gapHeight;
    public float gapWidth;

    Vector3 startPos = Vector3.zero;

    public int[] catastropheChances = new int[3];

    // Start is called before the first frame update
    void Start() {

        AddGap();
        CreateGrid();
    }

    public int getLivabilityScore() {

        int value = 0;
        foreach (var hexagon in surfaceHexagons) {
            value += hexagon.GetComponent<Hexagon>().Score;
        }
        return value;
    }

    public int getHexagonIndex(Transform selectedHex)
    {
        for (int index = 0; index < surfaceHexagons.Count; index++) {
            if (selectedHex == surfaceHexagons[index]) return index;
        }
        return -1;
    }

    public void highlightBiome(int hexIndex, bool isHovered) {

        if (isHovered) {
            surfaceHexagons[hexIndex].GetComponent<Hexagon>().GetComponentInChildren<ParticleSystem>().Play();
        }
        else {
            surfaceHexagons[hexIndex].GetComponent<Hexagon>().GetComponentInChildren<ParticleSystem>().Stop();
        }
    }

    void AddGap() {
        hexHeigth += hexHeigth * gapHeight;
        hexWidth += hexWidth * gapWidth;
    }

    void CreateGrid() {
        for (int i = 0; i < gridHeigth; i++) {
            for (int j = 0; j < gridWidth; j++) {

                Transform hex = Instantiate(hexPrefab).transform;
                Vector2 gridPos = new Vector2(i, j);

                hex.position = CalcWorldPos(gridPos);
                hex.rotation = CalcRotation(gridPos);
                hex.name = "Hexagon" + i + "|" + j;
                //hex.GetComponent<Hexagon>().setStartBiome();
                hex.parent = this.transform;
                surfaceHexagons.Add(hex);
            }
        }
    }

    Vector3 CalcWorldPos(Vector2 gridPos) {

        float radius = hexWidth * 2;
        float offset = 0;
        if (gridPos.y % 2 != 0) {
            offset = 0.5f;
        }
        float ang = (360 / gridHeigth) * (gridPos.x + offset);
        Vector3 pos;
        pos.z = startPos.z + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = startPos.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.x = startPos.x + (gridPos.y) * (hexHeigth) * 0.75f;
        return pos;
    }

    private Quaternion CalcRotation(Vector2 gridPos) {
        float offset = 0;
        if (gridPos.y % 2 != 0) {
            offset = 0.5f;
        }
        return Quaternion.Euler((gridPos.x + offset) * 360 / gridHeigth + 90, 0, 0);
    }

    public void raiseTempOfXRandomHex(int x) {
        if (x > surfaceHexagons.Count) x = surfaceHexagons.Count;
        for (int i = 0; i < x; i++) {
            Hexagon hex = surfaceHexagons[UnityEngine.Random.Range(0, surfaceHexagons.Count)].GetComponent<Hexagon>();
            hex.SetTemperatur(hex.Temperatur + 1);
        }
    }

    public void lowerTempOfXRandomHex(int x) {
        if (x > surfaceHexagons.Count) x = surfaceHexagons.Count;
        for (int i = 0; i < x; i++) {
            Hexagon hex = surfaceHexagons[UnityEngine.Random.Range(0, surfaceHexagons.Count)].GetComponent<Hexagon>();
            hex.SetTemperatur(hex.Temperatur - 1);
        }
    }

    public void raiseHumidOfXRandomHex(int x) {
        if (x > surfaceHexagons.Count) x = surfaceHexagons.Count;
        for (int i = 0; i < x; i++) {
            Hexagon hex = surfaceHexagons[UnityEngine.Random.Range(0, surfaceHexagons.Count)].GetComponent<Hexagon>();
            hex.setHumidity(hex.Humidity + 1);
        }
    }

    public void lowerHumidOfXRandomHex(int x) {
        if (x > surfaceHexagons.Count) x = surfaceHexagons.Count;
        for (int i = 0; i < x; i++) {
            Hexagon hex = surfaceHexagons[UnityEngine.Random.Range(0, surfaceHexagons.Count)].GetComponent<Hexagon>();
            hex.setHumidity(hex.Humidity - 1);
        }
    }

    public void increaseCatastrophe(int CatNumber, int increase) {
        if (CatNumber == catastropheChances[0]) {
            catastropheChances[0] = increase;
        }
        if (CatNumber == catastropheChances[1]) {
            catastropheChances[1] = increase;
        }
        if (CatNumber == catastropheChances[2]) {
            catastropheChances[2] = increase;
        }
    }
}
