using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public abstract class Card : MonoBehaviour
{
    public int cost = 1;
    public Vector3 targetPosition;

    public abstract void Effect();
}
