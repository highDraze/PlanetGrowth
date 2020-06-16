using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Contains information about the biome
/// </summary>
public class Biome {

    /// <summary>
    /// <see cref="id"/> has no purpose other than refer to it in the list that keeps biome instances
    /// </summary>
    private int id;

    private int liveAbilityScore;
    private int[] temperatureRange = new int[2];
    private int[] humidRange = new int[2];
    public String biome_name;
    private GameObject biomeModell;
    private GameObject biomePrefab;

    public int LiveAbilityScore => liveAbilityScore;

    // Start is called before the first frame update
    void Start() {
         
         

    }

    public void InitBiome(int _id, int[] _tempetemperatureRanges,
        int[] _humidRanges, String _name, GameObject _biomeModell, int _score, GameObject _biomePrefab) {
        id = _id;
        temperatureRange = _tempetemperatureRanges;
        humidRange = _humidRanges;
        biome_name = _name;
        biomeModell = _biomeModell;
        biomePrefab = _biomePrefab;
        liveAbilityScore = _score;
    }

    public bool MatchBiome(int _temperature, int _humidity) {
        bool matchFound = false;
        if (_temperature >= temperatureRange[0] && _temperature <= temperatureRange[1]) {
            if (_humidity >= humidRange[0] && _humidity <= humidRange[1]) {
                matchFound = true;
            }
        }


        return matchFound;
    }


    // Update is called once per frame
    void Update() {

    }

    public GameObject getModell() {
        return biomeModell;
    }
    public GameObject getPrefab() {
        return biomePrefab;
    }

}
