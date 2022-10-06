using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform bar;

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector2(sizeNormalized, 1f);
    }
}
