using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobAuthorization : MonoBehaviour
{
    private void Awake()
    {
        MobileAds.Initialize(initStatus => {});
    }
}
