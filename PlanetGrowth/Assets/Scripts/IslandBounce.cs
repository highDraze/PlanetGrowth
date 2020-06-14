using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandBounce : MonoBehaviour
{
    public Transform left;
    public Transform right;

    public float journeyTime = 600.0f;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }
     void Update()
    {
        Vector3 center = (left.position + right.position) * 0.5F;

        center -= new Vector3(0, 1, 0);

        Vector3 riseRelCenter = left.position - center;
        Vector3 setRelCenter = right.position - center;

        float fracComplete = (Time.time - startTime) / journeyTime;

        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;
    }
}
