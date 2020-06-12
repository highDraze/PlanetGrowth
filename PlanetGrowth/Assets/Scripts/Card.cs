using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public abstract class Card : MonoBehaviour
{
    public int cost = 1;
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 10.0f;
    public float scaleSpeed = 10.0f;

    public float targetAngle;
    public Vector3 targetPosition;
    public Vector3 targetScale = new Vector3(1, 1, 1);
    public bool wasClicked = false;

    public abstract void Effect();

    void Update()
    {
        wasClicked = false;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(new Vector3(0, Mathf.LerpAngle(transform.localRotation.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime)));
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
    }

    void OnMouseDown()
    {
        wasClicked = true;
    }
}
