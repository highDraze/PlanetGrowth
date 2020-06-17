using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


public class Hexagon : MonoBehaviour
{

    public Biome BiomeModel { get; set; }
    public string modelName { get; set; } = "init";

    public int Score { get; set; }

    private bool changed = false;


    int temperature;
    int humidity;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInChildren<ParticleSystem>().Stop();
    }

    void Update()
    {
        if (changed)
        {
            updateBiomePrefab();
            changed = false;
        }
    }

    public void setStartBiome()
    {
        temperature = UnityEngine.Random.Range(-2, 2);
        humidity = UnityEngine.Random.Range(-2, 2);

        getNewBiome();
    }

    public void updateBiomePrefab()
    {
        if (getNewBiome() == null) return;
        //deleteBiomeModel();  
    }



    private Biome getNewBiome()
    {
        Planet planet = GameObject.Find("Planet").GetComponent<Planet>();
        Biome newBiome = planet.DetermineMatchingBiome(temperature, humidity);

        if (newBiome == null)
        {
            return null;
        }

        // deleteBiomeModel();


        Transform currentModel = transform.Find(modelName);
        if (currentModel != null)
        {
            GameObject obj = currentModel.gameObject;
            Destroy(obj);
            obj = null;
        }
        GameObject newBiomeModel = Instantiate(
            newBiome.BiomeModel,
            transform,
            false);

        modelName = newBiomeModel.name;


        //newBiomeModel.transform.parent = transform;
        //newBiomeModel.name = newBiome.modelName;

        // AddComponent<BoxCollider>();



        // if (BiomeModel != null) deleteBiomeModel();
        // BiomeModel = newBiome;

        return newBiome;



    }

    private void deleteBiomeModel()
    {
        //if (BiomeModel != null)
        // {
        Transform t = transform.Find(BiomeModel.modelName);
        Destroy(t.gameObject);

        //Debug.Log("t");
        //Debug.Log(t.gameObject);
        //}
    }

    private void changesRegistered()
    {
        changed = true;
    }

    void ApplyBiomeChanges(int biome)
    {

    }

    public int Temperatur => temperature;
    public int Humidity => humidity;




    public void SetTemperatur(int temp)
    {
        changeTemperatur(temp);
        changesRegistered();
    }
    public void changeTemperatur(int temp)
    {
        temperature = temp;
        temperature = Math.Min(temperature, 2);
        temperature = Math.Max(-2, temperature);
    }
    public void changeHumidity(int humid)
    {
        humidity = humid;
        humidity = Math.Min(humidity, 2);
        humidity = Math.Max(-2, humidity);
    }

    public void setHumidity(int humid)
    {
        changeHumidity(humid);
        changesRegistered();
    }
}
