using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Adinicialize : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        MobileAds.Initialize(iniStatus => { });
    }
}
