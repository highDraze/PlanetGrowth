using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
   public int energy = 3;
    public List<Card> handCards = new List<Card>();
    // Start is called before the first frame update
    void Start()
    {
        handCards.Add(GameObject.Find("Card").GetComponent<Card>());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Energy: " + energy);
        if (Input.GetKeyDown(KeyCode.Alpha1)&& handCards.Count > 0 && energy > handCards[0].cost )
        {
            energy -= handCards[0].cost;
            handCards[0].Effect();
            handCards.RemoveAt(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && handCards.Count > 1 && energy > handCards[1].cost)
        {
            energy -= handCards[1].cost;
            handCards[1].Effect();
            handCards.RemoveAt(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && handCards.Count > 2 && energy > handCards[2].cost)
        {
            energy -= handCards[2].cost;
            handCards[2].Effect();
            handCards.RemoveAt(2);
        }
    }
}
