using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Card : Card
{
    public enum Biomes {Arktis, Forest, Jungle, Ocean, Swamp, Ödland, Wiese, Wüste}
    public Effect effects;
    public Sprite[] artList;
    public Biomes[,] arts;

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
        arts = new Biomes[5, 5] {
                                { Biomes.Ödland, Biomes.Ödland, Biomes.Ödland, Biomes.Wüste, Biomes.Wüste},
                                { Biomes.Arktis, Biomes.Arktis, Biomes.Ödland, Biomes.Wüste, Biomes.Wüste},
                                { Biomes.Arktis, Biomes.Forest, Biomes.Wiese, Biomes.Wiese, Biomes.Wüste},
                                { Biomes.Ocean, Biomes.Forest, Biomes.Wiese, Biomes.Wiese, Biomes.Jungle},
                                { Biomes.Ocean, Biomes.Ocean, Biomes.Ocean, Biomes.Jungle, Biomes.Swamp }
                                };

        SpriteRenderer sr = transform.Find("Visuals").transform.Find("Art").gameObject.GetComponent<SpriteRenderer>();


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
        cardText.text = effectText;
    }
}
