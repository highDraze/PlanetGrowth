﻿using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ExampleCard : Card {
    Effect effects;
    public bool autoPlay = false;

    struct Effect {
        public int temperatureAdd;
        public int humidityAdd;
        public int temperatureSub;
        public int humiditySub;
    }

    private void Start() {
        base.Start();
        effects.humidityAdd = 0;
        effects.humiditySub = 0;
        effects.temperatureAdd = 0;
        effects.temperatureSub = 0;
        generateNewPhase1Card();
    }
    public override void Effects() {
        ExecuteEffects();
        Debug.Log("Example card played");
        Destroy(gameObject, 0.5f);
    }

    private void ExecuteEffects() {
        if (effects.temperatureAdd != 0) {
            ZylinderPlanet planet = GameObject.Find("Planet").GetComponent<ZylinderPlanet>();
            planet.raiseTempOfXRandomHex(effects.temperatureAdd);
        }
        if (effects.humidityAdd != 0) {
            ZylinderPlanet planet = GameObject.Find("Planet").GetComponent<ZylinderPlanet>();
            planet.raiseHumidOfXRandomHex(effects.humidityAdd);
        }
        if (effects.temperatureSub != 0) {
            ZylinderPlanet planet = GameObject.Find("Planet").GetComponent<ZylinderPlanet>();
            planet.lowerTempOfXRandomHex(effects.temperatureSub);
        }
        if (effects.humiditySub != 0) {
            ZylinderPlanet planet = GameObject.Find("Planet").GetComponent<ZylinderPlanet>();
            planet.lowerHumidOfXRandomHex(effects.humiditySub);
        }
    }

    private void generateNewPhase1Card() {
        String effectText = "Cost: " + cost + "\n";
        effectText += "Effekt auf Planet: \n";
        int numberOfEffects = UnityEngine.Random.Range(1, 4);
        while (numberOfEffects > 0) {
            numberOfEffects -= 1;
            int whichEffect = UnityEngine.Random.Range(0, 4);
            switch (whichEffect) {
            case 0 when effects.temperatureAdd == 0:
                effects.temperatureAdd = UnityEngine.Random.Range(1, 30);
                effectText += "Temperatur + " + effects.temperatureAdd + "\n";
                break;
            case 1 when effects.humidityAdd == 0:
                effects.humidityAdd = UnityEngine.Random.Range(1, 30);
                effectText += "Humidity + " + effects.humidityAdd + "\n";
                break;
            case 2 when effects.temperatureSub == 0:
                effects.temperatureSub = UnityEngine.Random.Range(1, 30);
                effectText += "Temperatur - " + effects.temperatureSub + "\n";
                break;
            case 3 when effects.humiditySub == 0:
                effects.humiditySub = UnityEngine.Random.Range(1, 30);
                effectText += "Humidity - " + effects.humiditySub + "\n";
                autoPlay = true;
                break;
            }
        }
        //  cost = effects.temperatureAdd + effects.humidityAdd + effects.humiditySub + effects.temperatureSub;
        cost = 1;
        cardText.text = effectText;
    }
}
