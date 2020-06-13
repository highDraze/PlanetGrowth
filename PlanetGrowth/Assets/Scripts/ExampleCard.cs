using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleCard : Card
{
     Effect effects;

     struct Effect
    {
        public int temperature;
        public int humidity;
        public int airquality;
    }


    private void Start()
    {
        base.Start();
        generateNewPhase1Card();
    }
    public override void Effects()
    {
        ExecuteEffects();
        Debug.Log("Example card played");
        Destroy(gameObject, 0.5f);
    }

    private void ExecuteEffects()
    {
        if (effects.temperature != 0) {
           Planet planet = GameObject.Find("Planet").GetComponent<Planet>();
            planet.temperature += effects.temperature;    
        }
        if (effects.humidity != 0)
        {
            Planet planet = GameObject.Find("Planet").GetComponent<Planet>();
            planet.humidity += effects.humidity;
        }
        if (effects.airquality != 0)
        {
            Planet planet = GameObject.Find("Planet").GetComponent<Planet>();
            planet.airquality += effects.airquality;
        }
    }

    private void generateNewPhase1Card()
    {
        String effectText = "Effect: \n";
        if (0 == UnityEngine.Random.Range(0, 2))
        {
            effects.temperature = UnityEngine.Random.Range(1, 4);
            effectText += "Temperatur + " + effects.temperature + "\n";
        }
        if (0 == UnityEngine.Random.Range(0, 2))
        {
            effects.humidity = UnityEngine.Random.Range(1, 4);
            effectText += "Humidity + " + effects.humidity + "\n";
        }
        if (0 == UnityEngine.Random.Range(0, 2))
        {
            effects.airquality = UnityEngine.Random.Range(1, 4);
            effectText += "Airquality + " + effects.airquality + "\n";
        }
        cost = effects.temperature + effects.humidity + effects.airquality;
        effectText += "Cost: " + cost;
        cardText.text = effectText;
    }
}
