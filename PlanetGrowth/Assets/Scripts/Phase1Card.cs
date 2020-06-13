using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Card : Card
{
    public Effect effects;

    public struct Effect
    {
        public int temperatureAdd;
        public int humidityAdd;
        public int temperatureSub;
        public int humiditySub;
    }


    private new void Start()
    {
        base.Start();
        effects.humidityAdd = 0;
        effects.humiditySub = 0;
        effects.temperatureAdd = 0;
        effects.temperatureSub = 0;
        generateNewCard();
    }
    public override void Effects()
    {
        ExecuteEffects();
        Debug.Log("Example card played");
     //   Destroy(gameObject, 0.5f);
    }

    private void ExecuteEffects()
    {
        if (effects.temperatureAdd != 0)
        {
            Planet planet = GameObject.Find("Planet").GetComponent<Planet>();
            planet.raiseTempOfXRandomHex(effects.temperatureAdd);
        }
        if (effects.humidityAdd != 0)
        {
            Planet planet = GameObject.Find("Planet").GetComponent<Planet>();
            planet.raiseHumidOfXRandomHex(effects.humidityAdd);
        }
        if (effects.temperatureSub != 0)
        {
            Planet planet = GameObject.Find("Planet").GetComponent<Planet>();
            planet.lowerTempOfXRandomHex(effects.temperatureSub);
        }
        if (effects.humiditySub != 0)
        {
            Planet planet = GameObject.Find("Planet").GetComponent<Planet>();
            planet.lowerHumidOfXRandomHex(effects.humiditySub);
        }
    }

    public void generateNewCard()
    {
        String effectText = "Cost: " + cost + "\n";
        effectText += "Effekt auf Planet: \n";
        int numberOfEffects = UnityEngine.Random.Range(1, 4);
        while (numberOfEffects > 0)
        {
            numberOfEffects -= 1;
            int whichEffect = UnityEngine.Random.Range(0, 4);
            if (whichEffect == 0 && effects.temperatureAdd == 0)
            {
                effects.temperatureAdd = UnityEngine.Random.Range(5, 20);
                effectText += "Temperatur + " + effects.temperatureAdd + "\n";
            }
            if (whichEffect == 1 && effects.humidityAdd == 0)
            {
                effects.humidityAdd = UnityEngine.Random.Range(5, 20);
                effectText += "Humidity + " + effects.humidityAdd + "\n";
            }
            if (whichEffect == 2 && effects.temperatureSub == 0)
            {
                effects.temperatureSub = UnityEngine.Random.Range(5, 20);
                effectText += "Temperatur - " + effects.temperatureSub + "\n";
            }
            if (whichEffect == 3 && effects.humiditySub == 0)
            {
                effects.humiditySub = UnityEngine.Random.Range(5, 20);
                effectText += "Humidity - " + effects.humiditySub + "\n";
            }
        }
        //  cost = effects.temperatureAdd + effects.humidityAdd + effects.humiditySub + effects.temperatureSub;
        cost = 1;

        cardText.text = effectText;
    }
}
