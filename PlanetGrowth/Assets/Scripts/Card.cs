using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public abstract class Card : MonoBehaviour
{
    public int cost = 1;
    public Vector3 targetPosition;
    public bool wasClicked = false;
    public float moveSpeed = 10.0f;

    public abstract void Effect();

    void Update()
    {
        wasClicked = false;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
    }

    void OnMouseDown()
    {
        wasClicked = true;
    }
}
