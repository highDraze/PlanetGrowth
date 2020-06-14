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
            foreach (Transform obj in transform.GetChild(0))
                Destroy(obj.gameObject);
            Instantiate(newBiome.getPrefab(), transform.position, transform.rotation, transform.GetChild(0));
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

    public void setHumidity(int humid)
    {
        humidity = humid;
        updateBiomePrefab();
    }
}
