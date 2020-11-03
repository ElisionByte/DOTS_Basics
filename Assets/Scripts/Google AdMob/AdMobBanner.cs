using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMobBanner : MonoBehaviour
{
    private BannerView _bannerView;

    private void Start()
    {
        this.RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#else
            string adUnitId = "unexpected_platform";
#endif    
        if (this._bannerView != null)
        {
            this._bannerView.Destroy();
        }

        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        this._bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

        this._bannerView.OnAdLoaded += this.HandleAdLoaded;
        this._bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this._bannerView.OnAdOpening += this.HandleAdOpened;
        this._bannerView.OnAdClosed += this.HandleAdClosed;
        this._bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

        AdRequest request = new AdRequest.Builder().Build();

        this._bannerView.LoadAd(request);
    }

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        MonoBehaviour.print(String.Format("Ad Height: {0}, width: {1}",
            this._bannerView.GetHeightInPixels(),
            this._bannerView.GetWidthInPixels()));
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
                "HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }
}

