using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


public class Hexagon : MonoBehaviour
{
    public Color color;
    public GameObject model1;
    public GameObject model2;
    public GameObject model3;

    private GameObject currentModel;
    private int biome;
    public int score;

    int temperature;
    int humidity;

    // Start is called before the first frame update
    void Start()
    {
     
        gameObject.GetComponentInChildren<ParticleSystem>().Stop();
       // transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Stop(); 
    }

    void updateBiomePrefab()
    {
        Planet planet = GameObject.Find("Planet").GetComponent<Planet>();
        Biome newBiome = planet.DetermineMatchingBiome(temperature, humidity);

        if(newBiome != null) {
            score = newBiome.LiveAbilityScore;
            Debug.Log("IMPORTANT: " + transform.Find("Rota Parent").name);
            UnityEngine.Quaternion cRotation = transform.Find("Rota Parent").rotation;
            foreach (Transform obj in transform.GetChild(0))
            {
                Destroy(obj.gameObject);
            }
            Instantiate(newBiome.getModell(), transform.position, cRotation, transform.GetChild(0));
            
        }
    }

    void applyBiomeChanges(int biome){

    }

    public int getTemperatur() {
        return temperature;
    }
    public int getHumidity() {
        return humidity;
    }

    public void setTemperatur(int temp)
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
