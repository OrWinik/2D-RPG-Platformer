using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float timeToDestroy = 1f;

    void Update()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
