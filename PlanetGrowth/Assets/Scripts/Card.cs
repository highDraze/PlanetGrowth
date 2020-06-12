using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cost = 1;

    public float moveSpeed = 5.0f;
    public Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
    }

    public void Effect() {
            //GameObject.Find("Planet").GetComponent<Planet>().temperature += 1;
       
    }
}
