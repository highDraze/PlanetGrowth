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

    public string modelName { get; set; }
    private int liveAbilityScore;
    private int[] temperatureRange = new int[2];
    private int[] humidRange = new int[2];


    public int LiveAbilityScore => liveAbilityScore;

    public GameObject BiomeModel { get; set; }



    // Start is called before the first frame update
    void Start() {



    }

    public void InitBiome(int _id, int[] _tempetemperatureRanges,
        int[] _humidRanges, String _name, GameObject _biomeModell, int _score) {
        id = _id;
        temperatureRange = _tempetemperatureRanges;
        humidRange = _humidRanges;
        modelName = _name;
        BiomeModel = _biomeModell;
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


}
