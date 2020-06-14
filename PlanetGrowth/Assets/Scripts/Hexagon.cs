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

    int temperature;
    int humidity;

    // Start is called before the first frame update
    void Start()
    {
       // transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Stop(); 
    }

    void updateBiomePrefab()
    {
        Debug.Log("test");
        Planet planet = GameObject.Find("Planet").GetComponent<Planet>();
        Biome newBiome = planet.DetermineMatchingBiome(temperature, humidity);

        if(newBiome != null) {

            Destroy(transform.GetChild(0).GetChild(0).gameObject);

            var hex_clone = Instantiate(newBiome.getPrefab(), transform.position, transform.rotation, transform.GetChild(0));
            

           // hex_clone.name = transform.name;
           // hex_clone.transform.parent = GameObject.Find("Planet").transform;
           // int index = planet.getHexagonIndex(transform);
           // if(index != -1) {
           //     planet.surfaceHexagons[index] = hex_clone.transform;
           // } else {
           //     Debug.Log(transform.name);
           // }
            
            //Destroy(this.gameObject);
            
            // succes - assign new prefab
        } else {
            // Fail - no match was found
        }
/*
        if (temperature == -2){
            if(humidity == -2){

            }
            else if(humidity == -1){

            }
            else if (humidity == 0){

            }
            else if (humidity == 1){

            }
            else if (humidity == 2){

            }
        }
        else if (temperature == -1){
            if (humidity == -2){

            }
            else if (humidity == -1){

            }
            else if (humidity == -1){

            }
            else if (humidity == 0){

            }
            else if (humidity == 1){

            }
            else if (humidity == 2){

            }
        }
        else if (temperature == 0){
            if (humidity == -2){

            }
            else if (humidity == -1){

            }
            else if (humidity == 0){

            }
            else if (humidity == 1){

            }
            else if (humidity == 2){

            }
        }
        else if (temperature == 1){
            if (humidity == -2){

            }
            else if (humidity == -1){

            }
            else if (humidity == 0){

            }
            else if (humidity == 1){

            }
            else if (humidity == 2){

            }
        }
        else if (temperature == 2){
            if (humidity == -2){

            }
            else if (humidity == -1){

            }
            else if (humidity == 0){

            }
            else if (humidity == 1){

            }
            else if (humidity == 2){

            }
        }



        if (temperature == 2 && humidity == 2)
        {
            GetComponentInChildren<MeshRenderer>().material = GameObject.Find("Planet").GetComponent<Planet>().biome1;

        }
        if (temperature == -2 && humidity == 2)
        {
            GetComponentInChildren<MeshRenderer>().material = GameObject.Find("Planet").GetComponent<Planet>().biome2;

        }
        if (temperature == 2 && humidity == -2)
        {
            GetComponentInChildren<MeshRenderer>().material = GameObject.Find("Planet").GetComponent<Planet>().biome3;

        }
        if (temperature == -2 && humidity == -2)
        {
            GetComponentInChildren<MeshRenderer>().material = GameObject.Find("Planet").GetComponent<Planet>().biome4;

        }
        if (temperature != -2 && temperature != 2) {
            GetComponentInChildren<MeshRenderer>().material = GameObject.Find("Planet").GetComponent<Planet>().biome5;

        }*/
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
