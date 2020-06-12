using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public struct CardInfo
{
    public GameObject card;
    public int amount;
}

public class Hand : MonoBehaviour
{
    public int handSize = 5;

    public int maxEnergy = 3;
    public int energy = 0;

    public Transform cardLayoutParent;
    public CardInfo[] cardInfos;
    private List<GameObject> possibleCardsToDraw = new List<GameObject>();

    private List<Card> handCards = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        energy = maxEnergy;
        foreach (var cardInfo in cardInfos)
            for (int i = 0; i < cardInfo.amount; i++)
                possibleCardsToDraw.Add(cardInfo.card);

        if (possibleCardsToDraw.Count == 0)
        {
            Debug.LogError("No cards provided");
            Destroy(gameObject);
            return;
        }

        while (handCards.Count < handSize)
            DrawCard();
        LayoutCards();
    }

    private void DrawCard()
    {
        var randomPrefab = possibleCardsToDraw[Random.Range(0, possibleCardsToDraw.Count)];
        var card = Instantiate(randomPrefab, cardLayoutParent);
        handCards.Add(card.GetComponent<Card>());
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < handCards.Count; i++)
        {
            var card = handCards[i];
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) && energy > card.cost)
            {
                energy -= card.cost;
                card.Effect();
                handCards.RemoveAt(i);
                Destroy(card.gameObject);
                DrawCard();
                LayoutCards();
            }
        }
    }

    private void LayoutCards()
    {
        float width = 5.0f;
        float stepSize = 2 * width / handSize;
        for (int i = 0; i < handCards.Count; i++)
        {
            handCards[i].targetPosition = new Vector3(i * stepSize - width, 0, 0);
        }
    }
}
