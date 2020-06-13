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
    public Material highlightedHex;

    public int handSize = 5;
    public float handWidth = 2.0f;
    public float cardDragHeight = 0.2f;

    public float maxEnergy;
    public float energy = 0;
    public float energyPerSecond = 1;

    public LayerMask mouseColliderLayer;
    public LayerMask hexLayer;

    public Transform cardLayoutParent;
    public Transform cardSpawnLocation;
    public CardInfo[] cardInfos;
    private List<GameObject> possibleCardsToDraw = new List<GameObject>();

    private List<Card> handCards = new List<Card>();
    private Card heldCard = null;

    private Hexagon hoveredHex;

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
        UpdateCardLayoutPosition();
    }

    private void DrawCard()
    {
        var randomPrefab = possibleCardsToDraw[Random.Range(0, possibleCardsToDraw.Count)];
        var card = Instantiate(randomPrefab, cardLayoutParent);
        card.transform.localPosition = cardSpawnLocation.localPosition;
        card.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        card.transform.localScale = new Vector3(0, 0, 0);
        handCards.Add(card.GetComponent<Card>());
    }

    // Update is called once per frame
    void Update()
    {
        energy += energyPerSecond * Time.deltaTime;
        if (energy > maxEnergy)
            energy = maxEnergy;

        for (int i = 0; i < handCards.Count; i++)
        {
            var card = handCards[i];
            if (energy > card.cost && Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                PlayCard(card);
            }
        }

        foreach (var card in handCards)
            if (card.wasClicked)
                heldCard = card;
        UpdateCardLayoutPosition();

        // Select Card
        if (heldCard != null)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitPoint, 1000, mouseColliderLayer.value))
            {
                var target = cardLayoutParent.worldToLocalMatrix.MultiplyPoint(hitPoint.point);
                target.z = -cardDragHeight;
                heldCard.targetPosition = target;
            }
        }
        // Hover with Card
        if (heldCard != null && Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out var hitPoint, 1000, hexLayer.value);

          //  if ( hoveredHex.Equals(hitPoint.transform.gameObject))
          //  {
          //      Debug.Log("true");
          //  }
          //  else
          //  {
          //      Debug.Log("false");
          //  }

            Hexagon hex = hitPoint.transform.gameObject.GetComponent<Hexagon>();
            if (hex != null) hoveredHex = hex;
        }



            // Release Card
            if (heldCard != null && Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (energy > heldCard.cost && Physics.Raycast(ray, out var hitPoint, 1000, hexLayer.value))
            {
                // Debug.Log(hitPoint.transform.gameObject);
                MeshRenderer hex = hitPoint.transform.gameObject.GetComponent<MeshRenderer>();
                if (hex != null) hex.material = highlightedHex;
                //Destroy(hitPoint.transform.gameObject); // Get Hexagon the ray cast hit when releasing the card
                PlayCard(heldCard);
            }

            heldCard = null;
            UpdateCardLayoutPosition();
        }
    }

    private void PlayCard(Card card)
    {
        heldCard = null;
        energy -= card.cost;

        card.Effects();

        handCards.Remove(card);
        DrawCard();
        UpdateCardLayoutPosition();
    }

    private void UpdateCardLayoutPosition()
    {
        float stepSize = 2 * handWidth / handSize;
        for (int i = 0; i < handCards.Count; i++)
        {
            handCards[i].targetPosition = new Vector3((i + 0.5f) * stepSize - handWidth, 0, 0);
        }
    }
}
