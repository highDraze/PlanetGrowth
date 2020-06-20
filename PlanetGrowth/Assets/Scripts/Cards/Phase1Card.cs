using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Phase1Card : Card {

    public enum Biomes {
        Arktis,
        Forest,
        Jungle,
        Ocean,
        Swamp,
        Ödland,
        Wiese,
        Wüste
    }
    public Effect effects;
    public Sprite[] artList;
    public Biomes[, ] arts;

    public Sprite meteor;
    public Sprite iceAge;
    public Sprite volcano;

    int MeteorChance;
    int IceAgeChance;
    int VolcanoChance;

    int whichCat;

    public struct Effect {
        public int temperatureAdd;
        public int humidityAdd;
        public int temperatureSub;
        public int humiditySub;
        public int CatNumber;
        public int CatChance;
    }

    private new void Start() {
        base.Start();
        effects.humidityAdd = 0;
        effects.humiditySub = 0;
        effects.temperatureAdd = 0;
        effects.temperatureSub = 0;
        effects.CatNumber = 0;
        effects.CatChance = 0;
        generateNewCard();
    }
    public override void Effects() {
        ExecuteEffects();
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
        if (effects.CatNumber == 1) {
            ZylinderPlanet planet = GameObject.Find("Planet").GetComponent<ZylinderPlanet>();
            planet.increaseCatastrophe(effects.CatNumber, MeteorChance);
        }
        if (effects.CatNumber == 2) {
            ZylinderPlanet planet = GameObject.Find("Planet").GetComponent<ZylinderPlanet>();
            planet.increaseCatastrophe(effects.CatNumber, IceAgeChance);
        }
        if (effects.CatNumber == 3) {
            ZylinderPlanet planet = GameObject.Find("Planet").GetComponent<ZylinderPlanet>();
            planet.increaseCatastrophe(effects.CatNumber, VolcanoChance);
        }
    }

    public void generateNewCard() {
        arts = new Biomes[5, 5] {
            {
            Biomes.Ödland, Biomes.Ödland, Biomes.Ödland, Biomes.Wüste, Biomes.Wüste
            }, {
            Biomes.Arktis,
            Biomes.Arktis,
            Biomes.Ödland,
            Biomes.Wüste,
            Biomes.Wüste
            }, {
            Biomes.Arktis,
            Biomes.Forest,
            Biomes.Wiese,
            Biomes.Wiese,
            Biomes.Wüste
            }, {
            Biomes.Ocean,
            Biomes.Forest,
            Biomes.Wiese,
            Biomes.Wiese,
            Biomes.Jungle
            }, {
            Biomes.Ocean,
            Biomes.Ocean,
            Biomes.Ocean,
            Biomes.Jungle,
            Biomes.Swamp
            }
        };

        SpriteRenderer sr = transform.Find("Visuals").transform.Find("Art").gameObject.GetComponent<SpriteRenderer>();


        String effectText = "";
        effectText += "Effekt auf Planet: \n";
        int numberOfEffects = UnityEngine.Random.Range(1, 4);

        MeteorChance = UnityEngine.Random.Range(5, 15);
        IceAgeChance = UnityEngine.Random.Range(5, 15);
        VolcanoChance = UnityEngine.Random.Range(5, 15);

        effects.temperatureAdd = 0;
        effects.temperatureSub = 0;
        effects.humidityAdd = 0;
        effects.humiditySub = 0;

        int rareEffect = UnityEngine.Random.Range(1, 20);

        while (numberOfEffects > 0) {
            numberOfEffects -= 1;
            int whichEffect = UnityEngine.Random.Range(0, 4);
            whichCat = UnityEngine.Random.Range(1, 3);
           

            //Catastrophs
            if (whichCat == 1 && rareEffect == 5) {
                effectText += "Meteor % + " + MeteorChance + "\n";
                effects.CatNumber = 1;
                effects.CatChance = MeteorChance;
                effects.humidityAdd += 5;
                effects.humiditySub += 5;
                effects.temperatureAdd += 5;
                effects.temperatureSub += 5;
                effects.humiditySub = (int) ((double) effects.humiditySub * 1.5);
                effects.humidityAdd = (int) ((double) effects.humidityAdd * 1.5);
                effects.temperatureSub = (int) ((double) effects.temperatureSub * 1.5);
                effects.temperatureAdd = (int) ((double) effects.temperatureAdd * 1.5);
                continue;
            }
            if (whichCat == 2 && rareEffect == 7) {
                effectText += "IceAge % + " + IceAgeChance + "\n";
                effects.CatNumber = 2;
                effects.CatChance = MeteorChance;
                effects.humidityAdd += 5;
                effects.humiditySub += 5;
                effects.temperatureAdd += 5;
                effects.temperatureSub += 5;
                effects.humiditySub = (int) ((double) effects.humiditySub * 1.5);
                effects.humidityAdd = (int) ((double) effects.humidityAdd * 1.5);
                effects.temperatureSub = (int) ((double) effects.temperatureSub * 1.5);
                effects.temperatureAdd = (int) ((double) effects.temperatureAdd * 1.5);
                continue;
            }
            if (whichCat == 3 && rareEffect == 11) {
                effectText += "Volcano % + " + VolcanoChance + "\n";
                effects.CatNumber = 3;
                effects.CatChance = MeteorChance;
                effects.humidityAdd += 5;
                effects.humiditySub += 5;
                effects.temperatureAdd += 5;
                effects.temperatureSub += 5;
                effects.humiditySub = (int) ((double) effects.humiditySub * 1.5);
                effects.humidityAdd = (int) ((double) effects.humidityAdd * 1.5);
                effects.temperatureSub = (int) ((double) effects.temperatureSub * 1.5);
                effects.temperatureAdd = (int) ((double) effects.temperatureAdd * 1.5);
                continue;
            }

            if (whichEffect == 0 && effects.temperatureAdd == 0) {
                effects.temperatureAdd += UnityEngine.Random.Range(5, 20);
                effectText += "Temperatur + " + effects.temperatureAdd + "\n";
            }
            if (whichEffect == 1 && effects.humidityAdd == 0) {
                effects.humidityAdd += UnityEngine.Random.Range(5, 20);
                effectText += "Humidity + " + effects.humidityAdd + "\n";
            }
            if (whichEffect == 2 && effects.temperatureSub == 0) {
                effects.temperatureSub += UnityEngine.Random.Range(5, 20);
                effectText += "Temperatur - " + effects.temperatureSub + "\n";
            }
            if (whichEffect == 3 && effects.humiditySub == 0) {
                effects.humiditySub += UnityEngine.Random.Range(5, 20);
                effectText += "Humidity - " + effects.humiditySub + "\n";
            }


        }
        //  cost = effects.temperatureAdd + effects.humidityAdd + effects.humiditySub + effects.temperatureSub;
        cost = 1;
        effectText += "Cost: " + cost + "\n";
        int tempSum = effects.temperatureAdd + effects.temperatureSub;
        int humidSum = effects.humidityAdd + effects.humiditySub;

        int x;
        int y;



        if (tempSum < -17)
            x = -2;
        else if (tempSum < -13)
            x = -1;
        else if (tempSum < 13)
            x = 0;
        else if (tempSum < 17)
            x = 1;
        else
            x = 2;

        if (humidSum < -17)
            y = -2;
        else if (humidSum < -13)
            y = -1;
        else if (humidSum < 13)
            y = 0;
        else if (humidSum < 17)
            y = 1;
        else
            y = 2;


        //sr.sprite = artList[(int)arts[x,y]];
        sr.sprite = artList[UnityEngine.Random.Range(0, artList.Length)];
        if (rareEffect == 5) {
            sr.sprite = meteor;
        }
        if (rareEffect == 7) {
            sr.sprite = iceAge;
        }
        if (rareEffect == 11) {
            sr.sprite = volcano;
        }
        cardText.text = effectText;

    }
}
