using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollorManeg : MonoBehaviour
{
    public static CollorManeg instance;
    public Color[] ceelColors; // храним цвета плиток
    [Space(5)]
    public Color pointDarkCollor;
    public Color pointLightCollor;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

}
