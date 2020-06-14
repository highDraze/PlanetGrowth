using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Turns : MonoBehaviour
{
    [SerializeField] private Hand hand;
    [SerializeField] private Planet planet;
    [SerializeField] private float timePerTurn = 20000;
    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private TextMeshPro currentTimeText;
    [SerializeField] private TextMeshPro turnText;
    [SerializeField] private TextMeshPro currentScoreText;

    private int turnCounter;

    private int currentScore = 0;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = timePerTurn;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        currentTimeText.text = time.ToString();
        if (0 > time)
            nextTurn();
    }

    private void nextTurn()
    {
        int score = planet.getLivabilityScore();
        currentScoreText.text = score.ToString();
        if (score < 0) gameOver();
        hand.restockHand();
        hand.refillEnergy();
        currentScore += score;
        turnCounter++;
        scoreText.text = currentScore.ToString();
        turnText.text = turnCounter.ToString();
        time = timePerTurn;
    }

    private void gameOver()
    {
    }
}
