using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class Turns : MonoBehaviour {
    [SerializeField] private Hand hand;
    [SerializeField] private ZylinderPlanet planet;
    [SerializeField] private float timePerTurn;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI currentTimeText;
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] public TextMeshProUGUI currentScoreText;

    private int turnCounter = 10;

    private int currentScore = 0;
    private float time;

    // Start is called before the first frame update
    void Start() {
        time = timePerTurn;
        turnText.text = turnCounter.ToString();
    }

    // Update is called once per frame
    void Update() {
        time -= Time.deltaTime;
        currentTimeText.text = time.ToString("#0.0");
        if (0 > time)
            nextTurn();
    }

    private void nextTurn() {
        int score = planet.getLivabilityScore();
        if (score < 0 && hand.phase == 3) gameOver();
        if (hand.GetPhase() == 1 && turnCounter == 0) {
            hand.SetPhase(2);
            turnCounter = 100;
            hand.SetHandSize(5);
            timePerTurn = 20;
        }
        hand.restockHand();
        hand.refillEnergy();
        currentScore += score;
        turnCounter--;

        scoreText.text = currentScore.ToString();
        turnText.text = turnCounter.ToString();
        time = timePerTurn;
    }

    private void gameOver() {
        Debug.Log("Planet not livable, game over");
    }
}
