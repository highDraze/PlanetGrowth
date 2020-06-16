using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


public class Hexagon : MonoBehaviour
{

    public Biome BiomeModel { get; set; }

    public int Score { get; set; }

    int temperature;
    int humidity;

    // Start is called before the first frame update
    void Start()
    {

        gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        // transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Stop(); 
    }

    void Update()
    {

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
        deleteBiomeModel();  
    }



    private Biome getNewBiome()
    {
        Planet planet = GameObject.Find("Planet").GetComponent<Planet>();
        Biome newBiome = planet.DetermineMatchingBiome(temperature, humidity);

        if (newBiome == null)
        {
            return null;
        }
        else
        {
           // UnityEngine.Quaternion cRotation = transform.rotation * newBiome.BiomeModel.transform.rotation ;
            GameObject newBiomeModel = Instantiate(
                newBiome.BiomeModel,
                transform.position,
                transform.rotation * newBiome.BiomeModel.transform.rotation);
            newBiomeModel.transform.parent = transform;
            BiomeModel = newBiome;
            return newBiome;
        }
        

    }

    private void deleteBiomeModel()
    {
        if (BiomeModel != null)
        {
            Destroy(transform.Find(BiomeModel.modelName).gameObject);
        }
    }



    void ApplyBiomeChanges(int biome)
    {

    }

    public int Temperatur => temperature;
    public int Humidity => humidity;




    public void SetTemperatur(int temp)
    {
        temperature = temp;
        updateBiomePrefab();
    }
    public void changeTemperatur(int temp)
    {

        temperature = Math.Min((temperature + temp), 2);
        temperature = Math.Max(-2, temperature);

    }
    public void changeHumidity(int humid)
    {

        humidity = Math.Min((humidity + humid), 2);
        humidity = Math.Max(-2, humidity);

    }

    public void setHumidity(int humid)
    {
        humidity = humid;
        updateBiomePrefab();
    }
}
