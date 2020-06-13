using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hexagon : MonoBehaviour
{
    public Color color;
    public GameObject model1;
     int temperature;
     int humidity;

    // Start is called before the first frame update
    void Start()
    {
        model1.SetActive(true);
        transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Stop();
        int a = Random.Range(0, 2);
        
        if (a == 0)
        {
            temperature = 2;
        }
        else { temperature = -2; }
         a = Random.Range(0, 2);
        if (a == 0)
        {
            humidity = 2;
        }
        else { humidity = -2; }

        updateMaterial();
    }

    void updateMaterial()
    {

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
        updateMaterial();
    }

    public void setHumidity(int humid)
    {
        humidity = humid;
        updateMaterial();
    }
}
