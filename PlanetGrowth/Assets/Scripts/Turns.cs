using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turns : MonoBehaviour
{
    [SerializeField] private Hand hand;
    [SerializeField] private Planet planet;
    [SerializeField] private float timePerTurn = 20000;

    private int turnCounter;

    private int currentScore = 0;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (timePerTurn < time)
            nextTurn();
    }

    private void nextTurn()
    {
        hand.restockHand();
        hand.refillEnergy();
        currentScore += planet.getLivabilityScore();
        time = 0;
    }
}
