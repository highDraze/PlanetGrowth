using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome
{
    private int id;
    
    private int[] temperatureRange = new int[2];
    private int[] humidRange = new int[2];
    private String biome_name;
    private GameObject biomePrefab;



    // Start is called before the first frame update
    void Start()
    {
            
    }

    public void InitBiome(int _id, int[] _tempetemperatureRanges, 
        int[] _humidRanges, String _name, GameObject _biomePrefab) 
    {
        id = _id;
        temperatureRange = _tempetemperatureRanges;
        humidRange = _humidRanges;
        biome_name = _name;
        biomePrefab = _biomePrefab;
    }
    
    public bool MatchBiome(int _temperature, int _humidity) {
        bool matchFound = false;
        if(  _temperature >= temperatureRange[0] && _temperature <= temperatureRange[1]) {
            if (_humidity >= humidRange[0] && _humidity <= humidRange[1]) {
                matchFound = true;
            }
        }


        return matchFound;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getPrefab() {
        return biomePrefab;
    }

}
