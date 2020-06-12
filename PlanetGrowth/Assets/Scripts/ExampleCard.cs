using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleCard : Card
{
    public override void Effect()
    {
        Debug.Log("Example card played");
        Destroy(gameObject, 0.5f);
    }
}
