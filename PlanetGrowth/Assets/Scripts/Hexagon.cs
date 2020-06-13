using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    public Color color;

     int temperature;
     int humidity;

    // Start is called before the first frame update
    void Start()
    {
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
