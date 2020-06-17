using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {



    public string Name { get; set; }
    public int age { get; set; }
    public List<Transform> surfaceHexagons;
    public List<Biome> biomes = new List<Biome>();




    public GameObject hexPrefab;

    public GameObject desertPrefab;
    public GameObject articPrefab;
    public GameObject oceanPrefab;
    public GameObject wastePrefab;
    public GameObject forestPrefab;
    public GameObject meadowPrefab;
    public GameObject dschunglePrefab;
    public GameObject swampPrefab;

    public GameObject desertBiom;
    public GameObject articBiom;
    public GameObject oceanBiom;
    public GameObject wasteBiom;
    public GameObject forestBiom;
    public GameObject meadowBiom;
    public GameObject dschungleBiom;
    public GameObject swampBiom;





    public int gridWidth;
    public int gridHeigth;

    float hexWidth = 1.732f;
    float hexHeigth = 2.0f;
    public float gapHeight;
    public float gapWidth;

    Vector3 startPos = Vector3.zero;

    public int[] catastropheChances = new int[3];

    // Start is called before the first frame update
    void Start() {


        for (int i = 0; i < 8; i++) {
            biomes.Add(new Biome());
        }

        biomes[0].InitBiome(0, new int[] { -2, 1 }, new int[] { -2, -1 }, wasteBiom.name, wasteBiom, -20);
        biomes[1].InitBiome(1, new int[] { 1, 2 }, new int[] { -2, -1 }, desertBiom.name, desertBiom, -10);
        biomes[2].InitBiome(2, new int[] { -2, -1 }, new int[] { -1, 1 }, articBiom.name, articBiom, -5);
        biomes[3].InitBiome(3, new int[] { 0, 1 }, new int[] { 0, 1 }, meadowBiom.name, meadowBiom, 30);
        biomes[4].InitBiome(4, new int[] { -1, 0 }, new int[] { 0, 1 }, forestBiom.name, forestBiom, 40);
        biomes[5].InitBiome(5, new int[] { 1, 2 }, new int[] { 1, 2 }, swampBiom.name, swampBiom, 10);
        biomes[6].InitBiome(6, new int[] { 2, 2 }, new int[] { 1, 2 }, dschungleBiom.name, dschungleBiom, 50);
        biomes[7].InitBiome(7, new int[] { -2, 0 }, new int[] { 1, 2 }, oceanBiom.name, oceanBiom, 10);

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



    // Update is called once per frame
    void Update() {

    }

    public Biome DetermineMatchingBiome(int _temperature, int _humidity) {

        List<Biome> matchingBiomes = new List<Biome>();
        foreach (Biome biome in biomes) {
            if (biome.MatchBiome(_temperature, _humidity)) matchingBiomes.Add(biome);
        }

        if (matchingBiomes.Count == 0) {
            return null;
        } else {
            int index = UnityEngine.Random.Range(0, matchingBiomes.Count);
            return matchingBiomes[index];
        }


    }


    public int getHexagonIndex(Transform selectedHex) {
        for (int index = 0; index < surfaceHexagons.Count; index++) {
            if (selectedHex == surfaceHexagons[index]) return index;
        }
        return -1;
    }

    public void highlightBiome(int hexIndex, bool isHovered) {

        if (isHovered) {
            surfaceHexagons[hexIndex].GetComponent<Hexagon>().GetComponentInChildren<ParticleSystem>().Play();
        } else {
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

                hex.GetComponent<Hexagon>().setStartBiome();
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
        for (int i = 0; i < x; i++)
        {
            Hexagon hex = surfaceHexagons[UnityEngine.Random.Range(0, surfaceHexagons.Count)].GetComponent<Hexagon>();
            hex.SetTemperatur(hex.Temperatur - 1);
        }
    }
    public void raiseHumidOfXRandomHex(int x) {
        if (x > surfaceHexagons.Count) x = surfaceHexagons.Count;
        for (int i = 0; i < x; i++)
        {
            Hexagon hex = surfaceHexagons[UnityEngine.Random.Range(0, surfaceHexagons.Count)].GetComponent<Hexagon>();
            hex.setHumidity(hex.Humidity + 1);
        }
    }
    public void lowerHumidOfXRandomHex(int x) {
        if (x > surfaceHexagons.Count) x = surfaceHexagons.Count;
        for (int i = 0; i < x; i++)
        {
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
