using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Reclam : MonoBehaviour
{
    private InterstitialAd intrestitia;
    private const string adUnitID = "	ca-app-pub-3940256099942544/6300978111";

    private void OnEnable()
    {
        this.intrestitia = new InterstitialAd(adUnitID);
        AdRequest request = new AdRequest.Builder().Build();
        this.intrestitia.LoadAd(request);

    }
    public void Show()
    {

        if(this.intrestitia.IsLoaded())
        {
            this.intrestitia.Show();    
        }
    }
}
