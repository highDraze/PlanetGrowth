using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public struct CardInfo
{
    public GameObject card;
    public int amount;
}

public class Hand : MonoBehaviour
{

   public int phase = 1;

    public Turns turnsScript;

    public Material highlightedHex;
    public Material normalHex;

    public AudioSource[] Sounds;
        public AudioSource hand;
        public AudioSource draw;
        public AudioSource shuffle;


    int handSize = 3;
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

    private List<int> hovereredList = new List<int>();
    private int hoveredHex;

    // Start is called before the first frame update
    public Card HeldCard => heldCard;

    void Start()
    {
        Sounds = GetComponents<AudioSource>();
        turnsScript = transform.GetComponent<Turns>();
        hand = Sounds[1];
        draw = Sounds[0];
        shuffle = Sounds[2];
        energy = maxEnergy;

        //     foreach (var cardInfo in cardInfos)
        //         for (int i = 0; i < cardInfo.amount; i++) { 
        //    possibleCardsToDraw.Add(cardInfo.card); }
        possibleCardsToDraw.Add(cardInfos[0].card);



        if (possibleCardsToDraw.Count == 0)
        {
            Debug.LogError("No cards provided");
            Destroy(gameObject);
            return;
        }
        redraw();
    }

    private void DrawCard()
    {
        var randomPrefab = possibleCardsToDraw[Random.Range(0, possibleCardsToDraw.Count)];
        var card = Instantiate(randomPrefab, cardLayoutParent);
        card.transform.localPosition = cardSpawnLocation.localPosition;
        card.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        card.transform.localScale = new Vector3(0, 0, 0);
     //   var ca = card.GetComponent<ExampleCard>();
        handCards.Add(card.GetComponent<Card>());
   //     play_auto_card(ca);
    }

    private void play_auto_card(ExampleCard ca)
    {
        if (!ca.autoPlay) return;
        StartCoroutine(startPlayinAuto(ca));
    }

    private IEnumerator startPlayinAuto(ExampleCard ca)
    {
        yield return new WaitForSeconds(.5f);
        PlayCard(ca);
    }


    // Update is called once per frame
    void Update()
    {

        //passiveEnergyRefill();

        for (int i = 0; i < handCards.Count; i++)
        {
            var card = handCards[i];
            /** if (energy > card.cost && Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                Debug.Log(heldCard);
                PlayCard(card);
            }*/
        }

        foreach (var card in handCards)
            if (card.wasClicked)
                heldCard = card;
        UpdateCardLayoutPosition();
        selectCard();
        hoverWithCard();

        if (heldCard != null && Input.GetMouseButtonUp(0))
        {
            releaseCard();
        }
    }

    private void passiveEnergyRefill()
    {
        energy += energyPerSecond * Time.deltaTime;
    }

    public void refillEnergy()
    {
        energy = maxEnergy;
    }

private void selectCard()
    {
        if (heldCard != null)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitPoint, 1000, mouseColliderLayer.value))
            {
                var target = cardLayoutParent.worldToLocalMatrix.MultiplyPoint(hitPoint.point);
                target.z = -cardDragHeight;
                heldCard.targetPosition = target;
            }

            // get selection method
        }
    }

    private void hoverWithCard()
    {
        if (heldCard != null && Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            Physics.Raycast(ray, out var hitPoint, 10000, hexLayer.value);

            if (hitPoint.transform != null)
            {
          
                MeshRenderer hex = hitPoint.transform.gameObject.GetComponent<MeshRenderer>();
                
                if (hex != null)
                {
                    
                    // selection cases

                    // single
                   
                    int new_index = GameObject.Find("Planet").GetComponent<Planet>()
                        .getHexagonIndex(hitPoint.transform.GetComponentInParent<Hexagon>().transform);
                  
                    //hex.material = highlightedHex;

                    if (new_index != hoveredHex)
                    {
                        if (phase == 1)
                        {
                          
                            selectSingle(new_index);
                        }

                        if (phase == 2)
                        {
                            selectSeven(new_index);
                        }

                        hoveredHex = new_index;
                    }
                }
            }
        }
    }

    private void releaseCard()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       

        if (energy >= heldCard.cost &&
            Physics.Raycast(ray,out var hit, 1000, hexLayer))
        {

            PlayCard(heldCard);
        }
        heldCard = null;
        UpdateCardLayoutPosition();
    }

    private void PlayCard(Card card)
    {
        Debug.Log("playCard");
        draw.Play();
      
        heldCard = null;
        energy -= card.cost;

        card.Effects();
        Destroy(card.gameObject, 0.5f);
        handCards.Remove(card);

        if (phase == 1)
        {
            removeCards();
        }
        
        turnsScript.currentScoreText.text = GameObject.Find("Planet").GetComponent<Planet>().getLivabilityScore().ToString();
    }

    private void redraw()
    {
        while (handCards.Count < handSize)
            DrawCard();
        UpdateCardLayoutPosition();
    }

    private void removeCards()
    {
        foreach (Card c in handCards)
            Destroy(c.gameObject, 0f);
        handCards = new List<Card>();
    }

    public void restockHand()
    {
        shuffle.Play();
        removeCards();
        redraw();
    }

    private void UpdateCardLayoutPosition()
    {
        float stepSize = 2 * handWidth / handSize;
        for (int i = 0; i < handCards.Count; i++)
        {
            handCards[i].targetPosition = new Vector3((i + 0.5f) * stepSize - handWidth, 0, 0);
        }
    }

    private void selectSingle(int new_index)
    {
  
        GameObject.Find("Planet").GetComponent<Planet>().highlightBiome(new_index, true);
        GameObject.Find("Planet").GetComponent<Planet>().highlightBiome(hoveredHex, false);
    }

    private void selectSeven(int new_index)
    {
        Planet planet = GameObject.Find("Planet").GetComponent<Planet>();
        int[] indexMods = { 0, -1, -10, 1, 9, 10, 11 };
        if (new_index % 2 == 0) // 
        {
            indexMods[4] = -9;
            indexMods[6] = -11;
        }

        int hex_end = planet.surfaceHexagons.Count;

        foreach (int ele in hovereredList)
        {
            planet.highlightBiome(ele, false);
        }

        hovereredList = new List<int>();

        for (int i = 0; i < indexMods.Length; i++)
        {

            if (new_index % planet.gridWidth == 0)
            {
                if ((new_index + indexMods[i]) % planet.gridWidth == planet.gridWidth - 1) continue;
            }
            if (new_index % planet.gridWidth == planet.gridWidth - 1)
            {
                if ((new_index + indexMods[i]) % planet.gridWidth == 0) continue;
            }
            int temp = new_index % planet.gridWidth;

            //if (temp + indexMods[i] >= 10) continue;

            if (new_index + indexMods[i] < 0)
            {
                hovereredList.Add(new_index + indexMods[i] + hex_end);
            }
            else if (new_index + indexMods[i] >= hex_end)
            {
                hovereredList.Add(new_index + indexMods[i] - hex_end);
            }
            else
            {
                hovereredList.Add(new_index + indexMods[i]);
            }
            
        }

        foreach ( int ele in hovereredList)
        {
            planet.highlightBiome(ele, true);
        }
    }

    public void changeLocalTemperature(int value)
    {
        foreach (int i in hovereredList) {
            GameObject.Find("Planet").GetComponent<Planet>().surfaceHexagons[i].GetComponent<Hexagon>().changeTemperatur(value);
        }
    }
    public void changeLocalHumidity(int value)
    {
        foreach (int i in hovereredList)
        {
            GameObject.Find("Planet").GetComponent<Planet>().surfaceHexagons[i].GetComponent<Hexagon>().changeHumidity(value);
        }
    }

    public int GetPhase() { return phase; }
    public void SetPhase(int newPhase)
    { 
        phase = newPhase;
        if (phase == 2) { 
        possibleCardsToDraw = new List<GameObject>();
            possibleCardsToDraw.Add(cardInfos[1].card);
        }
    }

    public void SetHandSize(int newHandSize) { handSize = newHandSize; }
}
