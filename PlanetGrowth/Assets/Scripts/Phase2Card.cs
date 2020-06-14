using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Card : Card
{
    public Effect effects;

    public struct Effect
    {
        public int temperatureAdd;
        public int humidityAdd;
    }


    private new void Start()
    {
        base.Start();
        effects.humidityAdd = 0;
        effects.temperatureAdd = 0;
        generateNewCard();
    }

    private void generateNewCard()
    {
        String effectText = "";
        effectText += "Effekt auf Planet: \n";
        int numberOfEffects = UnityEngine.Random.Range(1, 3);
        while (numberOfEffects > 0)
        {
            numberOfEffects -= 1;
            int whichEffect = UnityEngine.Random.Range(0, 10);
            if (whichEffect < 5 && effects.temperatureAdd == 0)
            {
                effects.temperatureAdd = UnityEngine.Random.Range(-3, 4);
                if (effects.temperatureAdd != 0)
                {
                    effectText += "Temperatur + " + effects.temperatureAdd + "\n";
                }
            }
            else if (whichEffect < 9 && effects.humidityAdd == 0)
            {
                effects.humidityAdd = UnityEngine.Random.Range(-3, 4);
                if (effects.humidityAdd != 0)
                {
                    effectText += "Humidity + " + effects.humidityAdd + "\n";
                }
            }
            else if (whichEffect == 9)
            {
                effectText += "Special thing link nutritional value\n";
                    }

        }
        cost = Math.Max(0, effects.temperatureAdd + effects.humidityAdd+UnityEngine.Random.Range(0,3));
        effectText = "Cost: " + cost + "\n";

        cardText.text = effectText;

    }

    public override void Effects()
    {
        ExecuteEffects();
      //  Destroy(gameObject, 0.5f);
    }

    private void ExecuteEffects()
    {
        if (effects.temperatureAdd != 0)
        {
            GameObject.Find("Hand").GetComponent<Hand>().changeLocalTemperature(effects.temperatureAdd);
        }
        if (effects.humidityAdd != 0)
        {
            GameObject.Find("Hand").GetComponent<Hand>().changeLocalHumidity(effects.humidityAdd);
        }


    }
}


