using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class EnergySlider : MonoBehaviour {
    private Hand hand;
    private Slider slider;

    void Start() {
        hand = GameObject.FindObjectOfType<Hand>();
        if (hand == null) {
            Debug.LogError("Could not find hand in scene");
            enabled = false;
            return;
        }
        slider = GetComponent<Slider>();
        slider.maxValue = hand.maxEnergy;
    }

    // Update is called once per frame
    void Update() {
        slider.value = hand.energy;
    }
}
