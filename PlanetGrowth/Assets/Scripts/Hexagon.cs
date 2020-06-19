using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

using UnityEngine;


public class Hexagon : MonoBehaviour {

    public Biome BiomeModel {
        get;
        set;
    }
    public string modelName {
        get;
        set;
    } = "init";

    public int Score {
        get;
        set;
    }

    private bool changed = false;

    [SerializeField] private BiomeController m_biomeController;
    private GameObject m_biome = null;
    [SerializeField] private int m_temperature;
    [SerializeField] private int m_humidity;

    // Start is called before the first frame update
    void Start() {
        m_biome = m_biomeController.assignBiome(m_temperature, m_humidity, transform);
        gameObject.GetComponentInChildren<ParticleSystem>().Stop();
    }

    void Update() {
        updateBiomePrefab();
    }

    public void setStartBiome() {
        Temperature = UnityEngine.Random.Range(-2, 2);
        m_humidity = UnityEngine.Random.Range(-2, 2);
        updateBiomePrefab();
    }

    public void updateBiomePrefab() {
        if (changed) {
            m_biome = m_biomeController.assignBiome(
                Temperature,
                m_humidity,
                m_biome);
            changed = false;
        }
    }

    //private Biome addNewBiomeModel()
    //{
    //    Planet planet = GameObject.Find("Planet").GetComponent<Planet>();
    //    Biome newBiome = planet.DetermineMatchingBiome(temperature, humidity);

    //    if (newBiome == null)
    //    {
    //        return null;
    //    }
    //    GameObject newBiomeModel = Instantiate(
    //        newBiome.BiomeModel,
    //        transform,
    //        false);


    //    Biome oldBiome = BiomeModel;
    //    BiomeModel = newBiome;
    //    BiomeModel.m_modelName = newBiomeModel.name;
    //    return oldBiome;
    //}

    //private void deleteOldBiomeModel(Biome _biome)
    //{
    //    Transform currentModel = transform.Find(_biome.m_modelName);
    //    if (currentModel != null)
    //    {
    //        GameObject obj = currentModel.gameObject;
    //        Destroy(obj);
    //        obj = null;
    //    }
    //}

    private void changesRegistered() {
        changed = true;
    }

    public int Temperatur => Temperature;
    public int Humidity => m_humidity;

    public int Temperature {
        get => m_temperature;
        set => m_temperature = value;
    }

    public void SetTemperatur(int temp) {
        changeTemperatur(temp);
        changesRegistered();
    }

    public void changeTemperatur(int temp) {
        Temperature = temp;
        Temperature = Math.Min(Temperature, 2);
        Temperature = Math.Max(-2, Temperature);
    }

    public void changeHumidity(int humid) {
        m_humidity = humid;
        m_humidity = Math.Min(m_humidity, 2);
        m_humidity = Math.Max(-2, m_humidity);
    }

    public void setHumidity(int humid) {
        changeHumidity(humid);
        changesRegistered();
    }
}
