using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public abstract class Card : MonoBehaviour
{
    public int cost = 1;
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 10.0f;
    public float scaleSpeed = 10.0f;

    public float targetAngle;
    public Vector3 targetPosition;
    public Vector3 targetScale = new Vector3(1, 1, 1);
    public bool wasClicked = false;
    Effect effects;
    TMPro.TextMeshPro cardText;

    struct Effect {
       public int temperature;
        public int humidity;
        public int airquality;
    }

    private void Start()
    {
        cardText = transform.Find("Visuals/Text").GetComponent<TextMeshPro>();
        generateNewPhase1Card();

    }

    private void generateNewPhase1Card()
    {
        String effectText = "Effect: \n";
        if (0 == UnityEngine.Random.Range(0, 2)) {
            effects.temperature = UnityEngine.Random.Range(1, 4);
            effectText += "Temperatur + " + effects.temperature+ "\n";
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

    public abstract void Effects();

    void Update()
    {
        wasClicked = false;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(new Vector3(0, Mathf.LerpAngle(transform.localRotation.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime)));
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
    }

    void OnMouseDown()
    {
        wasClicked = true;
    }
}
